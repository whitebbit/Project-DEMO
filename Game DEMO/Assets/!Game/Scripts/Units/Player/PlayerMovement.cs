using System.Collections.Generic;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerMovement : UnitMovement
    {
        #region FIELDS SERIALIZED

        #endregion

        #region FIELDS

        private Rigidbody _rigidbody;

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region METHODS

        public override void Move(Vector3 input)
        {
            var velocity = (transform.forward * input.z + transform.right * input.x).normalized;
            _rigidbody.velocity = velocity * Speed;
            
            SendMove();
        }

        public override void GetMoveInfo(out Vector3 position, out Vector3 velocity)
        {
            position =  transform.position;
            velocity =  _rigidbody.velocity;
        }

        private void SendMove()
        {
            GetMoveInfo(out var position, out var velocity);

            var data = new Dictionary<string, object>
            {
                { "pX", position.x },
                { "pY", position.y },
                { "pZ", position.z },
                { "vX", velocity.x },
                { "vY", velocity.y },
                { "vZ", velocity.z },
            };

            MultiplayerManager.Instance.SendMessage("move", data);
        }

        #endregion
    }
}