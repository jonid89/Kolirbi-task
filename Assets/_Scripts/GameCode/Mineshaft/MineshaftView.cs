using GameCode.GameArea;
using GameCode.Worker;
using TMPro;
using UnityEngine;

namespace GameCode.Mineshaft
{
    public class MineshaftView : MonoBehaviour
    {
        [SerializeField] private WorkerView _workerView;
        [SerializeField] private NextMineShaftView _nextShaftView;
        [SerializeField] private AreaUiCanvasView _areaUiCanvasView;
        [SerializeField] private MineshaftPositions _positions;
        [SerializeField] private TMP_Text _stashAmount;

        public WorkerView WorkerView => _workerView;
        public NextMineShaftView NextShaftView => _nextShaftView;
        public AreaUiCanvasView AreaUiCanvasView => _areaUiCanvasView;
        public MineshaftPositions Positions => _positions;

        public string StashAmount
        {
            set => _stashAmount.SetText(value);
        }
    }
}