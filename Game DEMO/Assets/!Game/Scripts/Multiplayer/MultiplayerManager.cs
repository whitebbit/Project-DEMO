using System.Collections.Generic;
using _Game.Scripts.Units.Enemy;
using _Game.Scripts.Units.Player;
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

        private async void Connect()
        {
            _room = await Instance.client.JoinOrCreate<State>("state_handler");
            _room.OnStateChange += OnStateChange;
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

            player.OnChange += enemy.OnChange;
        }

        private void RemoveEnemy(string key, Player player)
        {
        }

        #endregion
    }
}