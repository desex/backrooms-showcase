using System.Collections.Generic;
using UnityEngine;

namespace Db.Score.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ScoreParameters), fileName = nameof(ScoreParameters))]
    public class ScoreParameters : ScriptableObject, IScoreParameters
    {
        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float DistanceToMetersRatio { get; private set; } = 0.4f;
        
        [field: Header("Score")]
        [field: SerializeField]
        public int ScoreForNormalActionComplete { get; private set; } = 1;
        [field: SerializeField] public int ScoreForFastActionComplete { get; private set; } = 2;
        [field: SerializeField] public int ScoreForOnePassedUnit { get; private set; } = 1;
        
        [field: Header("Multiplier")]
        [field: SerializeField] public float DefaultMultiplier { get; private set; } = 1;
        [field: SerializeField] public int SuccessActionsCountBeforeStartMultiplier { get; private set; } = 1;
        [field: SerializeField] public float IncreaseMultiplierValue { get; private set; } = 1;
        [field: SerializeField] public float StartMultiplierValue { get; private set; } = 2;
        [field: SerializeField] public float ResetMultiplierDelaySeconds { get; private set; } = 3;
        
        
        [field: Header("Combo")]
        [field: SerializeField] 
        public float ComboDivideForAlmondWater { get; private set; } = 5;
        [field: SerializeField] public int SuccessActionsCountBeforeStartCombo { get; private set; } = 2;
        [SerializeField] private List<string> _defaultWords;
        [SerializeField] private List<string> _columnWords;
        

        public IReadOnlyList<string> DefaultWords => _defaultWords;
        public IReadOnlyList<string> ColumnWords => _columnWords;
    }
}