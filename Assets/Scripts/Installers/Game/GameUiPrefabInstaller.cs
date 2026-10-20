using Game.UI.ChaseProgressBar;
using Game.UI.Checkpoints.Impl;
using Game.UI.Collectables;
using Game.UI.Combo;
using Game.UI.Death;
using Game.UI.Effects;
using Game.UI.FinalScore;
using Game.UI.GameRestartButton;
using Game.UI.MainMenu;
using Game.UI.Pause;
using Game.Ui.PlayerMovementInput;
using Game.UI.QTE;
using Game.UI.Revive;
using Game.UI.Score;
using Game.UI.SettingsPopup;
using Game.UI.Tutorial;
using SimpleUi;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameUiPrefabInstaller), fileName = nameof(GameUiPrefabInstaller))]
    public class GameUiPrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas _canvas;
        
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
        [SerializeField] private DebugView _debug;
#endif
        [SerializeField] private ComboView _comboView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private CollectablesView _collectablesView;

        [SerializeField] private DeathBlackoutView _deathBlackoutView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private ReviveView _reviveForWater;
        [SerializeField] private FinalScoreView _finalScoreView;
        [SerializeField] private ChaserView _chaserView;
        [SerializeField] private CheckpointView _checkpointView;
        [SerializeField] private QteView _qteView;
        
        [Space]
        [Header("Settings")]
        [SerializeField] private SettingsPopupView _settingsPopupView;
        
        [Space]
        [Header("Input")]
        [SerializeField] private PlayerMovementInputView _playerMovementInputView;

        [Space]
        [Header("Main menu")]
        [SerializeField] private MainMenuView _mainMenuView;
        
        [Space]
        [Header("Effects")]
        [SerializeField] private UiEffectsView _uiEffectsView;
        
        [Space]
        [Header("Tutorial")]
        [SerializeField] private TutorialHintsView _tutorialHintsView;
        [SerializeField] private TutorialFailView _tutorialFailView;

        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(_canvas);
            var canvasTransform = canvasView.transform;

            Container.BindUiView<PlayerMovementInputController, PlayerMovementInputView>(_playerMovementInputView, canvasTransform);
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            Container.BindUiView<DebugController, DebugView>(_debug, canvasTransform);
#endif
            Container.BindUiView<ReviveController, ReviveView>(_reviveForWater, canvasTransform);
            Container.BindUiView<QteController, QteView>(_qteView, canvasTransform);
            Container.BindUiView<DeathController, DeathView>(_deathView, canvasTransform);
            Container.BindUiView<TutorialHintsController, TutorialHintsView>(_tutorialHintsView, canvasTransform);
            Container.BindUiView<TutorialFailController, TutorialFailView>(_tutorialFailView, canvasTransform);
            Container.BindUiView<DeathBlackoutController, DeathBlackoutView>(_deathBlackoutView, canvasTransform);
            Container.BindUiView<FinalScoreController, FinalScoreView>(_finalScoreView, canvasTransform);
            Container.BindUiView<ComboController, ComboView>(_comboView, canvasTransform);
            Container.BindUiView<PauseController, PauseView>(_pauseView, canvasTransform);
            Container.BindUiView<CollectablesController, CollectablesView>(_collectablesView, canvasTransform);
            Container.BindUiView<ScoreController, ScoreView>(_scoreView, canvasTransform);
            Container.BindUiView<SettingsPopupController, SettingsPopupView>(_settingsPopupView, canvasTransform);
            Container.BindUiView<MainMenuController, MainMenuView>(_mainMenuView, canvasTransform);
            Container.BindUiView<UiEffectsController, UiEffectsView>(_uiEffectsView, canvasTransform);
            Container.BindUiView<ChaserController, ChaserView>(_chaserView, canvasTransform);
            Container.BindUiView<CheckpointController, CheckpointView>(_checkpointView, canvasTransform);
        }
    }
}