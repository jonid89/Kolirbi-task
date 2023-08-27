using System;
using UnityEngine;

namespace GameCode.Worker
{
    [CreateAssetMenu(menuName = "Worker Config")]
    public class WorkerConfig : ScriptableObject, IWorkerConfig
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _skill;
        [SerializeField] private float _loadTime;
        [SerializeField] private float _dropTime;

        public float Speed => _speed;
        public float Skill => _skill;

        public float GetJobTime(WorkerState state)
        {
            switch (state)
            {
                case WorkerState.LOAD:
                    return _loadTime;
                case WorkerState.DROP:
                    return _dropTime;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}