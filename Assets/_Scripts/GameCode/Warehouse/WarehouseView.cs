using GameCode.GameArea;
using GameCode.Worker;
using UnityEngine;

namespace GameCode.Warehouse
{
    public class WarehouseView : MonoBehaviour
    {
        [SerializeField] private WorkerView _workerView;
        [SerializeField] private AreaUiCanvasView _areaUiCanvasView;
        [SerializeField] private WarehousePositions _positions;

        public WarehousePositions Positions => _positions;
        public WorkerView WorkerView => _workerView;
        public AreaUiCanvasView AreaUiCanvasView => _areaUiCanvasView;
    }
}