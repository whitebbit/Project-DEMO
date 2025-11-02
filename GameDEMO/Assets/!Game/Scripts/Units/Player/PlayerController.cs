using System;
using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.Controllers.Inputs;
using _Game.Scripts.Controllers.Interfaces;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Weapons;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerController : UnitController
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitInventory inventory;
        [SerializeField] private UnitMovement movement;
        [SerializeField] private CameraLook cameraLook;
        [SerializeField] private PlayerStateTransmitter stateTransmitter;

        private IInput _input;
        private Vector3 _moveDirection = Vector3.zero;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            _input = new DesktopInput();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            movement.Move(_moveDirection);
        }

        protected override void Update()
        {
            if (unit.Respawned)
            {
                SetMoveDirection(0, 0);

                cameraLook.RotateX(0);
                cameraLook.RotateY(0);

                stateTransmitter.SendTransform(customVelocity: Vector3.zero, customRotation: Vector2.zero);
                return;
            }

            base.Update();

            if (_input.GetShootKeyDown && inventory.EquippedWeapon.TryShoot(out var info))
                stateTransmitter.SendShoot(ref info);

            HandleWeaponSwitch();

            stateTransmitter.SendTransform();
        }

        #endregion

        #region METHODS

        protected override void HandleMovement()
        {
            SetMoveDirection(_input.GetHorizontalAxis, _input.GetVerticalAxis);

            cameraLook.RotateX(-_input.GetMouseYAxis);
            cameraLook.RotateY(_input.GetMouseXAxis);

            if (_input.GetJumpKeyDown)
                movement.Jump();
        }

        private void HandleWeaponSwitch()
        {
            var switchAxis = _input.GetWeaponSwitchAxis;
            
            switch (switchAxis)
            {
                case > 0f:
                    inventory.EquipNextWeapon();
                    break;
                case < 0f:
                    inventory.EquipPreviousWeapon();
                    break;
            }

            stateTransmitter.SendWeaponSwitch(inventory.CurrentIndex);
        }

        private void SetMoveDirection(float x, float z)
        {
            _moveDirection.x = x;
            _moveDirection.z = z;
        }

        public override void OnChange(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                switch (change.Field)
                {
                    case "currentHP":
                        unit.UnitHealth.HealthPoints = Convert.ToSByte(change.Value);
                        break;
                    case "loss":
                        MultiplayerManager.Instance.LossCounter.SetPlayerLoss((byte)change.Value);
                        break;
                    case "wI":
                        EquipWeapon(Convert.ToSByte(change.Value));
                        break;
                }
            }
        }

        #endregion
    }

    [Serializable]
    public struct RespawnInfo
    {
        public float x;
        public float z;

        public Vector3 ToVector3(float y = 0) => new Vector3(x, y, z);
    }
}