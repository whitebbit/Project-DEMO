using UnityEngine;

namespace _Game.Scripts.Units.Interfaces
{
    public interface IMovement
    {
        public float Speed { get; }
        public void Move(Vector3 position);
    }
}