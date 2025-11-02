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

        public void SendTransform(Vector3 customPosition = default,  Vector2 customRotation = default, Vector3 customVelocity = default)
        {
            movement.GetMoveInfo(out var position, out var velocity);
            cameraLook.GetRotateInfo(out var rotation);

            var pos = customPosition == default ? position : customPosition;
            var rot = customRotation == default ? rotation : customRotation;
            var vel = customVelocity == default ? velocity : customVelocity;
            
            var data = new Dictionary<string, object>
            {
                { "pX", pos.x },
                { "pY", pos.y },
                { "pZ", pos.z },
                { "vX", vel.x },
                { "vY", vel.y },
                { "vZ", vel.z },
                { "rX", rot.x },
                { "rY", rot.y },
            };

            _multiplayerManager.SendMessage("move", data);
        }

        #endregion
    }
}