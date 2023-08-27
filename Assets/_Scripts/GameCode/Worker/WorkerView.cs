using TMPro;
using UnityEngine;

namespace GameCode.Worker
{
    public class WorkerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _carryingAmount;

        public string CarryingAmount
        {
            set => _carryingAmount.SetText(value);
        }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}