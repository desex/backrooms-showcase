using Sirenix.OdinInspector;
using Tools.Bots;
using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(menuName = nameof(PassObstacleBotConfig))]
    public class PassObstacleBotConfig : ScriptableObject
    {
        [field: SerializeField]
        [field: Space]
        [field: Header("Значения можно менять в рантайме")]
        [field: Tooltip("Время задержки телепорта назад после окончания действия в секундах")]
        [field: MinValue(0)]
        public float DelayAfterActionEnd { get; private set; }

        [field: SerializeField]
        [field: Space]
        [field: Tooltip("Расстояние телепорта назад в метрах")]
        [field: MinValue(0)]
        public float BackOffset { get; private set; }

        [field: SerializeField]
        [field: Space]
        [field: Tooltip("Количество кадров, начиная с которого стоит применить действие")]
        [field: MinValue(0)]
        public int FramesToSkipMin { get; private set; }

        [field: SerializeField]
        [field: Space]
        [field: MinValue(0)]
        [field: Tooltip("Максимальное количество кадров, до которого можно применить действие")]
        [field: ValidateInput(nameof(ValidateFramesToSkipMax), "Значение должно быть больше, чем FramesToSkipMin")]
        public int FramesToSkipMax { get; private set; }
        
        [field: SerializeField]
        [field: Space]
        [field: Tooltip("Тип действия, которое выполнит бот")]
        public EPassObstacleBotActionType ActionType { get; private set; } = EPassObstacleBotActionType.SwipeUp;
        
        [field: SerializeField]
        [field: Space]
        [field: Range(0.1f, 3f)]
        [field: Tooltip("Множитель времени в игре")]
        public float TimeScale { get; private set; } = 1f;

        private bool ValidateFramesToSkipMax(int value) =>
            value > FramesToSkipMin;
    }
}