using System;
using AmazingAssets.CurvedWorld;
using UnityEngine;

namespace Db.CurvedWorld.Impl.Events
{
    [Serializable]
    public abstract class ACurvedWorldTypeParametersBase : ScriptableObject
    {
        [field: Header("Event Settings")]
        [field: SerializeField] public BendType BendType { get; private set; }
        [field: SerializeField] public int EventDurationInCountSequences { get; private set; }
        [field: SerializeField] public float TimeInterpolationToCurveWorld { get; private set; }
        [field: SerializeField] public float TimeToDisableCurvedWorld { get; private set; }
    }
}