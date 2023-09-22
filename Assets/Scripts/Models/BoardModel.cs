using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Common;
using Common.Board;
using Common.Map;
using Common.Unit;
using Common.Unit.Enemy;
using Common.Unit.Hero;
using Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Models
{
    public class BoardModel : IInitializable
    {
        private List<HeroFacade> heroes;
        private List<EnemyFacade> enemies;
        private Dictionary<Vector2Int, PlatformFacade> heroPlatforms;
        private Dictionary<Vector2Int, PlatformFacade> enemyPlatforms;
        
        [Inject]
        private MapModel _mapModel;

        [Inject]
        private PlatformSpawner _platformSpawner;

        [Inject]
        private BoardSettings _boardSettings;

        [Inject]
        private UnitSpawner _unitSpawner;

        [Inject]
        private AsyncHandler _asyncHandler;
        
        [Inject]
        private LoadingScreenController _loadingScreenController;

        public void Initialize()
        {
            var level = _mapModel.GetCurrentLevel();
            if (level == null)
            {
                return;
            }
            _boardSettings.enemyNameText.text = level.levelConfiguration.name;
            _mapModel.Lock();
            
            heroPlatforms = _platformSpawner.SpawnPlatforms(false, _boardSettings.heroPlatformParent);
            heroes = _unitSpawner.SpawnHeroes(_boardSettings.playerBoardConfiguration, heroPlatforms, _boardSettings.heroesParent);

            //SetPlatformsVisibility(heroPlatforms, false);

            switch (level.levelType)
            {
                case LevelType.Enemy:
                    SetupBattle(level);
                    break;
                case LevelType.Trade:
                    break;
                case LevelType.Event:
                    break;
                case LevelType.Boss:
                    SetupBattle(level);
                    break;
            }
        }

        private void SetPlatformsVisibility(Dictionary<Vector2Int, PlatformFacade> platforms, bool visible)
        {
            foreach (var platform in platforms)
            {
                platform.Value.gameObject.SetActive(visible);
            }
        }

        private void SetupBattle(MapLevelWrapper level)
        {
            enemyPlatforms = _platformSpawner.SpawnPlatforms(true, _boardSettings.enemyPlatformParent);
            enemies = _unitSpawner.SpawnEnemies(level.levelConfiguration, enemyPlatforms, _boardSettings.enemyParent);
            
            var height = _boardSettings.beginBattlePanel.rect.height;
            _boardSettings.beginBattlePanel.DOAnchorPosY(-height, 0.25f);
        }

        public void Win()
        {
            OpenBattleResult(true);
            EndBattle();
        }

        public void Lose()
        {
            OpenBattleResult(false);
            EndBattle();
        }

        private void OpenBattleResult(bool win)
        {
            _boardSettings.battleResult.SetActive(true);
            var rect = _boardSettings.battleResult.GetComponent<RectTransform>();
            var startPosition = rect.anchoredPosition3D;
            var target = startPosition + new Vector3(0, -Screen.height * 0.5f, 0);
            rect.DOJumpAnchorPos(target, 1, 1, 0.25f);
            _boardSettings.battleResultText.text = win ? "Победа" : "Поражение";
        }
        
        public void EndBattle()
        {
            foreach (var hero in heroes)
            {
                hero.EndBattle();
            }
            
            foreach (var enemy in enemies)
            {
                enemy.EndBattle();
            }
        }

        private void MakeUnitsToFindTargets()
        {
            foreach (var hero in heroes)
            {
                hero.FindTarget();
            }
            
            foreach (var enemy in enemies)
            {
                enemy.FindTarget();
            }
        }
        
        public void BeginBattle()
        {
            _mapModel.Lock();
            SetPlatformsVisibility(enemyPlatforms, false);
            SetPlatformsVisibility(heroPlatforms, false);
            _boardSettings.beginBattlePanel.DOAnchorPosY(0, 0.25f);

            foreach (var hero in heroes)
            {
                hero.BeginBattle();
                hero.OnUnitDie += OnUnitDie;
            }
            
            foreach (var enemy in enemies)
            {
                enemy.BeginBattle();
                enemy.OnUnitDie += OnUnitDie;
            }
        }

        private void OnUnitDie()
        {
            var anyHeroes = heroes.Any(x => x.IsAlive);
            var anyEnemies = enemies.Any(x => x.IsAlive);
            if (anyHeroes && !anyEnemies)
            {
                Win();
            }
            else if(!anyHeroes && anyEnemies)
            {
                Lose();
            }

            MakeUnitsToFindTargets();
        }
        
        public UnitFacade FindTarget(UnitFacade unitFacade)
        {
            if (unitFacade is HeroFacade)
            {
                return enemies
                    .OrderBy(x => Vector3.Distance(x.Platform.transform.position, unitFacade.transform.position))
                    .FirstOrDefault(x => x.IsAlive);
            }
            return heroes
                .OrderBy(x => Vector3.Distance(x.Platform.transform.position, unitFacade.transform.position))
                .FirstOrDefault(x => x.IsAlive);
        }

        public void Next()
        {
            var rect = _boardSettings.battleResult.GetComponent<RectTransform>();
            var target = new Vector3(0, 0, 0);
            if (_mapModel.IsFinished())
            {
                _mapModel.GenerateLevelPipeline();
                _mapModel.GenerateViewForMap();
                _asyncHandler.StartCoroutine(LoadTavern());
            }
            rect.DOJumpAnchorPos(target, 1, 1, 0.25f).onComplete += () =>
            {
                _mapModel.MoveNext();
                _mapModel.Unlock();
                _mapModel.Open();
            };
        }

        private IEnumerator LoadTavern()
        {
            _loadingScreenController.Open();
            var loadSceneAsync = SceneManager.LoadSceneAsync("Scenes/TavernScene");
            var timer = 0.0f;
            while (!loadSceneAsync.isDone && timer < 2.0f)
            {
                timer += Time.deltaTime;
                if (loadSceneAsync.progress >= 0.9f)
                {
                    loadSceneAsync.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}