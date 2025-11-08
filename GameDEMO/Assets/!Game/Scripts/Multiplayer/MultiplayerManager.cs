using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Maps;
using _Game.Scripts.Multiplayer.Schemas;
using _Game.Scripts.UI;
using _Game.Scripts.Units;
using _Game.Scripts.Units.Enemy;
using _Game.Scripts.Units.Player;
using _Game.Scripts.Weapons;
using Colyseus;
using UnityEngine;

namespace _Game.Scripts.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        #region FIELDS SERIALIZED

        [field: SerializeField] public LossCounter LossCounter { get; private set; }
        [field: SerializeField] public Map Map { get; private set; }

        [SerializeField] private PlayerUnit playerPrefab;
        [SerializeField] private EnemyUnit enemyPrefab;

        #endregion

        #region FIELDS

        private ColyseusRoom<State> _room;
        private readonly Dictionary<string, EnemyUnit> _enemies = new();

        #endregion

        #region UNITY FUNCTIONS

        protected override void Awake()
        {
            base.Awake();
            Instance.InitializeClient();
            Connect();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _room.Leave();
        }

        #endregion

        #region METHODS

        public void SendMessage(string key, Dictionary<string, object> data)
        {
            _room.Send(key, data);
        }

        public void SendMessage(string key, string data)
        {
            _room.Send(key, data);
        }

        public string GetSessionID() => _room.SessionId;

        private async void Connect()
        {
            var spawnIndex = Random.Range(0, Map.SpawnPointsCount);
            Map.GetPoint(spawnIndex, out var position, out var rotation);

            var data = new Dictionary<string, object>
            {
                { "speed", playerPrefab.Config.Movement.Speed },
                { "hp", playerPrefab.Config.Health.MaxHealth },
                { "spawnsCount", Map.SpawnPointsCount },
                { "pos", position },
                { "rot", rotation },
                { "spawn", spawnIndex },
                { "skins", Map.SkinsCount },
            };

            _room = await Instance.client.JoinOrCreate<State>("state_handler", data);
            _room.OnStateChange += OnStateChange;
            _room.OnMessage<string>("Shoot", ApplyShoot);
        }

        private void OnStateChange(State state, bool isFirstState)
        {
            if (!isFirstState) return;

            state.players.ForEach((key, player) =>
            {
                if (key == _room.SessionId) CreatePlayer(player);
                else CreateEnemy(key, player);
            });

            _room.State.players.OnAdd += CreateEnemy;
            _room.State.players.OnRemove += RemoveEnemy;
        }

        private void CreatePlayer(Player player)
        {
            var rotation = Quaternion.Euler(0, player.rotation.y, 0);
            var unit = Instantiate(playerPrefab, player.position.ToVector3(), rotation);

            unit.Initialize("", player);
            unit.SkinLoader.LoadSkin(Map.GetSkin(player.sI));
            _room.OnMessage<object>("Respawn", unit.Respawn);
        }

        private void CreateEnemy(string key, Player player)
        {
            var enemy = Instantiate(enemyPrefab, player.position.ToVector3(), Quaternion.identity);

            enemy.Initialize(key, player);
            enemy.SkinLoader.LoadSkin(Map.GetSkin(player.sI));

            _enemies.Add(key, enemy);
        }

        private void RemoveEnemy(string key, Player player)
        {
            if (!_enemies.TryGetValue(key, out var enemy)) return;

            enemy.Destroy();
            _enemies.Remove(key);
        }

        private void ApplyShoot(string jsonShootInfo)
        {
            var info = JsonUtility.FromJson<ShootInfo>(jsonShootInfo);

            if (!_enemies.TryGetValue(info.key, out var enemy)) return;

            enemy.Controller.Shoot(info);
        }

        #endregion
    }
}