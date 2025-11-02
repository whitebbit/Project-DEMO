using System;
using System.Collections.Generic;
using _Game.Scripts.Controllers;
using _Game.Scripts.Controllers.Inputs;
using _Game.Scripts.Controllers.Interfaces;
using _Game.Scripts.Weapons;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerController : UnitController
    {
        #region FIELDS SERIALIZED

        [SerializeField] private Unit unit;
        [SerializeField] private UnitInventory inventory;
        [SerializeField] private UnitMovement movement;
        [SerializeField] private CameraLook cameraLook;
        [SerializeField] private PlayerStateTransmitter stateTransmitter;

        private IInput _input;

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
            movement.Move(new Vector3(_input.GetHorizontalAxis, 0, _input.GetVerticalAxis));
        }

        protected override void Update()
        {
            base.Update();
            
            if (_input.GetShootKeyDown && inventory.EquippedWeapon.TryShoot(out var info))
                stateTransmitter.SendShoot(ref info);
            
            stateTransmitter.SendTransform();
        }

        #endregion

        #region METHODS
        
        protected override void HandleMovement()
        {
            cameraLook.RotateX(-_input.GetMouseYAxis);
            cameraLook.RotateY(_input.GetMouseXAxis);

            if (_input.GetJumpKeyDown)
                movement.Jump();
        }

        public override void OnChange(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                unit.UnitHealth.HealthPoints = change.Field switch
                {
                    "currentHP" => Convert.ToSByte(change.Value),
                    _ => unit.UnitHealth.HealthPoints
                };
                
            }
        }

        #endregion
    }
}