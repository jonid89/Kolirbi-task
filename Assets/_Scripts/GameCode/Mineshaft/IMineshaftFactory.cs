using UnityEngine;

namespace GameCode.Mineshaft
{
    public interface IMineshaftFactory
    {
        void CreateMineshaft(int mineshaftNumber, int mineshaftLevel, Vector2 position);
    }
}