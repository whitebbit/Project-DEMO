import { Room, Client } from "colyseus";
import { Schema, type, MapSchema } from "@colyseus/schema";

interface SpawnPoint {
    x: number;
    y: number;
    z: number;
  }

export class Player extends Schema {

    @type("number")
    spX = 0;

    @type("number")
    spY = 0;

    @type("number")
    spZ = 0;
    
    @type("number")
    pX = 0;

    @type("number")
    pY = 0;

    @type("number")
    pZ = 0;

    @type("number")
    vX = 0;

    @type("number")
    vY = 0;

    @type("number")
    vZ = 0;

    @type("number")
    rX = 0;

    @type("number")
    rY = 0;

    @type("number")
    speed = 0;

    @type("int8")
    maxHP = 0;

    @type("int8")
    currentHP = 0;

    @type("uint8")
    loss = 0;
}

export class State extends Schema {
    @type({ map: Player })
    players = new MapSchema<Player>();

    something = "This attribute won't be sent to the client-side";

    createPlayer(sessionId: string, data: any) {
        const player = new Player();

        player.maxHP = data.hp;
        player.currentHP = data.hp;
        player.speed = data.speed;

        this.players.set(sessionId, player);
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    movePlayer (sessionId: string, data: any) {
        const player = this.players.get(sessionId);

        player.pX = data.pX;
        player.pY = data.pY;
        player.pZ = data.pZ;
        
        player.vX = data.vX;
        player.vY = data.vY;
        player.vZ = data.vZ;

        player.rX = data.rX;
        player.rY = data.rY;
    }
}

export class StateHandlerRoom extends Room<State> {
    allSpawnPoints: SpawnPoint[] = [];
    usedSpawnIndexes: Set<number> = new Set();
    spawnPointsReady: boolean = false;
    waitingClients: { client: Client; data: any }[] = [];
    maxClients = 2;

    onCreate(options: any) {
        this.setState(new State());

        this.onMessage("register_spawn_points", (client, data: any[]) => {
          if (this.spawnPointsReady) return; 
    
          for (const p of data) {
            this.allSpawnPoints.push({ x: p.x, y: p.y, z: p.z });
          }
    
          this.spawnPointsReady = true;
    
          for (const entry of this.waitingClients) {
            this.spawnPlayer(entry.client, entry.data);
          }

          this.waitingClients = [];
        });
    
        this.onMessage("move", (client, data) => {
          this.state.movePlayer(client.sessionId, data);
        });
    
        this.onMessage("shoot", (client, data) => {
          this.broadcast("Shoot", data, { except: client });
        });

        this.onMessage("damage", (client, data) => {
            const targetId = data.id;
            const player = this.state.players.get(targetId);
            if (!player) return;

            let hp = player.currentHP - data.value;

            if (hp > 0) {
                player.currentHP = hp;
                return;
            }

            player.loss += 1;
            player.currentHP = player.maxHP;

            const x = player.spX;
            const y = player.spY;
            const z = player.spZ;

            const message = JSON.stringify({ x, y, z });

            for (const cln of this.clients) {
                if (cln.sessionId === targetId) {
                    cln.send("Respawn", message);
                    break;
                }
            }
        });
      }

    onAuth(client, options, req) {
        return true;
    }

    onJoin(client: Client, data: any) {
        if (!this.spawnPointsReady) {
          this.waitingClients.push({ client, data });
          return;
        }
    
        this.spawnPlayer(client, data);
      }

    onLeave(client: Client) {
        const player = this.state.players.get(client.sessionId);
        this.state.removePlayer(client.sessionId);
    }

    onDispose () {
        console.log("Dispose StateHandlerRoom");
    }

    spawnPlayer(client: Client, data: any) {
        const spawn = this.getFreeSpawnPoint();
    
        const player = new Player();
        player.pX = spawn.x;
        player.pY = spawn.y;
        player.pZ = spawn.z;
        player.spX = spawn.x;
        player.spY = spawn.y;
        player.spZ = spawn.z;
    
        player.speed = data.speed ?? 5;
        player.maxHP = data.hp ?? 100;
        player.currentHP = player.maxHP;
    
        this.state.players.set(client.sessionId, player);
    
        if (this.clients.length >= 2) this.lock();
    }

    getFreeSpawnPoint(): SpawnPoint {
        const free = this.allSpawnPoints.filter((_, i) => !this.usedSpawnIndexes.has(i));
    
        if (free.length === 0) {
          console.warn("[SERVER] No free spawn points left!");
          return { x: 0, y: 0, z: 0 };
        }
    
        const index = Math.floor(Math.random() * free.length);
        const spawn = free[index];
    
        this.usedSpawnIndexes.add(index);
        return spawn;
    }
}
