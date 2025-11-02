using System;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public class UnitHealth
    {
        public int MaxHealth { get; }
        public int HealthPoints
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Math.Clamp(value, 0, MaxHealth);

                OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
                if (_currentHealth <= 0)
                    OnDying?.Invoke();
            }
        }

        public event Action<int, int> OnHealthChanged;
        public event Action OnDying;

        private int _currentHealth;

        public UnitHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            _currentHealth = maxHealth;
        }
    }
}