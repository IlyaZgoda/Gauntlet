using Code.Infrastructure.States;
using Code.Logic;
using Code.Services.Observable;
using Infrastructure.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.UI.HUD
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HPBar _bar;
        [SerializeField] private WaveNumberView _waveNumberView;
        [SerializeField] private GameOverView _gameOverView;
        private CoreEventBus _eventBus;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(CoreEventBus eventBus, IGameStateMachine gameStateMachine)
        {
            _eventBus = eventBus;
            _gameStateMachine = gameStateMachine;
        }
            

        private void Start()
        {
            _eventBus.PlayerHealthChanged += UpdateHPBar;
            _eventBus.WaveSpawned += UpdateWaveCounter;
            _eventBus.PlayerDied += EndGame;
        }
               

        private void UpdateHPBar(float value) =>
            _bar.SetValue(value);   

        private void UpdateWaveCounter(int value)
        {
            _waveNumberView.gameObject.SetActive(true);
            _waveNumberView.SetValue(value);
            StartCoroutine(_waveNumberView.WaitForDisableRoutine());
        }
            
        private void EndGame()
        {
            _gameOverView.Show();
            StartCoroutine(WaitForDisableGameOverRoutine());
        }

        public IEnumerator WaitForDisableGameOverRoutine()
        {
            yield return new WaitForSeconds(3);

            _gameOverView.gameObject.SetActive(false);
            _gameStateMachine.Enter<LoadMenuState>();
        }

        private void OnDestroy()
        {
            _eventBus.PlayerHealthChanged -= UpdateHPBar;
            _eventBus.WaveSpawned += UpdateWaveCounter;
            _eventBus.PlayerDied += EndGame;
        }         
    }
}
