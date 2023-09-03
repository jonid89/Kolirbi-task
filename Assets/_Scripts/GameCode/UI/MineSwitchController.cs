using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.UI
{
    public class MineSwitchController
    {
        private readonly MineSwitchView _view;
        private readonly GameProgress _gameProgress;
        private readonly HudController _hudController;
        private List<Button> _mineSwitchButtons = new List<Button>();
        
        public MineSwitchView View => _view;

        public MineSwitchController(MineSwitchView view, GameProgress gameProgress, HudController hudController)
        {
            _view = view;
            _gameProgress = gameProgress;
            _hudController = hudController;
            
            _mineSwitchButtons = _view.MineSwitchButtons;
            AddButtonListeners();
            _gameProgress.SetMinesCount(_mineSwitchButtons.Count);
        }

        private void AddButtonListeners()
        {
            for(int i = 0; i < _mineSwitchButtons.Count; i++)
            {
                int buttonIndex = i;
                _mineSwitchButtons[i].onClick.AddListener(() => OnButtonClicked(buttonIndex));
            }
        }

        private void OnButtonClicked(int buttonIndex)
        {
            _gameProgress.SwitchToMine(buttonIndex);
            _hudController.SetMineID(buttonIndex);
        }
    }
}
