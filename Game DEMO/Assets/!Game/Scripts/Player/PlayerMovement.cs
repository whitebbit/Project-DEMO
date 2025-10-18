using System;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private float speed = 5;
        
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

        public void Move(float horizontal, float vertical)
        {
            var motion = new Vector3(horizontal, 0, vertical);
            _controller.Move(motion * speed * Time.deltaTime );
        }

        #endregion
    }
}