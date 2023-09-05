using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace GameCode.UI
{
    public class HudButtonController
    {
        private readonly HudButtonView _hudButtonView;
        private readonly MineSwitchController _mineswitchPanelController;
        private readonly CompositeDisposable _disposable;        
        private IDisposable _moveSubscription;
        private Vector2 _initialTopImagePosition;
        private readonly IReactiveProperty<bool> _isMoving = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsMoving => _isMoving;
        private string _buttonTag = "Map";
        public string ButtonTag => _buttonTag;


        public HudButtonController(HudButtonView hudButtonView, MineSwitchController mineswitchPanelController, CompositeDisposable disposable)
        {
            _hudButtonView = hudButtonView;
            _mineswitchPanelController = mineswitchPanelController;
            _disposable = disposable;

            _initialTopImagePosition = _hudButtonView.TopImagePosition;
            
            _hudButtonView.ButtonDown
                .Skip(1)
                .Subscribe(buttonPressed => HandleButtonPress(buttonPressed))
                .AddTo(disposable);
        }

        private void HandleButtonPress(bool buttonPressed)
        {
            
            if (buttonPressed)
            {
                ButtonPressed();
            }
            else if (!buttonPressed)
            {
                ButtonReleased();
            }
        }

        private void ButtonPressed()
        {
            _isMoving.Value = true;
            Vector2 targetPosition = _hudButtonView.BottomImagePosition;

            float speed = 5.0f;

            _moveSubscription = Observable.EveryUpdate()
                .TakeWhile(_ => _isMoving.Value)
                .TakeWhile(_ => Vector2.Distance(_hudButtonView.TopImagePosition, targetPosition) > 0.1f)
                .Subscribe(_ =>
                {
                    _hudButtonView.TopImagePosition = Vector2.Lerp(_hudButtonView.TopImagePosition, targetPosition, Time.deltaTime * speed);
                })
                .AddTo(_disposable);
        }

        private void ButtonReleased()
        {
            _buttonTag = _hudButtonView.ButtonTag;
            
            _isMoving.Value = false;
            
            _mineswitchPanelController.OnButtonClicked(_buttonTag);

            _moveSubscription.Dispose();
            
            _hudButtonView.TopImagePosition = _initialTopImagePosition;
            
        }

    }
}
