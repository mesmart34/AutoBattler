using System;

namespace Common
{
    [Serializable]
    public class PlayerState
    {
        public int health;
        public int money;
        public int currentMapProgress;
        public int mapRowSelectedIndex;
    }
}