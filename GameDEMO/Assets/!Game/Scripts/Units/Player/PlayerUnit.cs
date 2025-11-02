using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerUnit : Unit
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitController controller;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override void Initialize(string id, global::Player player)
        {
            player.OnChange += controller.OnChange;
            controller.EquipWeapon(player.wI);
        }

        public override void Respawn(string respawnInfo)
        {
            var info = JsonUtility.FromJson<RespawnInfo>(respawnInfo);
            Debug.Log(info.ToVector3());

            StartCoroutine(RespawnCoroutine(info.ToVector3(transform.position.y)));
        }

        private IEnumerator RespawnCoroutine(Vector3 position)
        {
            Respawned = true;
            transform.position = position;
            
            yield return new WaitForSeconds(config.Respawn.Delay);
            
            Respawned = false;
        }

        #endregion
    }
}