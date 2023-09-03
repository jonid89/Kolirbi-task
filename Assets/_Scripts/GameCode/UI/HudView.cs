using TMPro;
using UnityEngine;

namespace GameCode.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cashAmount;
        [SerializeField] private GameObject _tooltip;
        [SerializeField] private TMP_Text _mineLabel;

        public double CashAmount
        {
            set => _cashAmount.SetText(value.ToString("F0"));
        }

        public bool TooltipVisible
        {
            set => _tooltip.gameObject.SetActive(value);
        }

        public double MineLabel
        {
            set => _mineLabel.SetText("Money" + value.ToString("F0") + " :");
        }
    }
}