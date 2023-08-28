using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCode.Warehouse;
using GameCode.Elevator;
using GameCode.Mineshaft;
using UniRx;

public class GameProgress 
{
    private WarehouseModel _warehouseModel;
    private ElevatorModel _elevatorModel;
    private MineshaftCollectionModel _mineshaftCollectionModel;
    private MineSwitchController _mineSwitchController;
    private readonly CompositeDisposable _disposable;
    
    private int _elevatorLevel;

    public GameProgress(WarehouseModel warehouseModel, ElevatorModel elevatorModel, 
        MineshaftCollectionModel mineshaftCollectionModel, MineSwitchController mineSwitchController, CompositeDisposable disposable)
    {
        _warehouseModel = warehouseModel;
        _elevatorModel = elevatorModel;
        _mineshaftCollectionModel = mineshaftCollectionModel;
        _mineSwitchController = mineSwitchController;
        _disposable = disposable;

        Subscribe();
    }

    public void SetElevatorLevel(int elevatorLevel)
    {
        
    }

    private void Subscribe()
        {
            //_elevatorLevel = _elevatorModel.Level;
            
            _elevatorModel.Level.Subscribe(level => _elevatorLevel = level)
                .AddTo(_disposable);

            
            _mineSwitchController.View.MinewSwitchButton.OnClickAsObservable()
                    .Subscribe(_ => Debug.Log("Elevator Level:" + _elevatorLevel))
                    .AddTo(_disposable);

        }
}
