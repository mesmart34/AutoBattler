using System.Linq;
using Common.Enemy;
using Common.Unit;
using Common.Unit.Enemy;
using Common.Unit.Hero;
using Installers;
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
            _unitConfigurations = Resources.LoadAll<UnitConfiguration>("Hero Configurations");
           // _enemyConfigurations = Resources.LoadAll<EnemyConfiguration>("Enemy Configurations");
            _heroPrefab = Resources.Load("Prefabs/Hero");
            _enemyPrefab = Resources.Load("Prefabs/Enemy");
        }

        public HeroFacade CreateHeroByName(string name, Vector3 position, Transform parent)
        {
            var unitConfiguration = _unitConfigurations.FirstOrDefault(x => x.unitData.name == name);
            return CreateHero(unitConfiguration, position, parent);
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
                manaRegenerationAmount = unitData.manaRegenerationAmount,
                locked = unitData.locked,
                invertSpriteHorizontally = unitData.invertSpriteHorizontally
            });
            unitFacade.transform.position = position;
            return unitFacade;
        }

        public EnemyFacade CreateEnemy(EnemyConfiguration unitEnemyConfiguration, Vector3 position, Transform parent)
        {
            var unitData = unitEnemyConfiguration.unitData;
            var data = new UnitData()
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
                manaRegenerationAmount = unitData.manaRegenerationAmount,
                locked = unitData.locked,
                invertSpriteHorizontally = unitData.invertSpriteHorizontally
            };
            var enemyFacade = _diContainer.InstantiatePrefabForComponent<EnemyFacade>(_enemyPrefab, parent);
            enemyFacade.SetUnitData(data);
            enemyFacade.transform.position = position;
            return enemyFacade;
        }
    }
}