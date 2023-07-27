using System;
using System.Collections;
using AutoBattler;
using UnityEngine;

namespace Contracts
{
    public abstract class SkillData : ScriptableObject
    {
        public string Name;
        [TextArea]
        public string Description;
        
        public abstract void OnInitialize(Unit unit);
    }
}