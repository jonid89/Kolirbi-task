using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace GameCode.UI
{
    public class HudButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform _bottomImage;
        [SerializeField] private Transform _topImage;
        [SerializeField] private Button _button;
        public Vector2 BottomImagePosition
        {
            get => _bottomImage.position;
        }
        public Vector2 TopImagePosition
        {
            get => _topImage.position;
            set => _topImage.position = value;
        }
        public Button HudButton => _button;
        private readonly IReactiveProperty<bool> _buttonDown = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> ButtonDown => _buttonDown;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonDown.Value = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonDown.Value = false;
        }
    }
}