using UniRx;

namespace GameCode.Tutorial
{
    public class TutorialModel : ITutorialModel
    {
        public IReactiveProperty<bool> ShouldShowTooltip { get; }

        public TutorialModel()
        {
            ShouldShowTooltip = new ReactiveProperty<bool>(true);
        }
    }
}