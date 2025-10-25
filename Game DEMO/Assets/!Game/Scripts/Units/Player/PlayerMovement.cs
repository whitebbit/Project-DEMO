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

        private void SendMove()
        {
            var position = transform.position;

            var data = new Dictionary<string, object>
            {
                { "x", position.x },
                { "y", position.z },
            };

            MultiplayerManager.Instance.SendMessage("move", data);
        }

        #endregion
    }
}