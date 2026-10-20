using Ecs.Game.Components.Movement.ObstaclePass;
using Game.Providers;
using Game.Services.General;
using Game.Utils.Movement;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using Tools.Bots;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Tools
{
    public class PassObstacleBot : MonoBehaviour
    {
        [FormerlySerializedAs("_botConfig")]
        [SerializeField]
        private PassObstacleBotConfig _passObstacleBotConfig;

        private GameContext _game;
        private ICommandBuffer _commandBuffer;
        private IGameFieldProvider _gameFieldProvider;

        [Inject]
        private void Construct(
            ICommandBuffer commandBuffer,
            IGameFieldProvider gameFieldProvider,
            GameContext gameContext
        )
        {
            _commandBuffer = commandBuffer;
            _gameFieldProvider = gameFieldProvider;
            _game = gameContext;
        }
        
        private GameEntity _player;

        private bool _canAction;
        private bool _instantiated;

        private int _framesToSkipRandom;
        private int _framesToSkipLeft;
        private float _moveSpeed;

        private void Start()
        {
            _framesToSkipRandom = _passObstacleBotConfig.FramesToSkipMax;
        }

        public void Update()
        {
            if (!_instantiated)
            {
                Instantiate();
                return;
            }

            TryAction();
            SetTimeScale();
        }

        private void Instantiate()
        {
            if (_game == null)
                return;

            if (_commandBuffer == null)
                return;

            _player = _game.PlayerEntity;

            _player.OnComponentAdded += OnComponentAdded;
            _player.OnComponentReplaced += OnComponentReplaced;
            _player.OnComponentRemoved += OnComponentRemoved;

            _instantiated = true;

            DisableAllObstacleButFirst();
        }

        private void OnComponentAdded(IEntity entity, int index, IComponent component)
        {
            if (component is ReadyToObstaclePassComponent)
            {
                _canAction = true;
            }
        }

        private void OnComponentReplaced(IEntity entity, int index, IComponent previouscomponent, IComponent newcomponent)
        {
        }

        private void OnComponentRemoved(IEntity entity, int index, IComponent component)
        {
            if (component is ReadyToObstaclePassComponent)
            {
                DelayedInvocationService.Invoke(TeleportBack, _passObstacleBotConfig.DelayAfterActionEnd);
            }
        }

        private void TryAction()
        {
            if (_canAction == false)
                return;

            _framesToSkipLeft++;

            if (_framesToSkipLeft < _framesToSkipRandom)
                return;

            CacheMoveSpeed();
            DoAction();

            _canAction = false;
            _framesToSkipLeft = 0;
            _framesToSkipRandom = Random.Range(_passObstacleBotConfig.FramesToSkipMin, _passObstacleBotConfig.FramesToSkipMax);
        }

        private void DoAction()
        {
            switch (_passObstacleBotConfig.ActionType)
            {
                case EPassObstacleBotActionType.SwipeUp:
                    _commandBuffer.Swipe(Vector2.up);
                    break;

                case EPassObstacleBotActionType.SwipeRight:
                    _commandBuffer.Swipe(Vector2.right);
                    break;

                case EPassObstacleBotActionType.SwipeLeft:
                    _commandBuffer.Swipe(Vector2.left);
                    break;

                case EPassObstacleBotActionType.SwipeDown:
                    _commandBuffer.Swipe(Vector2.down);
                    break;

                default:
                case EPassObstacleBotActionType.None:
                    Debug.LogError("Set action type in BotConfig.ActionType");
                    return;
            }
        }

        private void DisableAllObstacleButFirst()
        {
            var parentObject = _gameFieldProvider.GameField.ObstaclesRoot.gameObject;

            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                if (i == 0)
                    continue;

                parentObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void TeleportBack()
        {
            var position = _player.Position.Value - new Vector3(0, 0, _passObstacleBotConfig.BackOffset);

            _player.CharacterController.Value.enabled = false;
            _player.IsEnableCharacterTrigger = false;
            _player.ReplacePosition(position);

            DelayedInvocationService.Invoke(() =>
                                            {
                                                _player.CharacterController.Value.enabled = true;
                                                _player.IsEnableCharacterTrigger = true;
                                                _player.ReplaceForwardSpeed(new ForwardSpeed(_moveSpeed, _moveSpeed));
                                            },
                                            10
            );
        }

        private void CacheMoveSpeed()
        {
            if (_moveSpeed > 0)
                return;

            _moveSpeed = _player.ForwardSpeed.Value.Current;
        }

        private void SetTimeScale()
        {
            Time.timeScale = _passObstacleBotConfig.TimeScale;
        }
    }
}