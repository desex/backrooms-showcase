using Db.BerserkMode;
using Db.Player;
using Ecs.Core.Interfaces;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.EventSystems;
using R3;

namespace Game.Ui.PlayerMovementInput
{
    public class PlayerMovementInputController : UiController<PlayerMovementInputView>,
        IUiInitializable,
        IUpdateSystem
    {
        private const float SWIPE_THRESHOLD = 0.05f;
        private const float SQR_SWIPE_THRESHOLD = SWIPE_THRESHOLD * SWIPE_THRESHOLD;
        
        private readonly IBerserkModeParameters _berserkModeParameters;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IPlayerDoubleTapParameters _tapParameters;
        private readonly IPlayerInputParametersBase _inputParameters;
        private readonly GameContext _game;
        private readonly ITimeProvider _timeProvider;

        private Vector2 _touchPosition;
        private Vector2 _currentTouchPosition;
        
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition; 

        private Vector2 _rawTouchPosition;
        private Vector2 _rawCurrentTouchPosition;

        private float _lastTapTime;
        private float _doubleTapCooldownTime;
        private float _touchTimeCount;

        private bool _isTouchPointerDown;
        private bool _isDoubleTapCooldown;
        private bool _isSwipe;

        private GameEntity PlayerEntity => _game.PlayerEntity;

        public PlayerMovementInputController(
            IBerserkModeParameters berserkModeParameters,
            IPlayerDoubleTapParameters tapParameters,
            IPlayerInputParametersBase inputParameters,
            ICommandBuffer commandBuffer,
            GameContext game,
            ITimeProvider timeProvider
        )
        {
            _berserkModeParameters = berserkModeParameters;
            _commandBuffer = commandBuffer;
            _tapParameters = tapParameters;
            _inputParameters = inputParameters;
            _game = game;
            _timeProvider = timeProvider;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public void Update()
        {
            // Более показать не могу
        }

        private void HoldPointerDown()
        {
            // Более показать не могу
        }

        private void DoubleTapCooldown()
        {
            // Более показать не могу
        }

        private void DoubleTapDetected()
        {
            // Более показать не могу
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            // Более показать не могу
        }

        private void OnPointerMove(PointerEventData eventData)
        {
            // Более показать не могу
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            // Более показать не могу
        }

        public override void OnHide()
            {
            // Более показать не могу
        }
        
        private static Vector2 NormalizeScreenPosition(Vector2 screenPosition)
        {
            // Более показать не могу
            return default;
        }
    }
}