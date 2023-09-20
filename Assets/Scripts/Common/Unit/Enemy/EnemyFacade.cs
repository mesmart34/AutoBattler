using Zenject;

namespace Common.Unit.Enemy
{
    public class EnemyFacade : UnitFacade
    {
        [InjectOptional]
        private UnitData _unitData;
    }
}