using System.Collections;
using UnityEngine;

namespace GameState
{
    public abstract class BaseState
    {
        public abstract void OnEnter();

        public abstract void OnExit();
    }
}