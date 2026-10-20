using System.Collections.Generic;
using Db.SoundFxBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Sound.Impl
{
    [CreateAssetMenu(menuName = "Settings/Chaser/" + nameof(ChaserSoundParameters), fileName = nameof(ChaserSoundParameters))]
    public class ChaserSoundParameters : ScriptableObject, IChaserSoundParameters
    {
        private const string BASE_FOLDOUT = "Base";
        private const string STEP_FOLDOUT = "Chaser steps";
        private const string ROAR_FOLDOUT = "Roar";
        private const string GREAT_ROAR_FOLDOUT = "Great roar";
        
        
        [field: FoldoutGroup(BASE_FOLDOUT)]
        [field: Range(0, 1f)]
        [field: SerializeField]
        public float BaseSoundsStartDistance { get; private set; }
        
        [field: FoldoutGroup(STEP_FOLDOUT)]
        [field: MinMaxSlider(0f, 1f, true)]
        [field: SerializeField]
        public Vector2 StepVolume { get; private set; }
        
        [field: FoldoutGroup(ROAR_FOLDOUT)]
        [field: MinMaxSlider(10f, 25f, true)]
        [field: SerializeField]
        public Vector2 RoarDelay { get; private set; }

        [FoldoutGroup(ROAR_FOLDOUT)]
        [SerializeField]
        private List<ESoundFxName> _roarSounds;
        public IReadOnlyList<ESoundFxName> RoarSounds => _roarSounds;

        
        [field: FoldoutGroup(GREAT_ROAR_FOLDOUT)]
        [field: SerializeField]
        public ESoundFxName GreatRoarSound { get; private set; }
        
        [field: FoldoutGroup(GREAT_ROAR_FOLDOUT)]
        [field: Range(0, 1f)]
        [field: SerializeField]
        public float GreatRoarDistance { get; private set; }
    }
}