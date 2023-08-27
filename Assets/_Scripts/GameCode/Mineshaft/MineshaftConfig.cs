using UnityEngine;

namespace GameCode.Mineshaft
{
    [CreateAssetMenu(menuName = "Mineshaft Config")]
    public class MineshaftConfig : ScriptableObject, IMineshaftConfig
    {
        [SerializeField] private float _baseMineshaftCost = 500f;
        [SerializeField] private float _mineshaftCostIncrement = 12.5f;
        [SerializeField] private int _maxMineshafts = 10;
        [SerializeField] private MineshaftView _mineshaftPrefab;

        public float GetMineshaftCost(int mineshaftCount)
        {
            return _baseMineshaftCost * Mathf.Pow(_mineshaftCostIncrement, mineshaftCount - 1);
        }

        public float BaseMineshaftCost => _baseMineshaftCost;
        public float MineshaftCostIncrement => _mineshaftCostIncrement;
        public int MaxMineshafts => _maxMineshafts;
        public MineshaftView MineshaftPrefab => _mineshaftPrefab;
    }
}