using _Game.Scripts.Controllers.Inputs;
using _Game.Scripts.Controllers.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitMovement movement;

        private IInput _input;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            _input = new DesktopInput();
        }

        private void Update()
        {
            movement.Move(new Vector3(_input.GetHorizontalAxis, 0,  _input.GetVerticalAxis));
        }

        #endregion

        #region METHODS
        
        #endregion
    }
}