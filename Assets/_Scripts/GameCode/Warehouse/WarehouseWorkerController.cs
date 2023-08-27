using System;
using GameCode.Elevator;
using GameCode.Worker;
using UniRx;
using UnityEngine;

namespace GameCode.Warehouse
{
    public class WarehouseWorkerController
    {
        private readonly WarehouseView _view;
        private readonly WarehouseModel _warehouseModel;
        private readonly WorkerModel _workerModel;
        private readonly ElevatorModel _elevatorModel;
        private readonly CompositeDisposable _disposable;

        private bool _busy;

        private WorkerView WorkerView => _view.WorkerView;

        public WarehouseWorkerController(WarehouseView view, WarehouseModel warehouseModel, WorkerModel workerModel,
            ElevatorModel elevatorModel, CompositeDisposable disposable)
        {
            _view = view;
            _warehouseModel = warehouseModel;
            _workerModel = workerModel;
            _elevatorModel = elevatorModel;
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
                    return _view.Positions.WorkerLoadPosition;
                case WorkerState.DROP:
                    return _view.Positions.WorkerDropOffPosition;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TriggerWorking(bool working)
        {
            _busy = working;
            if(!working)
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
                availableAmount = GetCurrentContainerResources(amount);
                _workerModel.CarryingAmount.Value += availableAmount;
            }
            else
            {
                availableAmount = _workerModel.CarryingAmount.Value;
                _workerModel.CarryingAmount.Value = 0;
                _warehouseModel.AddResource(availableAmount);
            }

            return availableAmount;
        }

        private double GetCurrentContainerResources(double amount)
        {
            return _elevatorModel.DrawResource(amount);
        }
    }
}