using Code.Infrastructure.SceneManagement;
using Cysharp.Threading.Tasks;
using Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Code.Infrastructure.States
{
    public class MenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public MenuState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public UniTask Enter() =>
            default;
            
        public void OnLoaded() {}

        public UniTask Exit() =>
            default;      
    }
}
