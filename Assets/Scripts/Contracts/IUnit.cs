using AutoBattler;

namespace Contracts
{
    public interface IUnit
    {
        public string Name { get; }
        public bool IsAlive { get; }
        public void Attack();
        public void GetDamage(Damage damage);
    }
}