using System;
using Game.Utils.Tutorial;

namespace Game.DaoData.Tutorial
{
    [Serializable]
    public struct PassedTutorialStateData
    {
        public ETutorialState State;
        public bool Condition;
    }
}