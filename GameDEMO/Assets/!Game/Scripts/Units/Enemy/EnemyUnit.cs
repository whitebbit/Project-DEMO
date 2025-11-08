using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Multiplayer;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyUnit : Unit
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitController controller;
        [SerializeField] private EnemyMovement movement;

        #endregion

        #region FIELDS

        public UnitController Controller => controller;
        private global::Player _player;
        private string _id;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override void Initialize(string id, global::Player player)
        {
            _id = id;
            _player = player;

            Health = new UnitHealth(player.maxHP);
            movement.SetSpeed(_player.speed);

            Health.OnDying += () => Respawn(null);
            _player.OnChange += controller.OnChange;
        }

        public override void Destroy()
        {
            _player.OnChange -= controller.OnChange;
            base.Destroy();
        }

        public override void ApplyDamage(int damage)
        {
            if (Respawned) return;

            base.ApplyDamage(damage);

            var data = new Dictionary<string, object>
            {
                { "id", _id },
                { "value", damage }
            };

            MultiplayerManager.Instance.SendMessage("damage", data);
        }

        public override void Respawn(object data)
        {
            StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine()
        {
            yield return null;

            Respawned = true;

            yield return new WaitForSeconds(config.Respawn.Delay);

            Respawned = false;
        }

        #endregion
    }
}