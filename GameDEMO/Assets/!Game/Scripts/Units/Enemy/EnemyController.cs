using System.Collections.Generic;
using System.Linq;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyLook look;

        #endregion

        #region FIELDS

        public float AverageInterval => _receiveTimeIntervals.Sum() / _receiveTimeIntervals.Count;

        private readonly List<float> _receiveTimeIntervals = new() { 0, 0, 0, 0, 0 };
        private float _lastReceiveTime;

        #endregion

        #region UNITY FUNCTIONS

        private void Update()
        {
            movement.Move(movement.TargetPosition);
        }

        #endregion

        #region METHODS

        private void SaveReceiveTime()
        {
            var interval = Time.time - _lastReceiveTime;
            _lastReceiveTime = Time.time;

            _receiveTimeIntervals.Add(interval);
            _receiveTimeIntervals.RemoveAt(0);
        }

        public void OnChange(List<DataChange> changes)
        {
            SaveReceiveTime();

            var position = movement.TargetPosition;
            var velocity = movement.Velocity;

            foreach (var change in changes)
            {
                switch (change.Field)
                {
                    case "pX":
                        position.x = (float)change.Value;
                        break;
                    case "pY":
                        position.y = (float)change.Value;
                        break;
                    case "pZ":
                        position.z = (float)change.Value;
                        break;
                    case "vX":
                        velocity.x = (float)change.Value;
                        break;
                    case "vY":
                        velocity.y = (float)change.Value;
                        break;
                    case "vZ":
                        velocity.z = (float)change.Value;
                        break;
                    case "rX":
                        look.SetRotateX((float)change.Value);
                        break;
                    case "rY":
                        look.SetRotateY((float)change.Value);
                        break;
                }
            }

            movement.SetMovement(position, velocity, AverageInterval);
        }

        #endregion
    }
}