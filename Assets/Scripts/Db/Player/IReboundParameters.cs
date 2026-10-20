using System.Collections.Generic;
using DG.Tweening;
using Game.Utils.Rebound;
using UnityEngine;

namespace Db.Player
{
    public interface IReboundParameters
    {
        IReadOnlyDictionary<EReboundAngle, float> ReboundAngles { get; }

        IReadOnlyDictionary<EReboundForce, float> ReboundForces { get; }
        float ReboundDuration { get; }
        float ReboundImpulse { get; }
        float SlideReboundImpulse { get; }
        AnimationCurve ReboundCurve { get; }
        
        float AnimationDuration { get; }
        Ease AnimationEase { get; }
        float AnimationRotationIntensity { get; }
        Vector3 AnimationForwardContactEulerAngles { get; }
        float ForwardRotationThresholdAngle { get; }
        float AutoTurnDelay { get; }
    }
}