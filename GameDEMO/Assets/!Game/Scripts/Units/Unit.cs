using System;
using _Game.Scripts.Units.Interfaces;
using _Game.Scripts.Units.Player;
using _Game.Scripts.Units.Scriptables;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        #region FIELDS SERIALIZED

        [SerializeField] protected UnitConfig config;
        [SerializeField] private UnitSkinLoader skinLoader;
        
        #endregion

        #region FIELDS

        protected UnitHealth Health;
        public UnitHealth UnitHealth => Health ??= new UnitHealth(config.Health.MaxHealth);

        public UnitSkinLoader SkinLoader => skinLoader;
        public UnitConfig Config => config;
        public bool Respawned { get; protected set; }

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public virtual void Initialize(string id, global::Player player)
        {
        }

        public virtual void Respawn(object data)
        {
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion

        public virtual void ApplyDamage(int damage)
        {
            if (Respawned) return;
            
            UnitHealth.HealthPoints -= damage;
        }
    }
}