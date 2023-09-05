using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.UI
{
    public class MineSwitchView : MonoBehaviour
    {
        [SerializeField] private float _animationSpeed = 5f;
        [SerializeField] private Transform _initialPosition;
        [SerializeField] private Transform _openedPosition;
        [SerializeField] private List<Button> _mineSwitchButtons = new List<Button>();
        [SerializeField] private GameObject _darkOverlay;
        public float AnimationSpeed => _animationSpeed;
        public Vector2 InitialPosition => _initialPosition.position;
        public Vector2 OpenedPosition => _openedPosition.position;
        public Vector2 TransformPosition
        {
            get => transform.position;
            set => transform.position = value;
        }
        public List<Button> MineSwitchButtons => _mineSwitchButtons;
        public GameObject DarkOverlay => _darkOverlay;

    }
}
