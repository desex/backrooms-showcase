using System;

namespace Game.Utils.Lights.Actions.Impl
{
    [Serializable]
    public class DelayLightActionData : ILightActionData
    {
        public float Delay;
    }
}