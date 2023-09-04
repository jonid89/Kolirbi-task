using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEditor.Search;

namespace GameCode.UI
{
    public class MineSwitchController
    {
        private readonly MineSwitchView _view;
        private readonly GameProgress _gameProgress;
        private readonly HudController _hudController;
        private readonly CompositeDisposable _disposable;   
        private IDisposable _moveSubscription;
        private List<Button> _mineSwitchButtons = new List<Button>();
        public MineSwitchView View => _view;

        public MineSwitchController(MineSwitchView view, GameProgress gameProgress, HudController hudController, CompositeDisposable disposable)
        {
            _view = view;
            _gameProgress = gameProgress;
            _hudController = hudController;
            _disposable = disposable;
            
            _mineSwitchButtons = _view.MineSwitchButtons;
            AddButtonListeners();
            _gameProgress.SetMinesCount(_mineSwitchButtons.Count);
        }

        private void AddButtonListeners()
        {
            for(int i = 0; i < _mineSwitchButtons.Count; i++)
            {
                int buttonIndex = i;
                _mineSwitchButtons[i].onClick.AddListener(() => MineSwitchedClicked(buttonIndex));
            }
        }

        private void MineSwitchedClicked(int buttonIndex)
        {
            _gameProgress.SwitchToMine(buttonIndex);
            _hudController.SetMineID(buttonIndex);
        }
        
        public void OnButtonClicked(string tag)
        {
            Debug.Log("tag: " + tag);
            if (tag == "Map")
            {
                OpenPanelAnimation();
            }
            else if (tag == "Close" || tag == "SwitchMine")
            {
                _view.TransformPosition = _view.InitialPosition;
                _moveSubscription.Dispose();
            }
        }


        private void OpenPanelAnimation()
        {
            _moveSubscription = Observable.EveryUpdate()
                .TakeWhile(_ => Vector2.Distance(_view.TransformPosition, _view.OpenedPosition) > 10f)
                .Subscribe(_ =>
                {
                    _view.TransformPosition = Vector2.Lerp(_view.TransformPosition, _view.OpenedPosition, Time.deltaTime * _view.AnimationSpeed);
                })
                .AddTo(_disposable);           
        }        
    }
}
