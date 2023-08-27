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
                .Subscribe(money => view.CashAmount = money)
                .AddTo(disposable);
            
            tutorialModel.ShouldShowTooltip
                .Subscribe(UpdateTooltipVisibility)
                .AddTo(disposable);
        }

        private void UpdateTooltipVisibility(bool shouldShowTooltip)
        {
            _view.TooltipVisible = shouldShowTooltip;
        }
    }
}