using UnityEngine;

namespace _Game.Scripts.Units.Scriptables
{
    [CreateAssetMenu(fileName = "UnitSkin", menuName = "Configs/Unit Skin", order = 0)]
    public class UnitSkin : ScriptableObject
    {
        [SerializeField] private Material material;
        public Material Material => material;
    }
}