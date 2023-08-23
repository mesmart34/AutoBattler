using Services;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class EffectController : MonoBehaviour, IInitializable
    {
        /*
        [SerializeField]
        public UnitInformationBar healthBar;
        
        [SerializeField]
        public UnitInformationBar manaBar;
        
        [SerializeField]
        public UnitInformationBar attackTimeoutBar;
        */

        [Inject]
        private HealthService _healthService;
        /*private UnitModel _unitModel;

        [Inject]
        public void Construct(UnitModel unitModel)
        {
            _unitModel = unitModel;
        }*/

        public void AddEffect()
        {
            
        }

        public void Initialize()
        {
            
        }
    }
}