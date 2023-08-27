using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCode.Warehouse;
using GameCode.Elevator;
using GameCode.Mineshaft;

public class GameProgress 
{
    /*private WarehouseModel _warehouseModel;
    private ElevatorModel _elevatorModel;
    private MineshaftCollectionModel _mineshaftCollectionModel;
    private MineSwitchController _mineSwitchController;

    public GameProgress(WarehouseModel warehouseModel, ElevatorModel elevatorModel, 
        MineshaftCollectionModel mineshaftCollectionModel, MineSwitchController mineSwitchController)
    {
        _warehouseModel = warehouseModel;
        _elevatorModel = elevatorModel;
        _mineshaftCollectionModel = mineshaftCollectionModel;
        _mineSwitchController = mineSwitchController;

        _mineSwitchController.
    }*/

    private int _elevatorLevel;
    public int ElevatorLevel => _elevatorLevel;

    public GameProgress()
    {

    }

    public void SetElevatorLevel(int elevatorLevel)
    {
        _elevatorLevel = elevatorLevel;
    }


}
