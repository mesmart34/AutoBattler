using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Board
{
    public class PlatformFacade : MonoBehaviour
    {
        public UnitFacade unitFacade;
        public Vector2Int position;
        public bool _draggable;
        public bool IsFront { get; set; }

        public bool Busy { get; set; }

        public void Setup(Vector2Int position, bool draggable)
        {
            this.position = position;
            _draggable = draggable;
        }

        public void Clear()
        {
            unitFacade = null;
            Busy = false;
        }
        
        public void SetUnit(UnitFacade unit)
        {
            unitFacade = unit;
            unitFacade.Platform = this;
            Busy = true;
        }
    }
}