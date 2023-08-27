using UnityEngine;

namespace GameCode.CameraRig
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _verticalLimit;
        [SerializeField] private float _tooltipDelay;

        public Vector2 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public float Speed => _speed;
        public Vector2 VerticalLimit => _verticalLimit;
        public float TooltipDelay => _tooltipDelay;
    }
}