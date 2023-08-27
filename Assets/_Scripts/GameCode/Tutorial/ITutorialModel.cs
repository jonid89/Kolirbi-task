using UniRx;

namespace GameCode.Tutorial
{
    public interface ITutorialModel
    {
        IReactiveProperty<bool> ShouldShowTooltip { get; }
    }
}