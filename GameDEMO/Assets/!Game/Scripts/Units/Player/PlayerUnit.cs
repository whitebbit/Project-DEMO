using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Multiplayer;
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

        public override void Respawn(object data)
        {
            var spawnIndex = Convert.ToInt32(data);

            MultiplayerManager.Instance.Map.GetPoint(spawnIndex, out var position, out var rotation);
            StartCoroutine(RespawnCoroutine(position, rotation));
        }

        private IEnumerator RespawnCoroutine(Vector3 position, Vector3 rotation)
        {
            Respawned = true;
            
            transform.position = position;
            transform.eulerAngles = new Vector3(0, rotation.y, 0);

            yield return new WaitForSeconds(config.Respawn.Delay);

            Respawned = false;
        }

        #endregion
    }
}