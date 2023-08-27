using GameCode.Init;
using GameCode.Mineshaft;
using GameCode.Worker;
using UniRx;

namespace GameCode.Elevator
{
    public class ElevatorController
    {
        private readonly ElevatorModel _model;

        public ElevatorController(ElevatorView view, ElevatorModel model, MineshaftCollectionModel mineshaftCollectionModel,
             GameConfig gameConfig, CompositeDisposable disposable)
        {
            _model = model;

            var workerModel = new WorkerModel(model, gameConfig.ElevatorWorkerConfig, disposable);
            new ElevatorWorkerController(view, model, workerModel, mineshaftCollectionModel, disposable);

            model.CanUpgrade
                .Subscribe(canUpgrade => view.AreaUiCanvasView.UpgradeButton.interactable = canUpgrade)
                .AddTo(disposable);

            view.AreaUiCanvasView.UpgradeButton.OnClickAsObservable()
                .Subscribe(_ => Upgrade())
                .AddTo(disposable);

            model.StashAmount.Subscribe(amount => view.StashAmount = amount.ToString("F0"))
                .AddTo(disposable);
            workerModel.CarryingCapacity
                .Subscribe(capacity => view.AreaUiCanvasView.CarryingCapacity = capacity.ToString("F0"))
                .AddTo(disposable);

            model.UpgradePrice
                .Subscribe(upgradePrice => view.AreaUiCanvasView.UpgradeCost = upgradePrice.ToString("F0"))
                .AddTo(disposable);
        }

        private void Upgrade()
        {
            _model.Upgrade();
        }
    }
}