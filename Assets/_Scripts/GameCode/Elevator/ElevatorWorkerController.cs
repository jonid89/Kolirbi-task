using System;
using GameCode.Mineshaft;
using GameCode.Worker;
using UniRx;
using UnityEngine;

namespace GameCode.Elevator
{
    public class ElevatorWorkerController
    {
        private readonly ElevatorView _elevatorView;
        private readonly ElevatorModel _elevatorModel;
        private readonly WorkerModel _workerModel;
        private readonly MineshaftCollectionModel _mineshaftCollectionModel;
        private readonly CompositeDisposable _disposable;

        private int _targetMineshaftNumber = 1;
        private bool _busy;

        private WorkerView WorkerView => _elevatorView.WorkerView;

        public ElevatorWorkerController(ElevatorView elevatorView, ElevatorModel elevatorModel, WorkerModel workerModel,
             MineshaftCollectionModel mineshaftCollectionModel, CompositeDisposable disposable)
        {
            _elevatorView = elevatorView;
            _elevatorModel = elevatorModel;
            _workerModel = workerModel;
            _mineshaftCollectionModel = mineshaftCollectionModel;
            _disposable = disposable;

            Observable.EveryUpdate()
                .Where(_ => !_busy)
                .Subscribe(Behave)
                .AddTo(disposable);

            workerModel.CarryingAmount.Subscribe(amount => WorkerView.CarryingAmount = amount.ToString("F0"))
                .AddTo(disposable);
        }

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
                    return _mineshaftCollectionModel.GetView(_targetMineshaftNumber).Positions.ElevatorLoadPosition;
                case WorkerState.DROP:
                    return _elevatorView.Positions.WorkerDropOffPosition;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TriggerWorking(bool working)
        {
            _busy = working;
            if (!working)
            {
                if(_workerModel.State == WorkerState.LOAD && (_workerModel.CarryingAmount.Value >= _workerModel.CarryingCapacity.Value || _targetMineshaftNumber >= _mineshaftCollectionModel.GetCount()))
                {
                    _workerModel.State = WorkerState.DROP;
                    _targetMineshaftNumber = 1;
                }
                else if (_workerModel.State == WorkerState.DROP)
                    _workerModel.State = WorkerState.LOAD;
                else
                    _targetMineshaftNumber++;
            }
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
                availableAmount = GetCurrentContainerResources(amount);
                _workerModel.CarryingAmount.Value += availableAmount;
            }
            else
            {
                availableAmount = _workerModel.CarryingAmount.Value;
                _workerModel.CarryingAmount.Value = 0;
                _elevatorModel.StashAmount.Value += availableAmount;
            }

            return availableAmount;
        }

        private double GetCurrentContainerResources(double amount)
        {
            return _mineshaftCollectionModel.GetModel(_targetMineshaftNumber).DrawResource(amount);
        }
    }
}