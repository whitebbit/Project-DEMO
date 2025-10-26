using System;
using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyMovement : UnitMovement
    {
        #region FIELDS SERIALIZED

        #endregion

        #region FIELDS

        public Vector3 TargetPosition { get; private set; } = Vector3.zero;
        private float _velocityMagnitude;

        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
            TargetPosition = transform.position;
        }

        #endregion

        #region METHODS

        public override float Speed { get; protected set; }
        public override Vector3 Velocity { get; protected set; }

        public void SetSpeed(float value) => Speed = value;

        public override void Move(Vector3 position)
        {
            var maxDistance = Time.deltaTime * _velocityMagnitude;
            transform.position = _velocityMagnitude > 0.1f
                ? Vector3.MoveTowards(transform.position, position, maxDistance)
                : Vector3.Lerp(transform.position, position, 20 * Time.deltaTime);
        }

        public override void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
        {
            TargetPosition = position + velocity * averageInterval;
            _velocityMagnitude = velocity.magnitude;
            
            Velocity = velocity;
        }

        public override void GetMoveInfo(out Vector3 position, out Vector3 velocity)
        {
            position = transform.position;
            velocity = Vector3.zero;
        }

        #endregion
    }
}