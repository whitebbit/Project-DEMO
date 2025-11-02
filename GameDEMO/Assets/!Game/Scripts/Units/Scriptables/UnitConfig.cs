using System;
using UnityEngine;

namespace _Game.Scripts.Units.Scriptables
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/Unit Config", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField] private HealthAttributes healthAttributes;
        public HealthAttributes Health => healthAttributes;

        [SerializeField] private MovementAttributes movementAttributes;
        public MovementAttributes Movement => movementAttributes;
        
        [SerializeField] private RespawnAttributes respawnAttributes;
        public RespawnAttributes Respawn => respawnAttributes;
    }

    [Serializable]
    public class HealthAttributes
    {
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
    }

    [Serializable]
    public class MovementAttributes
    {
        [SerializeField] private float speed;
        public float Speed => speed;
        [SerializeField] private float jumpForce = 5;
        public float JumpForce => jumpForce;
        [SerializeField] private float jumpDelay = 0.2f;
        public float JumpDelay => jumpDelay;
    }
    
    [Serializable]
    public class RespawnAttributes
    {
        [SerializeField] private float delay = 3;
        public float Delay => delay;
    }
}