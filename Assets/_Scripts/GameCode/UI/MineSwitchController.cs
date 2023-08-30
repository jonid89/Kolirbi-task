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
        /*_view.MinewSwitchButton.onClick.AddListener(OnButtonClicked);*/
    }

    
    private void AddButtonListeners()
    {
        for(int i = 0; i < _mineSwitchButtons.Count; i++)
        {
            _mineSwitchButtons[i].onClick.AddListener(() => OnButtonClicked(i));
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {
        // This method will be called when the button is Released
        _gameProgress.SwitchToMine(buttonIndex);
    }
    

}
