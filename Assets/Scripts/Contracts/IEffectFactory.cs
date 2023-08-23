using UnityEngine;

namespace Scripts.Contracts
{
    public interface IEffectFactory
    {
        public void Load();
        public void Create(string name, Transform effectBar);
    }
}