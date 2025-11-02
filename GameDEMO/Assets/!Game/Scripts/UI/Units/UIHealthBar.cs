using System;
using _Game.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Units
{
    public class UIHealthBar : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private Unit unit;
        [SerializeField] private Slider slider;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
            UpdateHealth(unit.UnitHealth.HealthPoints, unit.UnitHealth.MaxHealth);
            unit.UnitHealth.OnHealthChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            unit.UnitHealth.OnHealthChanged -= UpdateHealth;
        }

        #endregion

        #region METHODS

        private void UpdateHealth(int currentHealth, int maxHealth)
        {
            var percent = currentHealth * 1f / maxHealth;

            slider.value = percent;
        }

        #endregion
    }
}