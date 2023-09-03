﻿using UniRx;
using UnityEngine;

namespace GameCode.Finance
{
    public class FinanceModel
    {
        private readonly IReactiveProperty<double> _money;
        public IReadOnlyReactiveProperty<double> Money => _money;        

        private double _initialMoneyPerMine;
        public double InitialMoneyPerMine => _initialMoneyPerMine;

        public FinanceModel(double initialMoneyPerMine)
        {
            _initialMoneyPerMine = initialMoneyPerMine;
            _money = new ReactiveProperty<double>(initialMoneyPerMine);
        }

        public void AddResource(double amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Received negative amount to add to inventory!");
                return;
            }

            _money.Value += amount;
        }

        public double DrawResource(double amount)
        {
            var result = 0d;
            if (_money.Value <= amount)
            {
                result = _money.Value;
                _money.Value = 0;
            }
            else
            {
                result = amount;
                _money.Value -= amount;
            }

            return result;
        }
    }
}