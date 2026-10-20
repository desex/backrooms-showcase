using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(DashParameters), fileName = nameof(DashParameters))]
    public class DashParameters : ScriptableObject, IDashParameters
    {
        [field: SerializeField] public DashVo DashVo { get; private set; }
        [field: SerializeField] public DashInHoleVo DashInHoleVo { get; private set; }
    }
}