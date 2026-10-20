using System;
using UnityEngine;

namespace Db.CurvedWorld.Impl.Events
{
    [Serializable]
    [CreateAssetMenu(menuName = "Settings/CurvedWorldEvent/" + nameof(ClassicRunnerWorld), fileName = nameof(ClassicRunnerWorld))]
    public class ClassicRunnerWorld : ACurvedWorldTypeParametersBase
    {
        [field: Header("Vertical")]
        [field: SerializeField] public float BendVerticalSize { get; private set; }
        [field: SerializeField] public float BendVerticalOffset { get; private set; }
        
        [field: Header("Horizontal")]
        [field: SerializeField] public float BendHorizontalSize { get; private set; }
        [field: SerializeField] public float BendHorizontalOffset { get; private set; }
    }
}