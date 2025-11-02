using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using _Game.Scripts.Weapons;

namespace _Game.Scripts.Units
{
    public class UnitInventory : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private List<Weapon> weapons = new();
        [SerializeField] private float switchDelay = 0.5f; 

        #endregion

        #region FIELDS

        public Weapon EquippedWeapon { get; private set; }
        public int CurrentIndex{ get; private set; }

        private bool _canSwitch = true;

        #endregion

        #region UNITY FUNCTIONS

        private void Start()
        {
            weapons.ForEach(w => w.gameObject.SetActive(false));
            EquipWeapon(0);
        }

        #endregion

        #region METHODS

        public void EquipWeapon(int index)
        {
            if (index < 0 || index >= weapons.Count)
                return;

            EquippedWeapon?.gameObject.SetActive(false);
            EquippedWeapon = weapons[index];
            EquippedWeapon.gameObject.SetActive(true);

            CurrentIndex = index;
        }

        public async void EquipNextWeapon()
        {
            if (!_canSwitch || weapons.Count <= 1)
                return;

            _canSwitch = false;

            var nextIndex = (CurrentIndex + 1) % weapons.Count;
            EquipWeapon(nextIndex);

            await Task.Delay(TimeSpan.FromSeconds(switchDelay));
            _canSwitch = true;
        }

        public async void EquipPreviousWeapon()
        {
            if (!_canSwitch || weapons.Count <= 1)
                return;

            _canSwitch = false;

            var prevIndex = (CurrentIndex - 1 + weapons.Count) % weapons.Count;
            EquipWeapon(prevIndex);

            await Task.Delay(TimeSpan.FromSeconds(switchDelay));
            _canSwitch = true;
        }

        #endregion
    }
}