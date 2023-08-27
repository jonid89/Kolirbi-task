namespace GameCode.Worker
{
    public interface IWorkerConfig
    {
        float Speed { get; }
        float Skill { get; }
        float GetJobTime(WorkerState state);
    }
}