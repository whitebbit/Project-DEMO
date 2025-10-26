using System.Collections.Generic;
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

        [SerializeField] private PlayerUnit playerPrefab;
        [SerializeField] private EnemyUnit enemyPrefab;

        #endregion

        #region FIELDS

        private ColyseusRoom<State> _room;
        private Dictionary<string, EnemyUnit> _enemies = new();

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
            var data = new Dictionary<string, object>
            {
                { "speed", playerPrefab.GetComponent<UnitMovement>().Speed },
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
            var position = new Vector3(player.pX, player.pY, player.pZ);
            Instantiate(playerPrefab, position, Quaternion.identity);
        }

        private void CreateEnemy(string key, Player player)
        {
            var position = new Vector3(player.pX, player.pY, player.pZ);
            var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

            enemy.Initialize(player);

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