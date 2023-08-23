using UnityEngine;

namespace Scripts.Common.Unit
{
    [CreateAssetMenu(menuName = "Autobattler/Unit Configuration")]
    public class UnitConfiguration : ScriptableObject
    {
        public string name = "Unnamed";
        public bool isEnemy;
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