using System;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "Player Configuration", menuName = "Autobattler/Player Configuration")]
    public class PlayerConfiguration : ScriptableObject
    {
        public int mapProgress;
        public int mapLevelInColumn;
        private int fightingSpirit = 10;
        private int money = 20;

        public void AddMoney(int amount)
        {
            money += amount;
            OnMoneyChanged?.Invoke(money);
        }

        public bool RemoveMoney(int amount)
        {
            if (money - amount < 0)
            {
                return false;
            }
            
            money -= amount;
            OnMoneyChanged?.Invoke(money);
            return true;
        }
        
        public void AddFightingSpirit(int amount)
        {
            fightingSpirit += amount;
            OnFightingSpiritChanged?.Invoke(fightingSpirit);
        }

        public bool RemoveFightingSpirit(int amount)
        {
            if (fightingSpirit - amount < 0)
            {
                return false;
            }
            
            fightingSpirit -= amount;
            OnFightingSpiritChanged?.Invoke(fightingSpirit);
            return true;
        }
        
        
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnFightingSpiritChanged;

    }
}