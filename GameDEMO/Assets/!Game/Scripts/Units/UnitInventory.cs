using _Game.Scripts.Weapons;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public class UnitInventory : MonoBehaviour
    {

        #region FIELDS SERIALIZED

        [SerializeField] private Weapon weapon;
        
        
        #endregion

        #region FIELDS

        public Weapon EquippedWeapon => weapon;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        #endregion

    }
}