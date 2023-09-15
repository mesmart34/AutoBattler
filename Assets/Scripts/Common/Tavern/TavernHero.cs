using System;
using Common.Hero;
using UnityEngine;

namespace Common.Tavern
{
    [Serializable]
    public class TavernHero
    {
        public HeroConfiguration hero;
        public Transform spawnPoint;
    }
}