using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineSwitchController
{
    private readonly MineSwitchView _view;
    private Button myBotton;
    
    public MineSwitchView View => _view;

    public MineSwitchController(MineSwitchView view)
    {
        _view = view;
        
        _view.MinewSwitchButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // This method will be called when the button is Released
        

    }

}
