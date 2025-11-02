using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class LossCounter : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private TMP_Text text;

        #endregion

        #region FIELDS

        private int _enemyLoss;
        private int _playerLoss;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public void SetEnemyLoss(int value)
        {
            _enemyLoss = value;
            UpdateText();
        }

        public void SetPlayerLoss(int value)
        {
            _playerLoss = value;
            UpdateText();
        }

        private void UpdateText()
        {
            text.text = $"{_playerLoss}:{_enemyLoss}";
        }

        #endregion
    }
}