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
        private readonly CompositeDisposable _disposable;        
        private IDisposable _moveSubscription;
        private Vector2 _initialTopImagePosition;
        private bool isMoving = false;

        public HudButtonController(HudButtonView hudButtonView, CompositeDisposable disposable)
        {
            _hudButtonView = hudButtonView;
            _disposable = disposable;

            _initialTopImagePosition = _hudButtonView.TopImagePosition;
            
            _hudButtonView.ButtonDown
                .Subscribe(_ => HandleButtonPress());
        }

        private void HandleButtonPress()
        {
            if (_hudButtonView.ButtonDown.Value)
            {
                ButtonPressed();
            }
            if (!_hudButtonView.ButtonDown.Value)
            {
                ButtonRelease();
            }
        }

        private void ButtonPressed()
        {
            isMoving = true;
            Vector2 targetPosition = _hudButtonView.BottomImagePosition;

            float speed = 5.0f;

            _moveSubscription = Observable.EveryUpdate()
                .TakeWhile(_ => isMoving)
                .Subscribe(_ =>
                {
                    _hudButtonView.TopImagePosition = Vector2.Lerp(_hudButtonView.TopImagePosition, targetPosition, Time.deltaTime * speed);
                });
        }

        public void ButtonRelease()
        {
            isMoving = false;
            
            //_moveSubscription.Dispose();

            _hudButtonView.TopImagePosition = _initialTopImagePosition;
        }
    }
}
