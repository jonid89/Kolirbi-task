using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.UI
{
    public class MineSwitchView : MonoBehaviour
    {
        [SerializeField] private float _animationSpeed = 5f;
        public float AnimationSpeed => _animationSpeed;
        
        [SerializeField] private Transform _initialPosition;
        [SerializeField] private Transform _openedPosition;
        public Vector2 InitialPosition => _initialPosition.position;
        public Vector2 OpenedPosition => _openedPosition.position;

        public Vector2 TransformPosition
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        [SerializeField] private List<Button> _mineSwitchButtons = new List<Button>();
        public List<Button> MineSwitchButtons => _mineSwitchButtons;

    }
}
