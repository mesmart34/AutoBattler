using System;
using System.Collections;
using AutoBattler;
using UnityEngine;

namespace Contracts
{
    public abstract class SkillData : ScriptableObject
    {
        public abstract void OnInitialize(Unit unit);
    }
}