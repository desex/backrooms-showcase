using UnityEngine;

namespace Db.Loading.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(LoadingParameters), fileName = nameof(LoadingParameters))]
    public class LoadingParameters : ScriptableObject, ILoadingParameters
    {
        [field: SerializeField] public int TickBeforeUpdateLoadingDots { get; private set; }
    }
}