using System;
using Scripts.Common;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class BarController : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField]
        private Image _backgroundImage;
        
        [SerializeField]
        public UnitInformationBar healthBar;
        
        [SerializeField]
        public UnitInformationBar manaBar;
        
        [SerializeField]
        public UnitInformationBar attackTimeoutBar;

        [Inject]
        private HealthService _healthService;
        
        [Inject]
        private AttackService _attackService;
        
        [Inject]
        private ManaService _manaService;


        public void Initialize()
        {
            _healthService.OnHealthValueChanged += ChangeHealthBar;
            _attackService.OnAttackTimeoutValueChange += ChangeAttackBar;
            _manaService.OnManaValueChanged += ChangeManaBar;
        }
        
        private void ChangeHealthBar(int current, int max)
        {
            healthBar.ResizeBar(current / (float)max);   
        }

        private void ChangeAttackBar(float current, float max)
        {
            attackTimeoutBar.ResizeBar(current / max);      
        }
        
        private void ChangeManaBar(int current, int max)
        {
            manaBar.ResizeBar(current / (float)max);      
        }

        public void Dispose()
        {
            _healthService.OnHealthValueChanged -= ChangeHealthBar;
            _attackService.OnAttackTimeoutValueChange -= ChangeAttackBar;
            _manaService.OnManaValueChanged -= ChangeManaBar;
        }

        public void Hide()
        {
            _backgroundImage.enabled = false;
            SetChildrenActive(false);
        }

        public void Show()
        {
            _backgroundImage.enabled = true;
            SetChildrenActive(true);
        }
        
        private void SetChildrenActive(bool value)  
        {    
            foreach (Transform child in transform)     
            {  
                child.gameObject.SetActive(value);   
            }   
        }
    }
}