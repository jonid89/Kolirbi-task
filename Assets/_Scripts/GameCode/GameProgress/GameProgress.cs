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
    private readonly CompositeDisposable _disposable;
    
    private int _numberOfMines;
    private int _currentMineID = 0;
    
    private int[] _elevatorLevels = new int[] {1,1};
    private int _elevatorLevel;

    private int _mineShaftsCount;
    private List<int> _mineShaftsPerMine = new List<int>();
    private List<List<int>> _minesLevels = new List<List<int>>();
    //private int[] _mineShaft1Levels = new int[] {1,1};
    private int _mineShaft1Level;

    public GameProgress(WarehouseModel warehouseModel, ElevatorModel elevatorModel, 
        MineshaftCollectionModel mineshaftCollectionModel, CompositeDisposable disposable)
    {
        _warehouseModel = warehouseModel;
        _elevatorModel = elevatorModel;
        _mineshaftCollectionModel = mineshaftCollectionModel;
        _disposable = disposable;
        
        //SubscribeToMineModels();
    }

    private void SubscribeToMineModels()
    {            
        /*_elevatorModel.Level.Subscribe(level => _elevatorLevel = level)
            .AddTo(_disposable);

        _mineshaftCollectionModel.GetModel(1).Level.Subscribe(level => _mineShaft1Level = level)
            .AddTo(_disposable);*/

    }

    public void SwitchToMine(int switchMineID)
    {
        SaveMine();
        
        _currentMineID = switchMineID;
        
        LoadMine();

        Debug.Log("Mine loaded: " + _currentMineID );
    }

    private void SaveMine()
    {
        _elevatorLevels[_currentMineID] = _elevatorModel.Level.Value;

        for (int i = 1; i == _mineshaftCollectionModel.GetCount(); i++)
        {
            int _mineShaftLevelSave = _mineshaftCollectionModel.GetModel(i).Level.Value;
            _minesLevels[_currentMineID].Insert(i, _mineShaftLevelSave);
        }
    }

    private void LoadMine()
    {
        _elevatorLevel = _elevatorLevels[_currentMineID];
        _elevatorModel.MineSwitch(_elevatorLevel);
        
        for (int i = 1; i == _mineshaftCollectionModel.GetCount(); i++)
        {
            int _mineShaftLevelLoad = _minesLevels[_currentMineID][0];
            _mineshaftCollectionModel.GetModel(1).MineSwitch(_mineShaftLevelLoad);
        }
    }
    
    private void InitializeMineLevelsList()
    {
        // Initialize _minesLevels with 1 mineshaft on level 1 for each mine
        for (int i = 0; i < _numberOfMines; i++)
        {
            _minesLevels.Add(new List<int>());
            _minesLevels[i].Add(1);
        }
    }
    
    public void SetMinesCount(int count)
    {
        _numberOfMines = count;
        InitializeMineLevelsList();
    }


}
