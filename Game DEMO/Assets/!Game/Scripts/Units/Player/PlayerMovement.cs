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
        
        private CharacterController _controller;

        #endregion

        #region UNITY FUNCTIONS
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        #endregion

        #region METHODS

        public override void Move(Vector3 position)
        {
            _controller.Move(position * (Speed * Time.deltaTime));
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