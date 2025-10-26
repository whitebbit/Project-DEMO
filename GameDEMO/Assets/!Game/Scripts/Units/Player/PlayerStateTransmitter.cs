using System;
using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Weapons;
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

        private MultiplayerManager _multiplayerManager;
        
        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
            _multiplayerManager = MultiplayerManager.Instance;
        }

        #endregion

        #region METHODS

        public void SendShoot(ref ShootInfo info)
        {
            info.key = _multiplayerManager.GetSessionID();
            var json = JsonUtility.ToJson(info);
            
            _multiplayerManager.SendMessage("shoot", json);
        }

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

            _multiplayerManager.SendMessage("move", data);
        }

        #endregion
    }
}