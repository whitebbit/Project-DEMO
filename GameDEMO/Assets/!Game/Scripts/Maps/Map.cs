using _Game.Scripts.Units.Scriptables;
using UnityEngine;

namespace _Game.Scripts.Maps
{
    public class Map : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitSkin[] skins;
        [SerializeField] private Transform[] spawnPoints;

        #endregion

        #region FIELDS

        public int SpawnPointsCount => spawnPoints.Length;
        public int SkinsCount => skins.Length;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public void GetPoint(int index, out Vector3 position, out Vector3 rotation)
        {
            if (index >= SpawnPointsCount)
            {
                position = Vector3.zero;
                rotation = Vector3.zero;
                return;
            }

            position = spawnPoints[index].position;
            rotation = spawnPoints[index].eulerAngles;
        }

        public UnitSkin GetSkin(int index)
        {
            return skins.Length <= 0 ? skins[0] : skins[index];
        }

        #endregion
    }
}