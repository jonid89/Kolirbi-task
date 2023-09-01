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
    //private MineSwitchController _mineSwitchController;
    private readonly CompositeDisposable _disposable;
    
    private int _currentMineID = 0;
    private int[] _mineElevatorLevels = new int[] {1,1};
    
    private int _elevatorLevel;

    public GameProgress(WarehouseModel warehouseModel, ElevatorModel elevatorModel, 
        MineshaftCollectionModel mineshaftCollectionModel, CompositeDisposable disposable)
    {
        _warehouseModel = warehouseModel;
        _elevatorModel = elevatorModel;
        _mineshaftCollectionModel = mineshaftCollectionModel;
        //_mineSwitchController = mineSwitchController;
        _disposable = disposable;
        
        SubscribeToMineModels();
    }

    private void SubscribeToMineModels()
        {            
            _elevatorModel.Level.Subscribe(level => _elevatorLevel = level)
                .AddTo(_disposable);

            /*_mineSwitchController.View.MinewSwitchButton.OnClickAsObservable()
                    .Subscribe(_ => SetElevatorLevel(1) )
                    .AddTo(_disposable);*/
        }

    public void SwitchToMine(int switchMineID)
        {
            Debug.Log("_elevatorLevel before: " +_elevatorLevel);
            //Save
            _mineElevatorLevels[_currentMineID] = _elevatorLevel;
            
            //SetMineID
            _currentMineID = switchMineID;
            
            //Load
            _elevatorLevel = _mineElevatorLevels[_currentMineID];
            SetElevatorLevel(_elevatorLevel);
            Debug.Log("_elevatorLevel after: " +_elevatorLevel);
        }

    public void SetElevatorLevel(int levelToSet)
    {
        //Debug.Log("Previous Elevator Level:" + _elevatorLevel);
        
        _elevatorModel.MineSwitch(levelToSet);
    }

    

}
