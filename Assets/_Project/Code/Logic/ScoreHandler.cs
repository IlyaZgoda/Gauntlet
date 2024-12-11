using Code.Services.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    public class ScoreHandler : MonoBehaviour
    {
        private CoreEventBus _eventBus;
        [SerializeField] private TMP_Text _scoreText;
        private string _text = "Score: ";
        private int _score = 0;

        [Inject]
        public void Construct(CoreEventBus eventBus) =>
            _eventBus = eventBus;

        private void Start()
        {
            _eventBus.EnemyDied += UpdateScore;
        }

        private void UpdateScore()
        {
            _score += 5;
            _scoreText.text = _text + _score;
        }

        private void OnDestroy()
        {
            _eventBus.EnemyDied -= UpdateScore;
        }

    }
}
