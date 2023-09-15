using System.Linq;
using Common;
using Common.Enemy;
using Common.Unit;
using Common.Unit.Hero;
using Contracts;
using Installers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class UnitFactory
    {
        private readonly DiContainer _diContainer;

        private UnitConfiguration[] _unitConfigurations;
        private EnemyConfiguration[] _enemyConfigurations;
        private Object _heroPrefab;
        private Object _enemyPrefab;

        public UnitFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _unitConfigurations = Resources.LoadAll<UnitConfiguration>("Unit Configurations");
           // _enemyConfigurations = Resources.LoadAll<EnemyConfiguration>("Enemy Configurations");
            _heroPrefab = Resources.Load("Prefabs/Hero");
            //_enemyPrefab = Resources.Load("Prefabs/Enemy");
        }

        public HeroFacade CreateHero(UnitConfiguration unitConfiguration, Vector3 position, Transform parent)
        {
            var unitData = unitConfiguration.unitData;
            var unitFacade = _diContainer.InstantiatePrefabForComponent<HeroFacade>(_heroPrefab, parent);
            unitFacade.SetUnitData(new UnitData()
            {
                name = unitData.name,
                health = unitData.health,
                mana = unitData.mana,
                sprite = unitData.sprite,
                attackStrength = unitData.attackStrength,
                attackTimeout = unitData.attackTimeout,
                magicShield = unitData.magicShield,
                emissionMap = unitData.emissionMap,
                physicsShield = unitData.physicsShield,
                manaRegenerationAmount = unitData.manaRegenerationAmount
            });
            unitFacade.transform.position = position;
            return unitFacade;
        }
    }
}