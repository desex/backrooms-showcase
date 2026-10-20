using System;
using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Rebound;
using Game.Services.DistanceCalculation;
using Game.Services.Obstacles;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Game.Utils.Movement;
using Game.Utils.Rebound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Health
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 941, nameof(EFeatures.Move))]
    public sealed class ApplyDamageReboundSystem : ForEachCommandUpdateSystem<ApplyDamageReboundCommand>
    {
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly ITravelatorService _travelatorService;
        private readonly GameContext _game;
        private readonly IReboundParameters _reboundParameters;

        public ApplyDamageReboundSystem(
            ITravelatorService travelatorService,
            ICommandBuffer commandBuffer,
            GameContext game,
            IDistanceCalculationService distanceCalculationService,
            IReboundParameters reboundParameters,
            IChunkSegmentService chunkSegmentService
        ) : base(commandBuffer)
        {
            _travelatorService = travelatorService;
            _game = game;
            _distanceCalculationService = distanceCalculationService;
            _reboundParameters = reboundParameters;
            _chunkSegmentService = chunkSegmentService;
        }

        protected override void Execute(ref ApplyDamageReboundCommand command)
        {
            var characterEntity = _game.GetEntityWithUid(command.CharacterUid);
            var chunkUid = characterEntity.CurrentChunkId.Value;
            var chunkEntity = _game.GetEntityWithUid(chunkUid);

            Debug.Assert(
                chunkEntity != null,
                $"[{nameof(ApplyDamageReboundSystem)}] CurrentChunkId is null. Check "
                + characterEntity.Link.View.Transform.gameObject.name
            );

            if (!CanRotateInTurnChunk(chunkEntity.ChunkNode.Value.ChunkType, characterEntity))
                return;

            var chunkNode = chunkEntity.ChunkNode.Value;
            var inDownChunk = (chunkNode.ChunkType & EChunkType.Down) != 0;

            var forceType = inDownChunk ? command.ReboundSetup.DownReboundForce : command.ReboundSetup.ReboundForce;

            Debug.Assert(
                forceType != EReboundForce.None,
                $"[{nameof(ApplyDamageReboundSystem)}] Rebound force is none. Check "
                + chunkEntity.Link.View.Transform.gameObject.name
            );

            var reboundForce = _reboundParameters.ReboundForces[forceType];

            var angleType = command.ReboundSetup.ReboundAngle;
            var faceReboundAngle = _reboundParameters.ReboundAngles[angleType];

            var character = _game.GetEntityWithUid(command.CharacterUid);

            _travelatorService.KillTravelatorWork();

            if (!character.IsCanBeDamaged)
                return;

            if (!character.IsReboundState)
                return;

            var backward = -character.Transform.Value.forward;

            var rotationMultiplier = 0f;

            switch (command.ReboundSetup.ReboundDirection)
            {
                case EReboundDirection.Back:
                    rotationMultiplier = 0f;
                    break;

                case EReboundDirection.Left:
                    rotationMultiplier--;
                    break;

                case EReboundDirection.Right:
                    rotationMultiplier++;
                    break;

                case EReboundDirection.Left | EReboundDirection.Right:
                    var chunkSide = _distanceCalculationService.GetChunkSide(character.Position.Value, _chunkSegmentService.Segments);

                    rotationMultiplier = chunkSide switch
                    {
                        EChunkSide.Left => 1f,
                        EChunkSide.Right => -1f,
                        _ => throw new ArgumentOutOfRangeException(),
                    };

                    break;

                default:
                case EReboundDirection.None:
                    throw new Exception(
                        $"[{nameof(ApplyDamageReboundSystem)}] Rebound direction is None/default. Check [{nameof(ReboundModule)}]"
                    );
            }

            rotationMultiplier = -rotationMultiplier;

            var rotation = Quaternion.AngleAxis(faceReboundAngle * rotationMultiplier, Vector3.up);

            Vector3 reboundDirection;

            if (inDownChunk)
            {
                var anchors = chunkEntity.ChunkAnchors.Value;

                var startAnchor = anchors.StartAnchor;
                var endAnchor = anchors.EndOnlyAnchor;

                reboundDirection = rotation * (startAnchor.position - endAnchor.position).normalized * reboundForce;
            }
            else
            {
                reboundDirection = (rotation * backward).normalized * reboundForce;
            }

            character.ReplaceForwardSpeed(new ForwardSpeed(character.ForwardSpeed.Value.Target, 0));
            character.IsReboundState = false;
            character.ReplaceDamageReboundDirection(reboundDirection);

            var directionType = GetReboundDirectionType(
                character,
                reboundDirection,
                command.ReboundSetup.ReboundDirection
            );

            character.ReplaceDamageReboundDirectionType(directionType);
        }

        private EReboundDirection GetReboundDirectionType(
            GameEntity character,
            Vector3 direction,
            EReboundDirection obstacleDirection
        )
        {
            if (obstacleDirection == EReboundDirection.Back)
                return EReboundDirection.Back;

            var forward = character.Transform.Value.forward;
            var directionNormalized = direction.normalized;

            var dot = Vector3.Dot(forward, directionNormalized);
            var cross = Vector3.Cross(forward, directionNormalized);

            if (dot <= -0.99f)
                return EReboundDirection.Back;

            return cross.y < 0 ? EReboundDirection.Left : EReboundDirection.Right;
        }

        private bool CanRotateInTurnChunk(EChunkType chunkType, GameEntity playerEntity)
        {
            if (!ChunkUtility.IsTurn(chunkType))
                return true;

            if (!playerEntity.HasTurnChunkData)
                return true;

            if (playerEntity.IsReadyToTurn)
                return true;

            var direction = playerEntity.TurnChunkData.Value.Direction;

            switch (direction)
            {
                case ETurnDirection.Left:
                case ETurnDirection.Right:
                case ETurnDirection.Both:
                    return false;
            }

            return true;
        }
    }
}