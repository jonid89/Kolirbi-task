using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.UI
{
    public class MineSwitchView : MonoBehaviour
    {
        [SerializeField] private List<Button> _mineSwitchButtons = new List<Button>();
        
        public List<Button> MineSwitchButtons => _mineSwitchButtons;
    }
}
