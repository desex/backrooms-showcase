using System;
using UnityEngine;

namespace Db.CurvedWorld.Impl.Events
{
    [Serializable]
    [CreateAssetMenu(menuName = "Settings/CurvedWorldEvent/" + nameof(CylindricalRolloffWorld), fileName = nameof(CylindricalRolloffWorld))]
    public class CylindricalRolloffWorld : ACurvedWorldTypeParametersBase
    {
        [field: Header("Curvature")]
        [field: SerializeField] public float BendCurvatureSize { get; private set; }
        [field: SerializeField] public float BendCurvatureOffset { get; private set; }
    }
}