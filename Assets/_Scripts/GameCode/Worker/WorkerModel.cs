using GameCode.GameArea;
using UniRx;

namespace GameCode.Worker
{
    public class WorkerModel
    {
        private readonly IWorkerConfig _config;

        public WorkerState State;
        public IReactiveProperty<double> CarryingAmount;
        public float Speed => _config.Speed;
        public IReadOnlyReactiveProperty<double> CarryingCapacity { get; }
        public float JobTime => _config.GetJobTime(State);

        public WorkerModel(IAreaModel areaModel, IWorkerConfig config, CompositeDisposable disposable)
        {
            _config = config;
            CarryingAmount = new ReactiveProperty<double>();
            CarryingCapacity = areaModel.Level.Select(_ => _config.Skill * areaModel.SkillMultiplier)
                .ToReadOnlyReactiveProperty()
                .AddTo(disposable);
        }
    }
}