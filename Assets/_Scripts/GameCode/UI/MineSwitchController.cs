using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineSwitchController
{
    private readonly MineSwitchView _view;
    private Button myBotton;
    private GameProgress _gameProgress;

    public MineSwitchController(MineSwitchView view, GameProgress gameProgress)
    {
        _view = view;
        _gameProgress = gameProgress;
        
        _view.MinewSwitchButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // This method will be called when the button is Released
        Debug.Log("Elevator Level: " + _gameProgress.ElevatorLevel);

    }

    private void OnDestroy()
    {
        // Unsubscribe the listener when the script is destroyed to avoid memory leaks
        _view.MinewSwitchButton.onClick.RemoveListener(OnButtonClicked);
    }

}
