namespace GameCode.Mineshaft
{
    public interface IMineshaftConfig
    {
        float GetMineshaftCost(int mineshaftCount);
        int MaxMineshafts { get; }
        float BaseMineshaftCost { get; }
        float MineshaftCostIncrement { get; }
        MineshaftView MineshaftPrefab { get; }
    }
}