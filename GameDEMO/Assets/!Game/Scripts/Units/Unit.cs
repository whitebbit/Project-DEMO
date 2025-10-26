using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class Unit : MonoBehaviour
    {

        #region FIELDS SERIALIZED

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public abstract void Initialize(global::Player player);

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
        
        #endregion

        
    }
}