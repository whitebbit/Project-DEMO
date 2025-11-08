using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Multiplayer.Schemas;
using _Game.Scripts.Weapons;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyController : UnitController
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitInventory inventory;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyLook look;

        #endregion

        #region FIELDS

        private float AverageInterval => _receiveTimeIntervals.Sum() / _receiveTimeIntervals.Count;
        private readonly List<float> _receiveTimeIntervals = new() { 0, 0, 0, 0, 0 };
        private float _lastReceiveTime;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override void Shoot(in ShootInfo info)
        {
            inventory.EquippedWeapon.ShootByInfo(info);
        }

        private void SaveReceiveTime()
        {
            var interval = Time.time - _lastReceiveTime;
            _lastReceiveTime = Time.time;

            _receiveTimeIntervals.Add(interval);
            _receiveTimeIntervals.RemoveAt(0);
        }

        protected override void HandleMovement()
        {
            movement.Move(movement.TargetPosition);
        }

        public override void EquipWeapon(int index)
        {
            inventory.EquipWeapon(index);
        }

        public override void OnChange(List<DataChange> changes)
        {
            SaveReceiveTime();

            var position = movement.TargetPosition;
            var velocity = movement.Velocity;

            foreach (var change in changes)
            {
                switch (change.Field)
                {
                    case "position":
                        var positionValue = (Vector3Schema)change.Value;
                        position = positionValue.ToVector3();
                        break;
                    case "velocity":
                        var velocityValue = (Vector3Schema)change.Value;
                        velocity = velocityValue.ToVector3();
                        break;
                    case "rotation":
                        var rotationValue = (Vector2Schema)change.Value;
                        var rotation = rotationValue.ToVector2();

                        look.SetRotateX(rotation.x);
                        look.SetRotateY(rotation.y);
                        break;
                    case "currentHP":
                        if ((sbyte)change.Value > (sbyte)change.PreviousValue)
                            unit.UnitHealth.HealthPoints = Convert.ToSByte(change.Value);
                       
                        break;
                    case "loss":
                        MultiplayerManager.Instance.LossCounter.SetEnemyLoss((byte)change.Value);
                        break;
                    case "wI":
                        EquipWeapon(Convert.ToSByte(change.Value));
                        break;
                }
            }

            if (unit.Respawned)
                movement.Teleport(position);
            else
                movement.SetMovement(position, velocity, AverageInterval);
        }

        #endregion
    }
}