#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using System.Collections.Generic;
using DG.Tweening;
using Game.UI.UIBase;
using SimpleUi.Abstracts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.GameRestartButton
{
    public class DebugView : UiView, IUiDisablable
    {
        private const string BASIC_FOLDOUT = "Basic";
        private const string STATS_FOLDOUT = "Stats";
        private const string LEVEL_FOLDOUT = "Level";
        private const string PLAYER_FOLDOUT = "Player";
        private const string COLLECTABLES_FOLDOUT = "Collectables";
        private const string QTE_FOLDOUT = "QTE";
        private const string ADS_FOLDOUT = "Ads";
        private const string UI_VISIBILITY_FOLDOUT = "Ui visibility";
        private const string COMMON_FOLDOUT = "Common";

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public Button ToggleButton { get; private set; }

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public Button CloseButton { get; private set; }

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public GameObject Debug { get; private set; }

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI VersionText { get; private set; }

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public List<GameObject> Pages { get; private set; }

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public Button BackPageButton { get; private set; }

        [field: FoldoutGroup(BASIC_FOLDOUT)]
        [field: SerializeField]
        public Button NextPageButton { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public Button ToggleStatsButton { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public Button ToggleDevStatsButton { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public GameObject StatsView { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public GameObject DevStatsView { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI PlayerSpeedText { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI ChaserSpeedText { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public FPSView FPSView { get; private set; }

        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public DebugCollectablesView CollectablesView { get; private set; }        
        
        [field: FoldoutGroup(STATS_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI SequenceText { get; private set; }

        [field: FoldoutGroup(LEVEL_FOLDOUT)]
        [field: SerializeField]
        public Button RestartWithoutTutorialButton { get; private set; }

        [field: FoldoutGroup(LEVEL_FOLDOUT)]
        [field: SerializeField]
        public Button RestartWithTutorialButton { get; private set; }

        [field: FoldoutGroup(PLAYER_FOLDOUT)]
        [field: SerializeField]
        public Button ChangeSlidesButton { get; private set; }

        [field: FoldoutGroup(PLAYER_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI SlidesText { get; private set; }

        [field: FoldoutGroup(PLAYER_FOLDOUT)]
        [field: SerializeField]
        public Button ToggleCollisionButton { get; private set; }

        [field: FoldoutGroup(PLAYER_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI CollisionText { get; private set; }

        [field: FoldoutGroup(PLAYER_FOLDOUT)]
        [field: SerializeField]
        public Button ToggleDeathFromChaserButton { get; private set; }

        [field: FoldoutGroup(PLAYER_FOLDOUT)]
        [field: SerializeField]
        public TextMeshProUGUI DeathFromChaserText { get; private set; }
       
        [field: FoldoutGroup("Player")]
        [field: SerializeField]
        public Button TogglePlayerMovementButton { get; private set; }
        
        [field: FoldoutGroup("Player")]
        [field: SerializeField]
        public TextMeshProUGUI PlayerMovementText { get; private set; }

        [field: FoldoutGroup(COLLECTABLES_FOLDOUT)]
        [field: SerializeField]
        public Button AddAlmondWaterButton { get; private set; }

        [field: FoldoutGroup(COLLECTABLES_FOLDOUT)]
        [field: SerializeField]
        public Button ResetAlmondWaterButton { get; private set; }

        [field: FoldoutGroup(COLLECTABLES_FOLDOUT)]
        [field: SerializeField]
        public Button AddCassetteButton { get; private set; }

        [field: FoldoutGroup(COLLECTABLES_FOLDOUT)]
        [field: SerializeField]
        public Button ResetCassetteButton { get; private set; }

        [field: FoldoutGroup(ADS_FOLDOUT)]
        [field: SerializeField]
        public Button ToggleAdsButton { get; private set; }

        [field: FoldoutGroup(ADS_FOLDOUT)]
        [field: SerializeField]
        public Image AdsImage { get; private set; }

        [field: FoldoutGroup(UI_VISIBILITY_FOLDOUT)]
        [field: SerializeField]
        public Button ChangeChaserUiVisibilityButton { get; private set; }

        [field: FoldoutGroup(UI_VISIBILITY_FOLDOUT)]
        [field: SerializeField]
        public TMP_Text ChangeChaserUiVisibilityText { get; private set; }

        [field: FoldoutGroup(UI_VISIBILITY_FOLDOUT)]
        [field: SerializeField]
        public Button ChangeOtherUiVisibilityButton { get; private set; }
        
        [field: FoldoutGroup(UI_VISIBILITY_FOLDOUT)]
        [field: SerializeField]
        public TMP_Text ChangeOtherUiVisibilityText { get; private set; }
        
        [field: FoldoutGroup(COMMON_FOLDOUT)]
        [field: SerializeField]
        public Button ChangeFpsLockButton { get; private set; }
        
        [field: FoldoutGroup(COMMON_FOLDOUT)]
        [field: SerializeField]
        public TMP_Text ChangeFpsLockText { get; private set; }

        public void ToggleAdsImage(bool isEnabled)
        {
            // Более показать не могу
        }
    }
}
#endif