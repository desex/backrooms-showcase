using Db.SoundFxBase;
using Game.Utils.Sound;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Screamers
{
    [Game]
    public class ScreamerPlayBackStateComponent : IComponent
    {
        public EPlayBackStatus PlayBackStatus;
        public ESoundFxName SoundFxName;
    }
}