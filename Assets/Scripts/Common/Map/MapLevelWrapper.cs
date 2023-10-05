using System;
using System.Collections.Generic;
using Common.Enemy;
using UnityEngine.Serialization;

namespace Common.Map
{

    [Serializable]
    public class MapColumnWrapper
    {
        public List<MapLevelWrapper> levelColumn;
    }

    [Serializable]
    public class MapLevelWrapper
    {
        public LevelType levelType;
        public LevelConfiguration levelConfiguration;
    }
}