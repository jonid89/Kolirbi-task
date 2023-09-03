using GameCode.Finance;
using GameCode.Tutorial;
using UniRx;

namespace GameCode.UI
{
    public class HudController
    {
        private readonly HudView _view;

        public HudController(HudView view, FinanceModel financeModel, ITutorialModel tutorialModel,
            CompositeDisposable disposable)
        {
            _view = view;
            
            financeModel.Money
                .Subscribe(money => _view.CashAmount = money)
                .AddTo(disposable);
            
            tutorialModel.ShouldShowTooltip
                .Subscribe(UpdateTooltipVisibility)
                .AddTo(disposable);

            SetMineID(0);
        }

        private void UpdateTooltipVisibility(bool shouldShowTooltip)
        {
            _view.TooltipVisible = shouldShowTooltip;
        }

        public void SetMineID(int mineID)
        {
            _view.MineLabel = mineID+1;
        }
    }
}