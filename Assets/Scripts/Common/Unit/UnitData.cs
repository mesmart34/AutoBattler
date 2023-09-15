using System;
using UnityEngine;

namespace Common.Unit
{
    [Serializable]
    public class UnitData
    {
        public string name = "Unnamed";
        public bool locked = true;
        
        [Header("Health and mana configuration")]
        public int health;
        public int physicsShield;
        public int magicShield;
        public int mana;
        public int manaRegenerationAmount;
        
        [Header("Attack configuration")]
        public float attackTimeout;
        public int attackStrength;
        
        [Header("Visuals")]
        public Sprite[] sprite;
        public Texture2D[] emissionMap;
    }
}