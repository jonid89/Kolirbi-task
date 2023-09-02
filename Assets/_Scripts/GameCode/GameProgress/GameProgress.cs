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
    
    private int[] _elevatorLevels = new int[] {1,1};
    private int _elevatorLevel;
    private int[] _mineShaft1Levels = new int[] {1,1};
    private int _mineShaft1Level;

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

            _mineshaftCollectionModel.GetModel(1).Level.Subscribe(level => _mineShaft1Level = level)
                .AddTo(_disposable);

        }

    public void SwitchToMine(int switchMineID)
        {
            Debug.Log("_mineShaft1Level before: " +_mineShaft1Level);
            //Save
            _elevatorLevels[_currentMineID] = _elevatorLevel;
            _mineShaft1Levels[_currentMineID] = _mineShaft1Level;
            
            //SetMineID
            _currentMineID = switchMineID;
            
            //Load
            _elevatorLevel = _elevatorLevels[_currentMineID];
            SetElevatorLevel(_elevatorLevel);
            _mineShaft1Level = _mineShaft1Levels[_currentMineID];
            _mineshaftCollectionModel.GetModel(1).MineSwitch(_mineShaft1Level);
            Debug.Log("_mineShaft1Level after: " +_mineShaft1Level);
        }

    public void SetElevatorLevel(int levelToSet)
    {
        //Debug.Log("Previous Elevator Level:" + _elevatorLevel);
        
        _elevatorModel.MineSwitch(levelToSet);
    }

    

}
