using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerDoubleTapFxParameters), fileName = nameof(PlayerDoubleTapFxParameters))]
    public class PlayerDoubleTapFxParameters : ScriptableObject, IPlayerDoubleTapFxParameters
    {
        [field: ColorUsage(true, true)]
        [field: SerializeField] 
        public Color DefaultColorFx { get; private set; }
        
        [field: Space]
        [field: ColorUsage(true, true)]
        [field: SerializeField] 
        public Color BoostColorFx { get; private set; }
    }
}