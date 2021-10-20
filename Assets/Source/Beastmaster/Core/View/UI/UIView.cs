using System;
using Beastmaster.Core.Configs;
using Beastmaster.Core.State;
using UnityEngine;
using UnityEngine.UIElements;

namespace Beastmaster.Core.View.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class UIView : MonoBehaviour
    {
        private Label _turnTimeLabel;
        private Label _leftNickLabel;
        private Label _rightNickLabel;

        private bool _initialized;
        
        private void Awake()
        {
            var uiDocument = GetComponent<UIDocument>();
            _turnTimeLabel = uiDocument.rootVisualElement.Q<Label>("TurnTimerLabel");
            _leftNickLabel = uiDocument.rootVisualElement.Q<Label>("Nickname_Left");
            _rightNickLabel = uiDocument.rootVisualElement.Q<Label>("Nickname_Right");
        }

        public void ApplyState(ViewState state)
        {
            if (!_initialized)
            {
                _leftNickLabel.text = state.FightState.FightConfig.LeftPlayerData.Nickname;
                _rightNickLabel.text = state.FightState.FightConfig.RightPlayerData.Nickname;
                _initialized = true;
            }
            
            var turnTimeLeft = state.FightState.Meta.TurnEnd - DateTime.Now;
            _turnTimeLabel.text = Mathf.CeilToInt((float)turnTimeLeft.TotalSeconds).ToString();
            
            PaintNickname(state, _leftNickLabel, state.FightState.FightConfig.LeftPlayerData);
            PaintNickname(state, _rightNickLabel, state.FightState.FightConfig.RightPlayerData);
            
        }

        private void PaintNickname(ViewState state, Label label, PlayerData playerData)
        {
            label.style.color = playerData.PlayerId == state.FightState.Meta.TurnForPlayer ? Color.red : Color.white;
        }
    }
}