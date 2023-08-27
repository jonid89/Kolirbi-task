using System;
using UnityEngine;

namespace GameCode.Mineshaft
{
    [Serializable]
    public class MineshaftPositions
    {
        [SerializeField] private Transform _workerLoadTransform;
        [SerializeField] private Transform _workerDropOffTransform;
        [SerializeField] private Transform _elevatorLoadTransform;

        public Vector2 WorkerLoadPosition => _workerLoadTransform.position;
        public Vector2 WorkerDropOffPosition => _workerDropOffTransform.position;
        public Vector2 ElevatorLoadPosition => _elevatorLoadTransform.position;
    }
}