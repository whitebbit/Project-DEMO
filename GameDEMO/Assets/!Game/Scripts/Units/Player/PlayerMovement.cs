using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerMovement : UnitMovement
    {
        #region FIELDS SERIALIZED

        [SerializeField] private float speed = 5;
        [SerializeField] private float jumpForce = 5;
        [SerializeField] private float jumpDelay = 0.2f;

        [SerializeField] private GroundChecker groundChecker;
        
        #endregion

        #region FIELDS

        public override float Speed
        {
            get => speed;
            protected set => speed = value;
        }
        public override  Vector3 Velocity { get; protected set; }

        private Rigidbody _rigidbody;
        private float _jumpTime;

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
            Velocity = GetVelocity(input);
            _rigidbody.velocity = Velocity;
        }

        private Vector3 GetVelocity(Vector3 input)
        {
            var vY = _rigidbody.velocity.y;
            var horizontal = (transform.forward * input.z + transform.right * input.x).normalized * speed;

            if (float.IsNaN(vY) || float.IsInfinity(vY))
                vY = 0f;

            return new Vector3(horizontal.x, vY, horizontal.z);
        }

        public override void Jump()
        {
            if (!groundChecker.IsGrounded) return;

            if (Time.time - _jumpTime < jumpDelay) return;

            _jumpTime = Time.time;
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }

        public override void GetMoveInfo(out Vector3 position, out Vector3 velocity)
        {
            position = transform.position;
            velocity = _rigidbody.velocity;
        }

        
        #endregion
    }
}