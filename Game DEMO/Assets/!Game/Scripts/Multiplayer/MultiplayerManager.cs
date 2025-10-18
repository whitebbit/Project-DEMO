using _Game.Scripts.Player;
using Colyseus;
using UnityEngine;

namespace _Game.Scripts.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        #region FIELDS SERIALIZED

        [SerializeField] private PlayerUnit player;
        [SerializeField] private Transform enemy;

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

        private async void Connect()
        {
            _room = await Instance.client.JoinOrCreate<State>("state_handler");
            _room.OnStateChange += OnStateChange;
        }

        private void OnStateChange(State state, bool isFirstState)
        {
            if (!isFirstState) return;

            var serverPlayer = state.players[_room.SessionId];
            var position = new Vector3(serverPlayer.x - 200, 0, serverPlayer.y - 200) / 8;

            var obj = Instantiate(player, position, Quaternion.identity);

            state.players.ForEach(ForEachEnemy);
        }

        private void ForEachEnemy(string key, global::Player player)
        {
            if (key == _room.SessionId) return;
            
            var position = new Vector3(player.x - 200, 0, player.y - 200) / 8;
            var obj = Instantiate(enemy, position, Quaternion.identity);
        }

        #endregion
    }
}