using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.Tutorial.StateVos;
using DG.Tweening;
using Game.Utils.Sequence;
using Game.Utils.Tutorial;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Tutorial.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(TutorialParameters), fileName = nameof(TutorialParameters))]
    public class TutorialParameters : SerializedScriptableObject, ITutorialParameters
    {
        private const float DISTANCE_THRESHOLD = 1f;
        private const string SLOWDOWN_TAB = "Slowdown";
        private const string CONCRETE_STATES_FOLDOUT = "Concrete states";
        
        [field: SerializeField] public Vector3 InitialChunkOffset { get; private set; } = new(0, 0, -10f);
        [field: SerializeField] public float FailTeleportForwardOffset { get; private set; } = 1f;
        [field: SerializeField] public float TutorialDistanceInUnits { get; private set; } = 481.9118f;

        [PropertySpace(10)]
        [SerializeField] 
        private SequenceParametersData _preTutorialSequence;
        
        [SerializeField]
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Tutorial step", ValueLabel = "Tutorial sequence")]
        private Dictionary<ETutorialState, TutorialSequencesParameters> _tutorialSequences = new();

        public SequenceParameters PreTutorialSequence => _preTutorialSequence.SequenceParameters;
        public IReadOnlyDictionary<ETutorialState, TutorialSequencesParameters> TutorialSequences => _tutorialSequences;

        [Header("States")]
        [SerializeField]
        private List<ETutorialState> _statesInOrder = new();
        public IReadOnlyList<ETutorialState> StatesInOrder => _statesInOrder;

        [field: SerializeField] 
        public int FirstSequencesCount { get; private set; } = 3;
        
        [field: SerializeField]
        public float PlayerAfkDelay { get; private set; } = 5f;

        [field: TabGroup(SLOWDOWN_TAB)]
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float SlowdownTimeScale { get; private set; } = 0.05f;

        [field: TabGroup(SLOWDOWN_TAB)]
        [field: SerializeField]
        [field: Range(-1f, 0f)]
        public float SlowdownVerticalSpeed { get; private set; } = 0.05f;

        [field: TabGroup(SLOWDOWN_TAB)]
        [field: SerializeField]
        [field: MinValue(0f)]
        public float SlowdownDuration { get; private set; } = 0.7f;

        [field: TabGroup(SLOWDOWN_TAB)]
        [field: SerializeField]
        public Ease SlowdownEase { get; private set; } = Ease.InCubic;
        
        [field: TabGroup(SLOWDOWN_TAB)] 
        [field: SerializeField]
        [field: MinValue(0f)]
        public float RevertSlowdownDuration { get; private set; } = 0.3f;

        [field: TabGroup(SLOWDOWN_TAB)]
        [field: SerializeField]
        public Ease RevertSlowdownEase { get; private set; } = Ease.OutCubic;
        

        [field: FoldoutGroup(CONCRETE_STATES_FOLDOUT)]
        [field: SerializeField]
        public TutorialDoubleJumpParameters DoubleJumpParameters { get; private set; }

        [field: FoldoutGroup(CONCRETE_STATES_FOLDOUT)]
        [field: SerializeField]
        public TutorialTiltingParameters TiltingParameters { get; private set; }

        [field: FoldoutGroup(CONCRETE_STATES_FOLDOUT)]
        [field: SerializeField]
        public TutorialTurningParameters TurningParameters { get; private set; }
        
        [field: FoldoutGroup(CONCRETE_STATES_FOLDOUT)]
        [field: SerializeField]
        public TutorialSlidingParameters SlidingParameters { get; private set; }

        [field: FoldoutGroup(CONCRETE_STATES_FOLDOUT)]
        [field: SerializeField]
        public TutorialDashParameters DashParameters  { get; private set; }
        
        
        public float FirstTutorialDistanceThresholdInUnits => DISTANCE_THRESHOLD;
    }
}