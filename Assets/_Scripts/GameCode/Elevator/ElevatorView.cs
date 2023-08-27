using System;
using GameCode.GameArea;
using GameCode.Worker;
using TMPro;
using UnityEngine;

namespace GameCode.Elevator
{
    public class ElevatorView : MonoBehaviour
    {
        [SerializeField] private WorkerView _workerView;
        [SerializeField] private AreaUiCanvasView _areaUiCanvasView;
        [SerializeField] private TMP_Text _stashAmount;
        [SerializeField] private ElevatorPositions _positions;
        
        public ElevatorPositions Positions => _positions;
        public WorkerView WorkerView => _workerView;
        public AreaUiCanvasView AreaUiCanvasView => _areaUiCanvasView;
        public string StashAmount
        {
            set => _stashAmount.SetText(value);
        }
    }

    [Serializable]
    public class ElevatorPositions
    {
        [SerializeField]
        private Transform _workerDropOffTransform;

        public Vector2 WorkerDropOffPosition => _workerDropOffTransform.position;
    }
}