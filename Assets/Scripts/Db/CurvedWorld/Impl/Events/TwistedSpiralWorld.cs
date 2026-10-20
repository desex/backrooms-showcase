using UnityEngine;

namespace Db.CurvedWorld.Impl.Events
{
    [CreateAssetMenu(menuName = "Settings/CurvedWorldEvent/" + nameof(TwistedSpiralWorld), fileName = nameof(TwistedSpiralWorld))]
    public class TwistedSpiralWorld : ACurvedWorldTypeParametersBase
    {
        [field: Header("Horizontal")]
        [field: SerializeField] public float BendHorizontalSize { get; private set; }
        [field: SerializeField] public float BendHorizontalOffset { get; private set; }
        
        [field: Header("Vertical")]
        [field: SerializeField] public float BendVerticalSize { get; private set; }
        [field: SerializeField] public float BendVerticalOffset { get; private set; }
        
        [field: Header("Curvature")]
        [field: SerializeField] public float BendCurvatureSize { get; private set; }
        [field: SerializeField] public float BendCurvatureOffset { get; private set; }
    }
}