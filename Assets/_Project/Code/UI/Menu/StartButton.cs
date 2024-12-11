using Code.StaticData;
using Code.StaticData.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class StartButton : MonoBehaviour
    {
        private Button _button;

        public event Action<string> ClickEvent; 

        private void Awake()
        {
            if(TryGetComponent(out _button))           
                _button.onClick.AddListener(()
                    => ClickEvent?.Invoke(Scenes.Core));           
        }
    }
}
