using Db.ChasePlayer;
using Game.Services.Chaser;
using Game.Utils.Movement;
using NUnit.Framework;
using System.Collections;
using Tests.Helpers.Mock;
using Tests.Playable.UITest.Utils;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;
using Assert = ModestTree.Assert;

namespace Tests.Playable.Gameplay.Chaser
{
    public class ChaserTests : AIntegrationTest
    {
        private const string CHASER_VELOCITY_CHUNK_GENERATION = "ChaserVelocity_ChunkGenerationSettings";
        private const string CHASER_VELOCITY_SEQUENCE_GENERATION = "ChaserVelosity_SequenceGenerationSettings";
        private const string CHASER_VELOCITY_OBSTACLES_SEQUENCE_CHUNK_GENERATION = "ChaserVelocityObstaclesSequence_ChunkGenerationSettings";
        private const string CHASER_VELOCITY_OBSTACLES_SEQUENCE_SEQUENCE_GENERATION = "ChaserVelosityObstaclesSequence_SequenceGenerationSettings";
        
        protected override void SubstituteResources()
        {
            var chaserPresentationService = new Mock.ChaserPresentationService();
            ResourceSubstitute.ReplaceSubstitute<IChaserPresentationService>(chaserPresentationService);
        }

        [UnityTest]
        [Category(TestCategoriesNames.CHASER)]
        public IEnumerator ChaserVelocity_IsPlayerAndChaserVelocityEquals_InRun()
        {
            SetSequenceGenerationSettings(CHASER_VELOCITY_SEQUENCE_GENERATION);
            SetGenerationSettings(CHASER_VELOCITY_CHUNK_GENERATION);

            yield return GameplayUtils.Load();

            var game = DiUtils.Container.Resolve<GameContext>();
            var chaser = game.ChaserEntity;
            var player = game.PlayerEntity;

            yield return new WaitForSeconds(10f);

            var playerVelocity = player.ForwardSpeed.Value.Current;
            var chaserVelocity = chaser.ForwardSpeed.Value.Current;
            
            Assert.IsApproximately(playerVelocity, chaserVelocity, 0.1f);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.CHASER)]
        public IEnumerator ChaserVelocity_IsPlayerAndChaserVelocityEquals_AfterPassingObstacle()
        {
            SetSequenceGenerationSettings(CHASER_VELOCITY_OBSTACLES_SEQUENCE_SEQUENCE_GENERATION);
            SetGenerationSettings(CHASER_VELOCITY_OBSTACLES_SEQUENCE_CHUNK_GENERATION);

            yield return GameplayUtils.Load();

            var game = DiUtils.Container.Resolve<GameContext>();
            var chaser = game.ChaserEntity;
            var player = game.PlayerEntity;

            yield return PassMonkeyJump();

            yield return new WaitForSeconds(0.5f);
            
            yield return PassMonkeyJump();
            
            yield return new WaitForSeconds(1f);

            var playerVelocity = player.ForwardSpeed.Value.Current;
            var chaserVelocity = chaser.ForwardSpeed.Value.Current;
            
            Assert.IsApproximately(playerVelocity, chaserVelocity, 0.1f);
        }

        [UnityTest]
        [Category(TestCategoriesNames.CHASER)]
        public IEnumerator ChaserKill_IsPlayerDead()
        {
            SetSequenceGenerationSettings(CHASER_VELOCITY_OBSTACLES_SEQUENCE_SEQUENCE_GENERATION);
            SetGenerationSettings(CHASER_VELOCITY_OBSTACLES_SEQUENCE_CHUNK_GENERATION);

            var chaserParameters = new Mock.ChasePlayerParameters();
            ResourceSubstitute.ReplaceSubstitute<IChasePlayerParameters>(chaserParameters);

            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsDamaged(60f);

            yield return PlayerAssert.IsDead();

            yield return new WaitForSeconds(0.5f);
        }

        private IEnumerator PassMonkeyJump()
        {
            yield return PlayerAssert.ReadyToPassObstacle(EObstaclePassType.MonkeyJump, 20f);

            yield return GameplayUtils.MakeJump();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }
    }
}