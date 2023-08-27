using GameCode.Init;
using GameCode.Worker;
using UniRx;

namespace GameCode.Mineshaft
{
    public class MineshaftController
    {
        private readonly MineshaftView _view;
        private readonly MineshaftModel _model;
        private readonly IMineshaftFactory _mineshaftFactory;

        public MineshaftController(MineshaftView view, MineshaftModel model, IMineshaftFactory mineshaftFactory,
            GameConfig gameConfig, CompositeDisposable disposable)
        {
            _view = view;
            _model = model;
            _mineshaftFactory = mineshaftFactory;
            var workerModel = new WorkerModel(model, gameConfig.MineshaftWorkerConfig, disposable);
            new MineshaftWorkerController(view, model, workerModel, disposable);

            model.CanUpgrade
                .Subscribe(canUpgrade => view.AreaUiCanvasView.UpgradeButton.interactable = canUpgrade)
                .AddTo(disposable);

            view.AreaUiCanvasView.UpgradeButton.OnClickAsObservable()
                .Subscribe(_ => Upgrade())
                .AddTo(disposable);

            model.StashAmount
                .Subscribe(amount => view.StashAmount = amount.ToString("F0"))
                .AddTo(disposable);
            workerModel.CarryingCapacity
                .Subscribe(capacity => view.AreaUiCanvasView.CarryingCapacity = capacity.ToString("F0"))
                .AddTo(disposable);

            model.UpgradePrice
                .Subscribe(upgradePrice => view.AreaUiCanvasView.UpgradeCost = upgradePrice.ToString("F0"))
                .AddTo(disposable);

            view.NextShaftView.Cost = model.NextShaftPrice.ToString("F0");
            var canBuyNextShaft = model.CanBuyNextShaft.ToReactiveCommand();
            canBuyNextShaft.BindTo(view.NextShaftView.Button).AddTo(disposable);
            canBuyNextShaft.Subscribe(_ => BuyNextShaft())
                .AddTo(disposable);
        }

        private void Upgrade()
        {
            _model.Upgrade();
        }

        private void BuyNextShaft()
        {
            _model.BuyNextShaft();
            _view.NextShaftView.Visible = false;
            _mineshaftFactory.CreateMineshaft(_model.MineshaftNumber + 1, 1, _view.NextShaftView.NextShaftPosition);
        }
    }
}