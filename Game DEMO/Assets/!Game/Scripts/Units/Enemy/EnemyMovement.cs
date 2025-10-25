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

        public override void Move(Vector3 position)
        {
            var maxDistance = Time.deltaTime * _velocityMagnitude;
            transform.position = maxDistance > 0.1f
                ? Vector3.MoveTowards(transform.position, position, maxDistance)
                : position;
        }

        public override void SetMove(in Vector3 position, in Vector3 velocity, in float averageInterval)
        {
            TargetPosition = position + velocity * averageInterval;
            _velocityMagnitude = velocity.magnitude;
            Debug.Log($"{TargetPosition} - {position} - {velocity} - {averageInterval}");
        }

        public override void GetMoveInfo(out Vector3 position, out Vector3 velocity)
        {
            position = transform.position;
            velocity = Vector3.zero;
        }

        #endregion
    }
}