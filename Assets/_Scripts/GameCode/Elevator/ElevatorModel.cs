using GameCode.Finance;
using GameCode.GameArea;
using GameCode.Init;
using UniRx;
using UnityEngine;

namespace GameCode.Elevator
{
    public class ElevatorModel : IAreaModel
    {
        private const double BasePrice = 60;
        private readonly GameConfig _config;
        private readonly FinanceModel _financeModel;
        private readonly IReactiveProperty<double> _upgradePrice;
        private readonly IReactiveProperty<int> _level;

        //MineSwitching bool to determine when mine switching is happening
        private readonly IReactiveProperty<bool> _mineSwitching; 
        public IReadOnlyReactiveProperty<bool> MineSwitching => _mineSwitching;

        public ElevatorModel(int level, GameConfig config, FinanceModel financeModel, CompositeDisposable disposable)
        {
            _config = config;
            _financeModel = financeModel;

            _level = new ReactiveProperty<int>(level);
            StashAmount = new ReactiveProperty<double>();
            SkillMultiplier = Mathf.Pow(_config.ActorSkillIncrementPerShaft, 1) * Mathf.Pow(_config.ActorUpgradeSkillIncrement, _level.Value - 1);
            _upgradePrice = new ReactiveProperty<double>(BasePrice * Mathf.Pow(_config.ActorUpgradePriceIncrement, _level.Value - 1));
            CanUpgrade = _financeModel.Money
                .Select(money => money >= _upgradePrice.Value)
                .ToReadOnlyReactiveProperty()
                .AddTo(disposable);
            
            _mineSwitching = new ReactiveProperty<bool>(false);
        }

        public double SkillMultiplier { get; set; }
        public IReadOnlyReactiveProperty<bool> CanUpgrade { get; }
        public IReactiveProperty<double> StashAmount { get; }
        public IReadOnlyReactiveProperty<double> UpgradePrice => _upgradePrice;
        public IReadOnlyReactiveProperty<int> Level => _level;

        public void Upgrade()
        {
            if (_financeModel.Money.Value < _upgradePrice.Value) return;
            
            Debug.Log("Elevator Level: "+_level.Value);

            SkillMultiplier *= _config.ActorUpgradeSkillIncrement;
            var upgradePrice = _upgradePrice.Value;
            _upgradePrice.Value *= _config.ActorUpgradePriceIncrement;
            _financeModel.DrawResource(upgradePrice);
            _level.Value++;
        }
        
        public double DrawResource(double amount)
        {
            var result = 0d;
            if (StashAmount.Value <= amount)
            {
                result = StashAmount.Value;
                StashAmount.Value = 0;
            }
            else
            {
                result = amount;
                StashAmount.Value -= amount;
            }

            return result;
        }

        public void MineSwitch(int level)
        {
            _mineSwitching.Value = true;
            
            SkillMultiplier = Mathf.Pow(_config.ActorSkillIncrementPerShaft, 1) * Mathf.Pow(_config.ActorUpgradeSkillIncrement, level - 1);
            _upgradePrice.Value = BasePrice * Mathf.Pow(_config.ActorUpgradePriceIncrement, level - 1);
            _level.Value = level;
            StashAmount.Value = 0;

            _mineSwitching.Value = false;
            
        }

    }
}