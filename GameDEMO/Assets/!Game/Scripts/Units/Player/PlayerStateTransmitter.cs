using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.Multiplayer;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerStateTransmitter : MonoBehaviour
    {

        #region FIELDS SERIALIZED

        [SerializeField] private UnitMovement movement;
        [SerializeField] private CameraLook cameraLook;
        
        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public void SendTransform()
        {
            movement.GetMoveInfo(out var position, out var velocity);
            cameraLook.GetRotateInfo(out var rotation);
            
            var data = new Dictionary<string, object>
            {
                { "pX", position.x },
                { "pY", position.y },
                { "pZ", position.z },
                { "vX", velocity.x },
                { "vY", velocity.y },
                { "vZ", velocity.z },
                { "rX", rotation.x },
                { "rY", rotation.y },
            };

            MultiplayerManager.Instance.SendMessage("move", data);
        }

        #endregion

    }
}