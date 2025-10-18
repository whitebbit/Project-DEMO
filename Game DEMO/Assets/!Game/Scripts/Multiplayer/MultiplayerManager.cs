using Colyseus;

namespace _Game.Scripts.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        #region FIELDS SERIALIZED

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
        }

        #endregion
    }
}