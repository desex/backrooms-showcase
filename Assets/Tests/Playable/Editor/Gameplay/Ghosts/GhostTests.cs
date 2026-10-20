using System.Collections;
using Db.Ghost;
using Game.Services.Chaser;
using Game.Utils.Ghost;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Ghosts
{
    public class GhostTests : AIntegrationTest
    {
        private const string MONKEY_JUMP_OBSTACLE_GENERATION_SETTINGS = "MonkeyJumpObstacle_GenerationSettings";
        private const string SINGLE_JUMP_HOLE_GENERATION_SETTINGS = "SingleJumpHoleObstacle_GenerationSettings";
        private const string DOUBLE_JUMP_HOLE_GENERATION_SETTINGS = "DoubleJumpHole_GenerationSettings";

        private Mock.GhostsParameters _ghostParametersMock;


        protected override void SubstituteResources()
        {
            var chaserServiceMock = new Mock.ChaserPresentationService();
            ResourceSubstitute.AddSubstitute<IChaserPresentationService>(chaserServiceMock);
            
            _ghostParametersMock = new()
            {
                GhostCount = 1,
            };
            ResourceSubstitute.AddSubstitute<IGhostsParameters>(_ghostParametersMock);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.GHOST)]
        public IEnumerator GhostSuccessfullyPassObstacle_WhenZeroFailChances()
        {
            SetGenerationSettings(MONKEY_JUMP_OBSTACLE_GENERATION_SETTINGS);
            
            _ghostParametersMock.ObstacleFailChance = Vector2.zero;
            _ghostParametersMock.RandomTiltPointSidesDeltaRange = Vector2.zero;
            _ghostParametersMock.RandomTiltMissPointSidesDeltaRange = Vector2.zero;
            
            yield return GameplayUtils.Load();

            yield return GhostAssert.Alive();

            yield return GhostAssert.HasReadyForObstaclePass(true);
            yield return GhostAssert.IsDamaged(false);
            
            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Success);
            
            yield return GhostAssert.HasReadyForObstaclePass(false);
            yield return GhostAssert.IsDamaged(false);
            
            yield return GhostAssert.Alive();

            yield return new WaitForSeconds(2f);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.GHOST)]
        public IEnumerator GhostFailPassObstacle_WhenMaxFailChances()
        {
            SetGenerationSettings(MONKEY_JUMP_OBSTACLE_GENERATION_SETTINGS);
            
            _ghostParametersMock.ObstacleFailChance = Vector2.one;
            _ghostParametersMock.RandomTiltPointSidesDeltaRange = Vector2.zero;
            _ghostParametersMock.RandomTiltMissPointSidesDeltaRange = Vector2.zero;
            
            yield return GameplayUtils.Load();

            yield return GhostAssert.Alive();
            
            yield return GhostAssert.HasReadyForObstaclePass(true);
            yield return GhostAssert.IsDamaged(false);

            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Fail);

            yield return GhostAssert.HasReadyForObstaclePass(false);
            yield return GhostAssert.IsDamaged(true);

            yield return new WaitForSeconds(2f);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.GHOST)]
        public IEnumerator GhostSuccessfullySingleJump_WhenZeroFailChances()
        {
            SetGenerationSettings(SINGLE_JUMP_HOLE_GENERATION_SETTINGS);
            
            _ghostParametersMock.JumpFailChance = Vector2.zero;
            _ghostParametersMock.RandomTiltPointSidesDeltaRange = Vector2.zero;
            _ghostParametersMock.RandomTiltMissPointSidesDeltaRange = Vector2.zero;
            
            yield return GameplayUtils.Load();

            yield return GhostAssert.Alive();
            
            yield return GhostAssert.HasNeedJumpType(EGhostSimpleJumpType.SingleJump);
            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Success);
            
            yield return GhostAssert.IsDamaged(false);
            yield return GhostAssert.IsGrounded(false);
            
            yield return GhostAssert.HasJumping(EJumpType.Jump);
            
            yield return GhostAssert.IsGrounded(true);
            yield return GhostAssert.IsDamaged(false);
            yield return GhostAssert.NotHasNeedJump();

            yield return new WaitForSeconds(2f);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.GHOST)]
        public IEnumerator GhostSuccessfullyDoubleJump_WhenZeroFailChances()
        {
            SetGenerationSettings(DOUBLE_JUMP_HOLE_GENERATION_SETTINGS);
            
            _ghostParametersMock.JumpFailChance = Vector2.zero;
            _ghostParametersMock.RandomTiltPointSidesDeltaRange = Vector2.zero;
            _ghostParametersMock.RandomTiltMissPointSidesDeltaRange = Vector2.zero;
            
            yield return GameplayUtils.Load();

            yield return GhostAssert.Alive();
            
            yield return GameplayUtils.SmoothSwipe(Vector2.left, distance: 0.5f);
            
            yield return GhostAssert.HasNeedJumpType(EGhostSimpleJumpType.DoubleJump);
            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Success);
            
            yield return GhostAssert.IsDamaged(false);
            yield return GhostAssert.IsGrounded(false);
            
            yield return GhostAssert.HasJumping(EJumpType.Jump);
            yield return GhostAssert.HasJumping(EJumpType.DoubleJump);
            
            yield return GhostAssert.IsGrounded(true);
            yield return GhostAssert.IsDamaged(false);
            yield return GhostAssert.NotHasNeedJump();

            yield return new WaitForSeconds(2f);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.GHOST)]
        public IEnumerator GhostFailJumpAndFailComeback_WhenMaxFailChances()
        {
            SetGenerationSettings(DOUBLE_JUMP_HOLE_GENERATION_SETTINGS);
            
            _ghostParametersMock.JumpFailChance = Vector2.one;
            _ghostParametersMock.ComebackFailChance = Vector2.one;
            _ghostParametersMock.FailureMovementIncreaseChanceDelta = 0f;
            _ghostParametersMock.SuccessMovementDecreaseChanceDelta = 0f;
            _ghostParametersMock.RandomTiltPointSidesDeltaRange = Vector2.zero;
            _ghostParametersMock.RandomTiltMissPointSidesDeltaRange = Vector2.zero;
            
            yield return GameplayUtils.Load();

            yield return GhostAssert.Alive();
            
            yield return GameplayUtils.SmoothSwipe(Vector2.left, distance: 0.5f);
            
            yield return GhostAssert.HasNeedJumpType(EGhostSimpleJumpType.DoubleJump);
            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Fail);
            yield return GhostAssert.NotHasNeedJump();
            
            yield return GhostAssert.IsGrounded(false);
            
            yield return GhostAssert.HasReadyForObstaclePass(true);
            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Fail);
            yield return GhostAssert.HasReadyForObstaclePass(false);

            yield return GhostAssert.Dead();

            yield return new WaitForSeconds(2f);
        }
        
        [UnityTest]
        [Category(TestCategoriesNames.GHOST)]
        public IEnumerator GhostFailJumpAndSuccessfullyComeback_WhenMaxFailJumpChancesAndZeroFailComebackChances()
        {
            SetGenerationSettings(DOUBLE_JUMP_HOLE_GENERATION_SETTINGS);
            
            _ghostParametersMock.JumpFailChance = Vector2.one;
            _ghostParametersMock.ComebackFailChance = Vector2.zero;
            _ghostParametersMock.FailureMovementIncreaseChanceDelta = 0f;
            _ghostParametersMock.SuccessMovementDecreaseChanceDelta = 0f;
            _ghostParametersMock.RandomTiltPointSidesDeltaRange = Vector2.zero;
            _ghostParametersMock.RandomTiltMissPointSidesDeltaRange = Vector2.zero;
            
            yield return GameplayUtils.Load();

            yield return GhostAssert.Alive();
            
            yield return GameplayUtils.SmoothSwipe(Vector2.left, distance: 0.5f);
            
            yield return GhostAssert.HasNeedJumpType(EGhostSimpleJumpType.DoubleJump);
            yield return GhostAssert.NotHasNeedJump();

            yield return GhostAssert.HasReadyForObstaclePass(true);
            yield return GhostAssert.MadeDecision(EGhostSelectedActionResult.Success);
            
            yield return GhostAssert.HasReadyForObstaclePass(false);
            yield return GhostAssert.IsGrounded(true);
            yield return GhostAssert.Alive();

            yield return new WaitForSeconds(2f);
        }
    }
}