﻿using System.Collections.Generic;
using Common.Board;
using Factories;
using UnityEngine;
using Zenject;

namespace Common
{
    public class PlatformSpawner
    {
        private BoardPlatformSettings _boardPlatformSettings;
        
        [Inject]
        private PlatformFactory _platformFactory;

        public PlatformSpawner(BoardPlatformSettings boardPlatformSettings)
        {
            _boardPlatformSettings = boardPlatformSettings;
        }
        
        public Dictionary<Vector2Int, PlatformFacade>  SpawnPlatforms(bool invertSides, Transform parent)
        {
            Dictionary<Vector2Int, PlatformFacade> platforms = new();
            var spacing = _boardPlatformSettings.spacing;
            for (var x = 0; x < _boardPlatformSettings.boardPlayerSideSize.x; x++)
            {
                for (var y = 0; y < _boardPlatformSettings.boardPlayerSideSize.y; y++)
                {
                    var logicPos = new Vector2Int(x, y);
                    var pos = new Vector3(x * spacing + spacing,  0,
                        y * spacing + spacing) + _boardPlatformSettings.position;
                    var platform = _platformFactory.Create(logicPos, pos, invertSides, parent);
                    if (invertSides)
                    {
                        if (x == 0)
                        {
                            platform.IsFront = true;
                        }
                    }
                    else
                    {
                        if (x == _boardPlatformSettings.boardPlayerSideSize.x - 1)
                        {
                            platform.IsFront = true;
                        }
                    }

                    platform._draggable = !invertSides;
                    platforms.Add(logicPos, platform);
                }
            }

            return platforms;
        }
    }
}