using UniRx;

namespace GameCode.GameArea
{
    public interface IAreaModel
    {
        IReadOnlyReactiveProperty<bool> CanUpgrade { get; }
        IReadOnlyReactiveProperty<double> UpgradePrice { get; }
        IReadOnlyReactiveProperty<int> Level { get; }
        double SkillMultiplier { get; set; }
        void Upgrade();
    }
}