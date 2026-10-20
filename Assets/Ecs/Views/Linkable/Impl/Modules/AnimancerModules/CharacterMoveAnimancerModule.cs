using Animancer;
using Db.Obstacles;
using Db.Player;
using Ecs.Utils;
using Ecs.Utils.Travelator;
using Game.Services.Start;
using Game.Utils.Animancer;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class CharacterMoveAnimancerModule : AObjectViewModule
    {
        [SerializeField] private MotionClipTransition _defaultRun;
        [SerializeField] private MotionClipTransition _speedBoostRun;
        [SerializeField] private MotionClipTransition _turnLeft;
        [SerializeField] private MotionClipTransition _turnRight;

        private IGameStartService _gameStartService;
        private IPlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
        private IPlayerAccelerationParametersBase _playerAccelerationParametersBase;
        private ITravelatorParameters _travelatorParameters;
        private GameEntity _self;
        private GameContext _gameContext;
        private float _defaultRunSpeed;

        [Inject]
        private void Construct(
            IGameStartService gameStartService,
            IPlayerSpeedProgressionParameters playerSpeedProgressionParameters,
            IPlayerAccelerationParametersBase playerAccelerationParametersBase,
            ITravelatorParameters travelatorParameters,
            GameContext gameContext
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnSpeedInAnimationBoost(GameEntity entity, float value)
        {
            // Более показать не могу
        }
        
        private void OnSpeedBoostOnTravelator(GameEntity entity, TravelatorData value)
        {
            // Более показать не могу
        }

        private void SetFadeDuration(GameEntity entity)
        {
            // Более показать не могу
        }

        private void ResetDefaultRunFadeDuration(GameEntity entity)
        {
            // Более показать не могу
        }
 
        private void OnSpeedChanged(GameEntity entity, float value)
        {
            // Более показать не могу
        }

        private void OnBodyMoveType(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }

        private void OnSpeedBoostRun(GameEntity entity)
        {
            // Более показать не могу
        }
        
        private void EndSpeedBoostRun()
        {
            // Более показать не могу
        }

        private void OnTurningAdded(GameEntity entity, TurningValues turningValues)
        {
            // Более показать не могу
        }

        private void OnTiltingChanged(GameEntity entity, ETiltType tiltType)
        {
            // Более показать не могу
        }

        private void OnTurningRemoved(GameEntity entity)
        {
            // Более показать не могу
        }

        private void SetFadeDurationComponent()
        {
            // Более показать не могу
        }
    }
}