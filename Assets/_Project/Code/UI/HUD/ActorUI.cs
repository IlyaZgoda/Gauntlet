using Code.Services.Observable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.UI.HUD
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] HPBar _bar;
        private CoreEventBus _eventBus;

        [Inject]
        public void Construct(CoreEventBus eventBus) =>
            _eventBus = eventBus;

        private void Start() =>
            _eventBus.PlayerHealthChanged += UpdateHPBar;    

        private void UpdateHPBar(float value) =>
            _bar.SetValue(value);   

        private void OnDestroy() =>   
            _eventBus.PlayerHealthChanged -= UpdateHPBar;
        
    }
}
