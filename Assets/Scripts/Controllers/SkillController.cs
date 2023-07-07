using System;
using System.Collections.Generic;
using Contracts;
using AutoBattler;
using UnityEngine;

namespace Controllers
{
    public class SkillController : MonoBehaviour
    {
        public List<SkillData> _skills;
        private Unit _unit;
        
        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void Start()
        {
            foreach (var skill in _skills)
            {
                skill.OnInitialize(_unit);
            }
        }
    }
}