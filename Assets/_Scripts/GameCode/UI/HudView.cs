using TMPro;
using UnityEngine;

namespace GameCode.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cashAmount;
        [SerializeField] private GameObject _tooltip;

        public double CashAmount
        {
            set => _cashAmount.SetText(value.ToString("F0"));
        }

        public bool TooltipVisible
        {
            set => _tooltip.gameObject.SetActive(value);
        }
    }
}