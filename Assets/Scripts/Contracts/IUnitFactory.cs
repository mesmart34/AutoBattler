using Common;
using Common.Unit;
using UnityEngine;

namespace Contracts
{
    public interface IUnitFactory
    {
        public void Load();
        public UnitFacade Create(string prefabName, bool enemy, Transform boardPlatform);
    }
}