using UnityEngine;

namespace Db.TvLines.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(TvLinesParameters), fileName = nameof(TvLinesParameters))]
    public class TvLinesParameters : ScriptableObject, ITvLinesParameters
    {
        [field: SerializeField] public float MainMenuTvLinesDisappearSmoothTime { get; private set; } = 3f;
        [field: SerializeField] public float MainMenuTvLinesSkipMainMenuSmoothTime { get; private set; } = 1.2f;
    }
}