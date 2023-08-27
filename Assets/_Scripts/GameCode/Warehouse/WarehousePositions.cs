using System;
using UnityEngine;

namespace GameCode.Warehouse
{
    [Serializable]
    public class WarehousePositions
    {
        [SerializeField] private Transform _workerDropOffTransform;
        [SerializeField] private Transform _workerLoadTransform;

        public Vector2 WorkerLoadPosition => _workerLoadTransform.position;
        public Vector2 WorkerDropOffPosition => _workerDropOffTransform.position;
    }
}