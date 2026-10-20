using System.Collections.Generic;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Obstacles.Impls
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ReactionObstacleEffectBase), fileName = nameof(ReactionObstacleEffectBase))]
    public class ReactionObstacleEffectBase : SerializedScriptableObject, IReactionObstacleEffectBase
    {
        [SerializeField] private Dictionary<EObstacleEffectType, DestructableEffectParameters> _effectList;
        
        [field: SerializeField] public float OffsetY { get; private set; }
        [field: SerializeField] public float MultiplierZ { get; private set; }
        [field: SerializeField] public float MultiplierZForDashMin { get; private set; }
        [field: SerializeField] public float MultiplierZForDashMax { get; private set; }
        [field: SerializeField] public float PossibleCapPlayerSpeed { get; private set; }
        [field: SerializeField] public float IncreaseMultiplierDashMax { get; private set; }
        public IReadOnlyDictionary<EObstacleEffectType, DestructableEffectParameters> EffectsList => _effectList;
    }
}