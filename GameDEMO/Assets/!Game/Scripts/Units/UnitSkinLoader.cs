using _Game.Scripts.Units.Scriptables;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public class UnitSkinLoader : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private MeshRenderer[] renderers;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public void LoadSkin(UnitSkin skin)
        {
            foreach (var render in renderers)
            {
                render.material = skin.Material;
            }
        }

        #endregion
    }
}