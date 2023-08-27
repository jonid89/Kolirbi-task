using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.Mineshaft
{
    public class NextMineShaftView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Button _button;
        [SerializeField] private Transform _nextShaftPosition;

        public string Cost
        {
            set => _cost.SetText(value);
        }

        public Button Button => _button;
        public Vector2 NextShaftPosition => _nextShaftPosition.position;

        public bool Visible
        {
            set => gameObject.SetActive(value);
        }
    }
}