using UnityEngine;

namespace _Game.Scripts.Units.Interfaces
{
    public interface IMovement
    {
        public float Speed { get; }
        public void Move(Vector3 position);
        public void SetMove(in Vector3 position, in Vector3 velocity, in float averageInterval);
        public void GetMoveInfo(out Vector3 position, out Vector3 velocity);
    }
}