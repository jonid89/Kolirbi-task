using System;
using GameCode.Worker;
using UniRx;
using UnityEngine;

namespace GameCode.Mineshaft
{
    public class MineshaftWorkerController
    {
        private readonly MineshaftView _mineshaftView;
        private readonly MineshaftModel _mineshaftModel;
        private readonly WorkerModel _workerModel;
        private readonly CompositeDisposable _disposable;

        private bool _busy;

        public MineshaftWorkerController(MineshaftView mineshaftView, MineshaftModel mineshaftModel,
            WorkerModel workerModel, CompositeDisposable disposable)
        {
            _mineshaftView = mineshaftView;
            _mineshaftModel = mineshaftModel;
            _workerModel = workerModel;
            _disposable = disposable;

            Observable.EveryUpdate()
                .Where(_ => !_busy)
                .Subscribe(Behave)
                .AddTo(disposable);

            workerModel.CarryingAmount.Subscribe(amount => WorkerView.CarryingAmount = amount.ToString("F0"))
                .AddTo(disposable);
        }

        private WorkerView WorkerView => _mineshaftView.WorkerView;

        private void Behave(long tick)
        {
            var targetPosition = GetCurrentTargetPosition();

            if (targetPosition != WorkerView.Position)
            {
                Move();
                return;
            }

            TriggerWorking(true);

            Observable.Timer(TimeSpan.FromSeconds(PutToWork()))
                .Subscribe(_ => TriggerWorking(false))
                .AddTo(_disposable);
        }

        private void Move()
        {
            var position = WorkerView.Position;
            var destination = GetCurrentTargetPosition();
            position.x = Mathf.MoveTowards(position.x, destination.x, _workerModel.Speed * Time.deltaTime);
            position.y = Mathf.MoveTowards(position.y, destination.y, _workerModel.Speed * Time.deltaTime);
            WorkerView.Position = position;
        }

        private Vector2 GetCurrentTargetPosition()
        {
            switch (_workerModel.State)
            {
                case WorkerState.LOAD:
                    return _mineshaftView.Positions.WorkerLoadPosition;
                case WorkerState.DROP:
                    return _mineshaftView.Positions.WorkerDropOffPosition;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TriggerWorking(bool working)
        {
            _busy = working;
            if (!working)
                _workerModel.State = _workerModel.State == WorkerState.LOAD ? WorkerState.DROP : WorkerState.LOAD;
        }

        private double PutToWork()
        {
            var targetCapacity = _workerModel.CarryingCapacity.Value;
            var targetFreeCapacity = targetCapacity - _workerModel.CarryingAmount.Value;

            var amountTransferred = TransferResources(targetFreeCapacity);
            var effortMultiplier = amountTransferred / targetCapacity;

            return _workerModel.JobTime * effortMultiplier;
        }

        private double TransferResources(double amount)
        {
            double availableAmount;

            if (_workerModel.State == WorkerState.LOAD)
            {
                availableAmount = _workerModel.CarryingAmount.Value += amount;
            }
            else
            {
                availableAmount = _workerModel.CarryingAmount.Value;
                _workerModel.CarryingAmount.Value = 0;
                _mineshaftModel.StashAmount.Value += availableAmount;
            }

            return availableAmount;
        }
    }
}