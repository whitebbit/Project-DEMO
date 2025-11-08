import { Room, Client } from "colyseus";
import { Schema, type, MapSchema } from "@colyseus/schema";

type Vector3 = { x: number; y: number; z: number };
type Vector2 = { x: number; y: number };

export class Vector3Schema extends Schema {
    @type("number")
    x = 0;

    @type("number")
    y = 0;

    @type("number")
    z = 0;

    set(vector: Vector3) {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }
}

export class Vector2Schema extends Schema {
    @type("number")
    x = 0;

    @type("number")
    y = 0;

    set(vector: Vector2) {
        this.x = vector.x;
        this.y = vector.y;
    }
}

export class Player extends Schema {
    
    @type(Vector3Schema)
    position = new Vector3Schema();

    @type(Vector3Schema)
    velocity = new Vector3Schema();

    @type(Vector2Schema)
    rotation = new Vector2Schema();

    @type("number")
    speed = 0;

    @type("int8")
    maxHP = 0;

    @type("int8")
    currentHP = 0;

    @type("uint8")
    loss = 0;

    @type("int8")
    wI = 0;
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
        
        if (data.position){
          var position = new Vector3Schema();

          position.x = data.position.x;
          position.y = data.position.y;
          position.z = data.position.z;

          player.position = position;
        }
        
        if (data.velocity){
          var velocity = new Vector3Schema();

          velocity.x = data.velocity.x;
          velocity.y = data.velocity.y;
          velocity.z = data.velocity.z;

          player.velocity = velocity;
        }
        
        if (data.rotation){
          var rotation = new Vector2Schema();

          rotation.x = data.rotation.x;
          rotation.y = data.rotation.y;

          player.rotation = rotation;
        }
    }
}

export class StateHandlerRoom extends Room<State> {
    allSpawnPoints: Vector3[] = [];
    usedSpawnIndexes: Set<number> = new Set();
    playersSpawns = new MapSchema<Vector3>();
    maxClients = 2;

    onCreate(options: any) {

        for (const spawnPoint of options.spawnPoints) {
          this.allSpawnPoints.push({ x: spawnPoint.x, y: spawnPoint.y, z: spawnPoint.z });
        }

        this.setState(new State());

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
            
            const spawn = this.playersSpawns.get(targetId);
            const message = JSON.stringify(spawn);

            for (const cln of this.clients) {
                if (cln.sessionId === targetId) {
                    cln.send("Respawn", message);
                    break;
                }
            }
        });

        this.onMessage("weapon_switch", (client, data) => {
            const player = this.state.players.get(client.sessionId);
            player.wI = data.wI;
          });
      }

    onAuth(client, options, req) {
        return true;
    }

    onJoin(client: Client, data: any) {
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

        this.playersSpawns.set(client.sessionId, spawn);

        const player = new Player();
        player.position.set(spawn);
    
        player.speed = data.speed ?? 5;
        player.maxHP = data.hp ?? 100;
        player.currentHP = player.maxHP;
    
        this.state.players.set(client.sessionId, player);
    
        if (this.clients.length >= 2) this.lock();
    }

    getFreeSpawnPoint(): Vector3 {
        const free = this.allSpawnPoints.filter((_, i) => !this.usedSpawnIndexes.has(i));
    
        if (free.length === 0) {
          return { x: 0, y: 0, z: 0 };
        }
    
        const index = Math.floor(Math.random() * free.length);
        const spawn = free[index];
    
        this.usedSpawnIndexes.add(index);
        return spawn;
    }
}
