using System;
using Code.Unit;
using Models;

namespace Scripts.Contracts
{
    public interface IEffect
    {
        public string Name { get; set; }
        public string IconPath { get; set; }

        public event Action<IEffect> OnEffectCooldown;
        public event Action<IEffect> OnEffectAdd;
        public event Action<IEffect> OnEffectRemove;

        public void Initialize(UnitModel model);
        public void Tick(UnitModel model);
        public void Dispose(UnitModel model);
    }
}