using GameCode.Mineshaft;
using GameCode.Worker;
using UnityEngine;

namespace GameCode.Init
{
    [CreateAssetMenu(menuName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private MineshaftConfig _mineshaftConfig;
        [SerializeField] private WorkerConfig _mineshaftWorkerConfig;
        [SerializeField] private WorkerConfig _elevatorWorkerConfig;
        [SerializeField] private WorkerConfig _warehouseWorkerConfig;

        public float ActorUpgradePriceIncrement;
        public float ActorUpgradeSkillIncrement;

        public float ActorPriceIncrementPerShaft;
        public float ActorSkillIncrementPerShaft;

        public IMineshaftConfig MineshaftConfig => _mineshaftConfig;
        public IWorkerConfig MineshaftWorkerConfig => _mineshaftWorkerConfig;
        public IWorkerConfig ElevatorWorkerConfig => _elevatorWorkerConfig;
        public IWorkerConfig WarehouseWorkerConfig => _warehouseWorkerConfig;
    }
}