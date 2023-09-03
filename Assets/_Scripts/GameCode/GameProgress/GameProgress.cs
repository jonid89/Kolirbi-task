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
    private List<List<int>> _minesMineShaftsLevels = new List<List<int>>();
    //private int[] _mineShaft1Levels = new int[] {1,1};
    private int _mineShaft1Level;

    public GameProgress(WarehouseModel warehouseModel, ElevatorModel elevatorModel, 
        MineshaftCollectionModel mineshaftCollectionModel, CompositeDisposable disposable)
    {
        _warehouseModel = warehouseModel;
        _elevatorModel = elevatorModel;
        _mineshaftCollectionModel = mineshaftCollectionModel;
        _disposable = disposable;
        
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
        //Saving Elevator Level
        _elevatorLevels[_currentMineID] = _elevatorModel.Level.Value;

        //Iterate through List of mineshafts on Current Mine to save Active mineshafts Level
        for (int i = 1; i <= _mineshaftCollectionModel.GetCount(); i++)
        {
            if(_mineshaftCollectionModel.GetView(i).gameObject.activeSelf)
            {
                int _mineShaftLevelSave = _mineshaftCollectionModel.GetModel(i).Level.Value;
                _minesMineShaftsLevels[_currentMineID].Insert(i-1, _mineShaftLevelSave);
                _mineShaftsPerMine[_currentMineID] = i;                
            }
        }
    }

    private void LoadMine()
    {
        _elevatorLevel = _elevatorLevels[_currentMineID];
        _elevatorModel.MineSwitch(_elevatorLevel);
        
        for (int i = 1; i <= _mineshaftCollectionModel.GetCount(); i++)
        {
            if(i <= _mineShaftsPerMine[_currentMineID])
            {
                _mineshaftCollectionModel.GetView(i).gameObject.SetActive(true);
                int _mineShaftLevelLoad = _minesMineShaftsLevels[_currentMineID][i-1];
                Debug.Log("_mineShaftLevelLoad: " + _mineShaftLevelLoad);
                _mineshaftCollectionModel.GetModel(i).MineSwitch(_mineShaftLevelLoad);
                if(i == _mineShaftsPerMine[_currentMineID])
                {
                    _mineshaftCollectionModel.GetView(i).NextShaftView.gameObject.SetActive(true);
                }
            }
            else{
                _mineshaftCollectionModel.GetModel(i).MineSwitch(1);
                _mineshaftCollectionModel.GetView(i).gameObject.SetActive(false);
            }
        }
    }
    
    private void InitializeMineLevelsList()
    {
        // Initialize _minesMineShaftsLevels with 1 mineshaft on level 1 for each mine
        for (int i = 0; i < _numberOfMines; i++)
        {
            _mineShaftsPerMine.Insert(i, 1);
            _minesMineShaftsLevels.Add(new List<int>());
            _minesMineShaftsLevels[i].Add(1);
        }
    }
    
    public void SetMinesCount(int count)
    {
        _numberOfMines = count;
        InitializeMineLevelsList();
    }


}
