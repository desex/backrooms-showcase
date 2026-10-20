using System.Collections.Generic;
using System.Threading;
using Db.Tutorial;
using Db.Tutorial.StateVos;
using Ecs.Managers;
using Ecs.Utils.Groups;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Game.Services.Hint;
using Game.Services.Player;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.UI;
using Game.Utils.ColorsUtils;
using Game.Utils.MovementBlock;
using Game.Utils.Tutorial;
using JCMG.EntitasRedux.Commands;
using R3;
using SimpleUi.Signals;
using UnityEngine;
using Zenject;

namespace Game.Utils.States.Impl
{
    public class DashTutorialState : ATutorialState
    {
        private readonly Dictionary<Uid, Vector3> _almondWaterPositionsByUid = new();
        private readonly HashSet<Vector3> _almondWaterPositionsToRespawn = new();
        private readonly ReactiveProperty<EDashSubStates> _dashSubState = new();
        
        private readonly ITutorialHintService _tutorialHintService;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly SignalBus _signalBus;
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly ICollectableSpawnService _collectableSpawnService;

        private Vector3 _closestToFirstWallWaterPosition;
        private Vector3 _closestToSecondWallWaterPosition;
        private Vector3? _chunkStartPosition;
        
        private TutorialDashParameters DashParameters => Parameters.DashParameters; 
        private EBlockedMovement EnterMovementsWithSlide => BlockedMovementsOnFirstEnter | EBlockedMovement.Slide;
        private EBlockedMovement EnterMovementsWithSlideAndJumps => EnterMovementsWithSlide | EBlockedMovement.Jump | EBlockedMovement.DoubleJump;
        private EBlockedMovement EnterMovementsWithoutDash => BlockedMovementsOnFirstEnter.WithUnlockedMovement(EBlockedMovement.Dash);
        
        public DashTutorialState(
            ITutorialParameters parameters,
            GameContext game,
            ITimeScaleService timeScaleService,
            ICommandBuffer commandBuffer,
            ETutorialState stateType,
            ITutorialHintService tutorialHintService,
            IPlayerMovementTypeService playerMovementTypeService,
            ITutorialService tutorialService,
            IGameGroupUtils gameGroupUtils,
            SignalBus signalBus,
            ICollectiblesStorage collectiblesStorage,
            ICollectableSpawnService collectableSpawnService
        ) : base(parameters,
                 game,
                 timeScaleService,
                 commandBuffer,
                 stateType,
                 tutorialHintService,
                 playerMovementTypeService,
                 tutorialService
        )
        {
            _tutorialHintService = tutorialHintService;
            _gameGroupUtils = gameGroupUtils;
            _signalBus = signalBus;
            _collectiblesStorage = collectiblesStorage;
            _collectableSpawnService = collectableSpawnService;
        }

        protected override void OnEnter()
        {
            // Более показать не могу
        }

        protected override void OnRestart()
        {
            // Более показать не могу
        }

        protected override void OnExit()
        {
            // Более показать не могу
        }
        
        private void OnPickUpCollectableType(GameEntity entity, ECollectableType value)
        {
            // Более показать не могу
        }

        private void OnDash(GameEntity entity)
        {
            // Более показать не могу
        }

        private void OnDashSubState(EDashSubStates newState)
        {
            // Более показать не могу
        }

        private void CheckDistances()
        {
            // Более показать не могу
        }

        private void OnWaterPickedUp(GameEntity entity)
        {
            // Более показать не могу
        }

        private (Vector3 closestToFirstWallWaterPosition, Vector3 closestToSecondWallWaterPosition) GetInitializedTargetAlmondWatersPositions()
        {
            // Более показать не могу
            return default;
        }

        private void FindClosestAlmondWatersEntities(in List<GameEntity> closestAlmondWaters, int targetCount)
        {
            // Более показать не могу
        }
    }
}