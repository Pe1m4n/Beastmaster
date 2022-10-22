﻿using System.Collections.Generic;
using Beastmaster.Core.Controllers;
using Beastmaster.Core.State;
using Beastmaster.Core.State.Fight;
using Beastmaster.Core.View;
using Zenject;

namespace Beastmaster.Core
{
    public class GameLoop : ITickable
    {
        private readonly IStateActionMediator _stateActionMediator;
        private readonly ViewsContainer _viewsContainer;
        private readonly FightStateContainer _fightStateContainer;
        private readonly PlayerController _playerController;
        private readonly FightInputContainer _fightInputContainer;

        private readonly List<ActionData> _actionsThisFrame = new List<ActionData>();
        private readonly ViewState _viewState;
        private readonly PlayerState _playerState;

        public GameLoop(IStateActionMediator stateActionMediator,
            ViewsContainer viewsContainer,
            FightStateContainer fightStateContainer,
            PlayerController playerController,
            FightInputContainer fightInputContainer)
        {
            _stateActionMediator = stateActionMediator;
            _viewsContainer = viewsContainer;
            _fightStateContainer = fightStateContainer;
            _playerController = playerController;
            _fightInputContainer = fightInputContainer;
            _playerState = new PlayerState(0, _fightStateContainer.State); //TODO: local player id getter
            _viewState = new ViewState(_fightStateContainer.State, _playerState);
        }
        
        public void Tick()
        {
            var state = _fightStateContainer.GetLatestState();
            _playerState.FightState = state;
            _viewState.FightState = state;
            
            _fightInputContainer.Tick();
            _playerController.Tick(_playerState, _actionsThisFrame);
            _viewsContainer.ApplyState(_viewState);
            _stateActionMediator.PerformActions(_actionsThisFrame);
            _actionsThisFrame.Clear();
        }
    }
}