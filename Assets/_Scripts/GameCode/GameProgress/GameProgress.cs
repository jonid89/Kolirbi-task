using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCode.Warehouse;
using GameCode.Elevator;
using GameCode.Mineshaft;
using GameCode.Finance;
using UniRx;

public class GameProgress 
{
    private WarehouseModel _warehouseModel;
    private ElevatorModel _elevatorModel;
    private MineshaftCollectionModel _mineshaftCollectionModel;
    private FinanceModel _financeModel;
    private readonly CompositeDisposable _disposable;
    
    private int _numberOfMines;
    private int _currentMineID = 0;
    private List<double> _minesMoney = new List<double>();
    
    private int[] _elevatorLevels = new int[] {1,1};
    private int _elevatorLevel;

    private int[] _warehouseLevels = new int[] {1,1};
    private int _warehouseLevel;

    private int _mineShaftsCount;
    private List<int> _mineShaftsPerMine = new List<int>();
    private List<List<int>> _minesMineShaftsLevels = new List<List<int>>();
    private int _mineShaftLevelSave;
    private int _mineShaftLevelLoad;

    public GameProgress(WarehouseModel warehouseModel, ElevatorModel elevatorModel, 
        MineshaftCollectionModel mineshaftCollectionModel, FinanceModel financeModel, CompositeDisposable disposable)
    {
        _warehouseModel = warehouseModel;
        _elevatorModel = elevatorModel;
        _mineshaftCollectionModel = mineshaftCollectionModel;
        _financeModel = financeModel;
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
        //Saving mine's Money
        _minesMoney.Insert(_currentMineID, _financeModel.Money.Value);
        //Debug.Log("Money Saved on" + _currentMineID + ": " + _minesMoney[_currentMineID]);
        
        //Saving Elevator Level
        _elevatorLevels[_currentMineID] = _elevatorModel.Level.Value;

        //Saving Warehouse Level
        _warehouseLevels[_currentMineID] = _warehouseModel.Level.Value;

        //Iterate through List of mineshafts on Current Mine to save Active mineshafts Level
        for (int i = 1; i <= _mineshaftCollectionModel.GetCount(); i++)
        {
            if(_mineshaftCollectionModel.GetView(i).gameObject.activeSelf)
            {
                _mineShaftLevelSave = _mineshaftCollectionModel.GetModel(i).Level.Value;
                _minesMineShaftsLevels[_currentMineID].Insert(i-1, _mineShaftLevelSave);
                _mineShaftsPerMine[_currentMineID] = i;                
            }
        }
    }

    private void LoadMine()
    {
        //Loading mine's Money
        _financeModel.SetMineMoney(_minesMoney[_currentMineID]);
        /*_financeModel.DrawResource(_financeModel.Money.Value);
        _financeModel.AddResource(_minesMoney[_currentMineID]);*/
        //Debug.Log("Money Loaded on" + _currentMineID + ": " +_minesMoney[_currentMineID]);
        
        //Loading Elevator Level
        _elevatorLevel = _elevatorLevels[_currentMineID];
        _elevatorModel.MineSwitch(_elevatorLevel);

        //Loading Warehouse Level
        _warehouseLevel = _warehouseLevels[_currentMineID];
        _warehouseModel.MineSwitch(_warehouseLevel);
        
        //Iterate through List of mineshafts on Current Mine to load Active mineshafts Level
        for (int i = 1; i <= _mineshaftCollectionModel.GetCount(); i++)
        {
            if(i <= _mineShaftsPerMine[_currentMineID])
            {
                _mineshaftCollectionModel.GetView(i).gameObject.SetActive(true);
                _mineShaftLevelLoad = _minesMineShaftsLevels[_currentMineID][i-1];
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
    
    public void SetMinesCount(int count)
    {
        _numberOfMines = count;
        InitializeMineLevelsList();
    }

    private void InitializeMineLevelsList()
    {
        for (int i = 0; i < _numberOfMines; i++)
        {
            // Initialize _minesMineShaftsLevels with 1 mineshaft on level 1 for each mine
            _mineShaftsPerMine.Add(1); //.Insert(i, 1);
            _minesMineShaftsLevels.Add(new List<int>());
            _minesMineShaftsLevels[i].Add(1);

            // Initialize _minesMoney with initial money amount each mine
            _minesMoney.Add(_financeModel.InitialMoneyPerMine);
        }
    }
    
 


}
