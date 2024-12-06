using Code.Infrastructure;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public class SceneLoadingProgressView : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        private LoadingProgressPresenter _progress;

        [Inject]
        public void Construct(LoadingProgressPresenter progress) =>      
            _progress = progress;
        
        public void ShowProgress(float progress) =>      
            _progressSlider.value = progress;           
        
        private void Start() =>      
            _progress.OnProgress += ShowProgress;
        
        private void OnDestroy() =>
            _progress.OnProgress -= ShowProgress;
    }
}
