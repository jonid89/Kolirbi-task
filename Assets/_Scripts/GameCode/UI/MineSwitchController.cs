using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineSwitchController
{
    private readonly MineSwitchView _view;
    private readonly GameProgress _gameProgress;
    private List<Button> _mineSwitchButtons = new List<Button>();
    
    public MineSwitchView View => _view;

    public MineSwitchController(MineSwitchView view, GameProgress gameProgress)
    {
        _view = view;
        _gameProgress = gameProgress;
        
        _mineSwitchButtons = _view.MineSwitchButtons;
        AddButtonListeners();
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
        // This method is called when the button is Released
        _gameProgress.SwitchToMine(buttonIndex);
    }
    

}
