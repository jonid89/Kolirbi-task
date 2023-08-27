using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.GameArea
{
    public class AreaUiCanvasView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _carryingCapacity;
        [SerializeField] private TMP_Text _upgradeCost;
        [SerializeField] private Button _upgradeButton;

        public string CarryingCapacity
        {
            set => _carryingCapacity.SetText(value);
        }

        public string UpgradeCost
        {
            set => _upgradeCost.SetText(value);
        }

        public Button UpgradeButton => _upgradeButton;
    }
}