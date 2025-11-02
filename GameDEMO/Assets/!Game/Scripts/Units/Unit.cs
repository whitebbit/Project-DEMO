using System;
using _Game.Scripts.Units.Interfaces;
using _Game.Scripts.Units.Scriptables;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitConfig config;

        #endregion

        #region FIELDS

        protected UnitHealth Health;
        public UnitHealth UnitHealth => Health ??= new UnitHealth(config.Health.MaxHealth);
        public UnitConfig Config => config;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public virtual void Initialize(string id, global::Player player)
        {
            
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion

        public virtual void ApplyDamage(int damage)
        {
            UnitHealth.HealthPoints -= damage;
        }
    }
}