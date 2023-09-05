using GameCode.CameraRig;
using GameCode.Elevator;
using GameCode.Finance;
using GameCode.Mineshaft;
using GameCode.Tutorial;
using GameCode.UI;
using GameCode.Warehouse;
using GameCode.Progress;
using UniRx;
using UnityEngine;

namespace GameCode.Init
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private HudView _hudView;
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private MineSwitchView _mineswitchPanelView;
        [SerializeField] private HudButtonView _mapButtonView;
        [SerializeField] private HudButtonView _closeButtonView;
        [SerializeField] private ElevatorView _elevatorView;
        [SerializeField] private WarehouseView _warehouseView;
        [SerializeField] private Transform _mineshaftStartingPosition;

        private void Start()
        {
            var disposable = new CompositeDisposable().AddTo(this);

            var tutorialModel = new TutorialModel();
            var financeModel = new FinanceModel(_gameConfig.InitialMoneyPerMine);
                        
            new CameraController(_cameraView, tutorialModel);

            //Hud
            var hudController = new HudController(_hudView, financeModel, tutorialModel, disposable);
            
            //Mineshaft
            var mineshaftCollectionModel = new MineshaftCollectionModel();
            var mineshaftFactory = new MineshaftFactory(mineshaftCollectionModel, financeModel, _gameConfig, disposable);
            mineshaftFactory.CreateMineshaft(1,1, _mineshaftStartingPosition.position);

            //Elevator
            var elevatorModel = new ElevatorModel(1, _gameConfig, financeModel, disposable);
            new ElevatorController(_elevatorView, elevatorModel, mineshaftCollectionModel, _gameConfig, disposable);
            
            //Warehouse
            var warehouseModel = new WarehouseModel(1, _gameConfig, financeModel, disposable);
            new WarehouseController(_warehouseView, warehouseModel, elevatorModel, _gameConfig, disposable);

            //GameProgress
            var gameProgress = new GameProgress(warehouseModel, elevatorModel, mineshaftCollectionModel, financeModel, disposable);

            //MineSwitch
            var mineswitchController = new MineSwitchController(_mineswitchPanelView, gameProgress, hudController, disposable);
            
            new HudButtonController(_mapButtonView, mineswitchController, disposable);
            new HudButtonController(_closeButtonView, mineswitchController, disposable);
    
            foreach (var mineswitchButton in _mineswitchPanelView.MineSwitchButtons)
            {
                HudButtonView mineswitchButtonView = mineswitchButton.gameObject.GetComponent<HudButtonView>();
                new HudButtonController(mineswitchButtonView, mineswitchController, disposable);
            }
                


        }
    }
}