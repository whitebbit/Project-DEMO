using System;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public class UnitHitbox : MonoBehaviour, IHitVisitor
    {
        [SerializeField] private Unit unit;
        [SerializeField] private float damageMultiplier = 1f;

        public void Visit(DamageData damageData)
        {
            unit.ApplyDamage((int)Math.Floor(damageData.Damage * damageMultiplier));
        }
    }
}