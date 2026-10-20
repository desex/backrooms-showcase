using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Db.Obstacles;
using Db.Player;
using Ecs.Action.Commands.Obstacles;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.General;
using Game.Services.Revive;
using Game.Utils.Movement;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;
using UnityEngine;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 930, nameof(EFeatures.Move))]
    public class HitObstacleSystem : ForEachCommandUpdateSystem<HitObstacleCommand>
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IObstacleParametersBase _obstacleParametersBase;
        private readonly IRandomProvider _randomProvider;
        private readonly IReboundParameters _reboundParameters;
        private readonly IReviveObstacleService _reviveObstacleService;

        public HitObstacleSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            ILinkedEntityRepository linkedEntityRepository,
            IObstacleParametersBase obstacleParametersBase,
            IAnalyticsService analyticsService,
            IReviveObstacleService reviveObstacleService,
            IRandomProvider randomProvider,
            IReboundParameters reboundParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _commandBuffer = commandBuffer;
            _linkedEntityRepository = linkedEntityRepository;
            _obstacleParametersBase = obstacleParametersBase;
            _analyticsService = analyticsService;
            _reviveObstacleService = reviveObstacleService;
            _randomProvider = randomProvider;
            _reboundParameters = reboundParameters;
        }

        protected override void Execute(ref HitObstacleCommand command)
        {
            var hitObstacle = command.Value.ObjectHash;
            var hasEntity = _linkedEntityRepository.TryGet(hitObstacle, out var obstacle);

            if (!hasEntity)
                return;

            var character = _game.GetEntityWithUid(command.Value.CharacterUid);

            if (!character.HasCurrentChunkId)
                return;

            if (character.IsDead)
                return;

            if (character.IsPlayer)
                _analyticsService.HandleObstacleHit(obstacle.Uid.Value);

            if (_game.GetEntityWithUid(character.CurrentChunkId.Value) == null)
            {
                if (character.IsPlayer)
                    Debug.LogError(
                        "HitObstacleSystem: CurrentChunkId is null. Check "
                        + character.Link.View.Transform.gameObject.name
                    );

                return;
            }

            DeactivateObstacleGroupChildren(obstacle);
            _reviveObstacleService.UpdateReviveDataOnPlayer(character, obstacle);

            var moveDirection = command.Value.MoveDirection;
            HandleCollision(command, obstacle, character, moveDirection);

            var tutorial = _game.TutorialEntity;

            if (!tutorial.IsInTutorial)
                character.IsCanSwipeVertical = character.HasSlideChunk;
        }

        private void HandleCollision(
            HitObstacleCommand command,
            GameEntity obstacle,
            GameEntity character,
            Vector3 moveDirection
        )
        {
            if (obstacle.IsKillingInstantly && character.IsProtectedFromInstantKill)
                _commandBuffer.ReceiveDamage(character.Uid.Value, moveDirection);
            else if (obstacle.IsKillingInstantly)
                _commandBuffer.KillPlayer(character.Uid.Value, EDeathReason.Obstacle, false);
            else
                Rebound(command, character, moveDirection);
        }

        private void Rebound(HitObstacleCommand command, GameEntity character, Vector3 moveDirection)
        {
            if (character.IsReadyToTurn)
            {
                _commandBuffer.ReceiveDamage(character.Uid.Value, moveDirection);
                _commandBuffer.ApplyDamageRebound(character.Uid.Value, command.Value.ReboundSetup);

                var delay = _reboundParameters.AutoTurnDelay;
                DelayedInvocationService.Invoke(() => Turn(character), delay);
            }
            else
            {
                if (!character.IsCanBeDamaged)
                    return;

                if (!character.IsReboundState)
                    return;

                _commandBuffer.ReceiveDamage(character.Uid.Value, moveDirection);
                _commandBuffer.ApplyDamageRebound(character.Uid.Value, command.Value.ReboundSetup);
            }
        }

        private EHorizontalMoveDirection GetTurnDirection(GameEntity character)
        {
            var randomDirection = _randomProvider.Range(0, 1) == 0
                ? EHorizontalMoveDirection.Left
                : EHorizontalMoveDirection.Right;

            var turnData = character.TurnChunkData.Value;
            var turnDirection = turnData.Direction;

            return turnDirection switch
            {
                ETurnDirection.Left => EHorizontalMoveDirection.Left,
                ETurnDirection.Right => EHorizontalMoveDirection.Right,
                ETurnDirection.Both => randomDirection,
                _ => throw new Exception(
                    $"[{nameof(HitObstacleSystem)}] You should not autoTurn in this turn direction {turnDirection}"
                ),
            };
        }

        private void Turn(GameEntity character)
        {
            var direction = GetTurnDirection(character);
            character.ReplaceHorizontalMoveDirection(direction);
            _commandBuffer.StartPlayerTurn(character.Uid.Value);
        }

        private void DeactivateObstacleGroupChildren(IOwnerEntity obstacle)
        {
            var obstacleOwnerUid = obstacle.Owner.Uid;
            var obstacles = _game.GetEntitiesWithOwner(obstacleOwnerUid);

            SetObstaclesState(obstacles, false);

            var obstacleReactivationDelay = _obstacleParametersBase.ReactivationDelay;
            CancelReactivateObstacleAfterDelay(obstacleReactivationDelay, obstacles).ForgetAndWrapExceptions();
        }

        private async UniTask CancelReactivateObstacleAfterDelay(float delay, HashSet<GameEntity> obstacles)
        {
            await UniTaskUtils.PausableDelay(delay, () => _game.PauseEntity.Pause.Value);
            SetObstaclesState(obstacles, true);
        }

        private static void SetObstaclesState(HashSet<GameEntity> obstacles, bool state)
        {
            foreach (var children in obstacles)
            {
                if (children.IsObstacle)
                    children.IsActive = state;
            }
        }
    }
}