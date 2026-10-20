using System.Collections.Generic;
using Game.Utils.HoleGenerator;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.HoleGenerator.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(HoleConstructorParameters), fileName = nameof(HoleConstructorParameters))]
    public class HoleConstructorParameters : SerializedScriptableObject, IHoleConstructorParameters
    {
        [field: Title("Hole Settings")]
        [field: Title("Parts Hole")]
        [field: SerializeField] public GameObject HoleBottom { get; private set; }
        [field: SerializeField] public GameObject HoleWall { get; private set; }
        [field: SerializeField] public GameObject HoleFloor { get; private set; } 
        [field: SerializeField] public GameObject UpHoleWall { get; private set; }
        [field: SerializeField] public GameObject UpHoleCelling { get; private set; }
    
        [field: Title("Holes Trigger")]
        [field: SerializeField] public GameObject EffectsTriggers { get; private set; }
        [field: SerializeField] public GameObject EffectsFeedbackBeforeTriggers { get; private set; }
        [field: SerializeField] public GameObject GhostJumpTrigger { get; private set; }
    
        [field: SerializeField] public Dictionary<EGeneratorHoleType, List<ObstaclePassSettingsParameters>> PassObstacleHole { get; private set; }
    
        [field: Title("Floor Settings")]
        [OdinSerialize]
        private Dictionary<EWallSide, HoleFloorDimensionsParameters> _floorsSettings;
    
        public IReadOnlyDictionary<EWallSide, HoleFloorDimensionsParameters> FloorsSettings
            => _floorsSettings;
    
        [field: Title("Materials")]
        [field: Space]
        [field: Title("Materials in Yellow Level")]
        [field: SerializeField] public Material HoleWallAttachedYellowMaterial { get; private set; }
        [field: SerializeField] public Material HoleWallYellowMaterial { get; private set; }
        [field: SerializeField] public Material HoleFloorYellowMaterial { get; private set; }
        [field: SerializeField] public Material UpHoleWallYellowMaterial { get; private set; }
        [field: SerializeField] public Material UpHoleCellingYellowMaterial { get; private set; }
        
        [field: Space]
        [field: Title("Materials in Pool Level")]
        [field: SerializeField] public Material HoleWallAttachedPoolMaterial { get; private set; }
        [field: SerializeField] public Material HoleWallPoolMaterial { get; private set; }
        [field: SerializeField] public Material HoleFloorPoolMaterial { get; private set; }
        [field: SerializeField] public Material UpHoleWallPoolMaterial { get; private set; }    
        [field: SerializeField] public Material UpHoleCellingPoolMaterial { get; private set; }
        
        [field: Space]
        [field: Title("Materials in Red Level")]
        [field: SerializeField] public Material HoleWallAttachedRedMaterial { get; private set; }
        [field: SerializeField] public Material HoleWallRedMaterial { get; private set; }
        [field: SerializeField] public Material HoleFloorRedMaterial { get; private set; }
        [field: SerializeField] public Material UpHoleWallRedMaterial { get; private set; }
        [field: SerializeField] public Material UpHoleCellingRedMaterial { get; private set; }
    }
}
