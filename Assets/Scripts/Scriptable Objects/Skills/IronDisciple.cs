using System;
using Contracts;
using AutoBattler;
using UnityEngine;

namespace ScriptableObjects.Skills
{
    [CreateAssetMenu]
    public class IronDisciple : SkillData
    {
        public int ResistBonus = 10;
        public float Radius = 2.0f;


        public override void OnInitialize(Unit unit)
        {
            Board.Instance.GetUnits((x) => x.IsEnemy == unit.IsEnemy 
                                           && Vector3.Distance(unit.transform.position, x.transform.position) <= Radius );
            unit._healthController.PhysicalResist += ResistBonus;
        }
    }
}