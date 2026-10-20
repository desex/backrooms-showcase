using System.Collections.Generic;
using Ecs.Utils;
using UnityEngine;

namespace Db.Effects
{
    public interface ISpeedEffectParameters
    { 
        float DefaultFov { get; } 
        float TargetFov { get; } 
        float Duration { get; }
        Color TargetColor { get; }
        Color DefaultColor { get; }
        Dictionary<ESpeedEffectAxis, SpeedShaderParameters> SpeedShaderParameters { get; }
    }
}