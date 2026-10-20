using System.Collections.Generic;
using DG.Tweening;
using Game.Utils.Rebound;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ReboundParameters), fileName = nameof(ReboundParameters))]
    public class ReboundParameters : SerializedScriptableObject, IReboundParameters
    {
        [OdinSerialize] private Dictionary<EReboundAngle, float> _reboundAngles;
        [OdinSerialize] private Dictionary<EReboundForce, float> _reboundForces;

        public IReadOnlyDictionary<EReboundAngle, float> ReboundAngles => _reboundAngles; 
        public IReadOnlyDictionary<EReboundForce, float> ReboundForces => _reboundForces; 

        [field: SerializeField] public float ReboundDuration { get; private set; }

        [field: BoxGroup("Impulse")]
        [field: SerializeField]
        public float ReboundImpulse { get; private set; }

        [field: BoxGroup("Impulse")]
        [field: SerializeField]
        public float SlideReboundImpulse { get; private set; }

        [field: SerializeField] public AnimationCurve ReboundCurve { get; private set; }

        [field: Header("Animation")]
        [field: SerializeField]
        public float AnimationDuration { get; private set; }

        [field: SerializeField] public Ease AnimationEase { get; private set; }

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float AnimationRotationIntensity { get; private set; }

        [field: SerializeField] public Vector3 AnimationForwardContactEulerAngles { get; private set; }

        [field: SerializeField] public float ForwardRotationThresholdAngle { get; private set; }

        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float AutoTurnDelay { get; private set; } = 0.3f;
    }
}