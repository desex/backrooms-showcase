#if !DISABLE_GHOSTS
using Db.Ghost;
using Ecs.Action.Commands.Ghosts;
using Ecs.Utils;
using Game.Utils.Ghost;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;

namespace Ecs.Action.Systems.Ghost
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 699, nameof(EFeatures.Ghost))]
    public class RandomNewGhostChancesSystem : ForEachCommandUpdateSystem<RandomNewGhostChancesCommand>
    {
        private readonly IGhostsParameters _ghostsParameters;
        private readonly IRandomProvider _randomProvider;
        private readonly GameContext _game;

        public RandomNewGhostChancesSystem(
            ICommandBuffer commandBuffer, 
            IGhostsParameters ghostsParameters, 
            IRandomProvider randomProvider, 
            GameContext game
        ) : base(commandBuffer)
        {
            _ghostsParameters = ghostsParameters;
            _randomProvider = randomProvider;
            _game = game;
        }

        protected override void Execute(ref RandomNewGhostChancesCommand command)
        {
            var ghost = _game.GetEntityWithUid(command.GhostUid);

            var obstacleFastMoveChances = _ghostsParameters.ObstacleFastMoveChance;
            var obstacleFastMoveChance = _randomProvider.Range(obstacleFastMoveChances.x, obstacleFastMoveChances.y);
            
            var obstacleFailChances = _ghostsParameters.ObstacleFailChance;
            var obstacleFailChance = _randomProvider.Range(obstacleFailChances.x, obstacleFailChances.y);
            
            var comebackFailChances = _ghostsParameters.ComebackFailChance;
            var comebackFailChance = _randomProvider.Range(comebackFailChances.x, comebackFailChances.y);
            
            var jumpFailChances = _ghostsParameters.JumpFailChance;
            var jumpFailChance = _randomProvider.Range(jumpFailChances.x, jumpFailChances.y);

            var turnFailChances = _ghostsParameters.TurningFailChance;
            var turnFailChance = _randomProvider.Range(turnFailChances.x, turnFailChances.y);

            var chanceData = new GhostChancesData(
                jumpFailChance,
                comebackFailChance,
                turnFailChance,
                obstacleFailChance,
                obstacleFastMoveChance
            );
            
            ghost.ReplaceGhostChances(chanceData);
        }
    }
}
#endif