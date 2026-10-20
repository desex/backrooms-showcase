using System.Collections;
using Db.Player;
using Ecs.Views.Linkable.Impl.Player;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class DoubleJumpTest : AIntegrationTest
    {
        private Mock.PlayerSlideAndJumpParameters _slideAndJumpParameters;
        private const string TEST_SCENE = "DoubleJump_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);

            _slideAndJumpParameters = new Mock.PlayerSlideAndJumpParameters
            {
                DoubleJumpHeight = 0.07f,
            };
            ResourceSubstitute.AddSubstitute<IPlayerSlideAndJumpParameters>(_slideAndJumpParameters);
        }
        
        //[UnityTest]
        public IEnumerator DoubleJumpLogic()
        {
            yield return GameplayUtils.Load();

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.Jump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);
            
            yield return new WaitForSeconds(0.2f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.DoubleJump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run, 2f);
            yield return PlayerAssert.NotJumping();
            
            yield return PlayerAssert.IsReadyForFastActionMove();

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.JumpOverObstacle);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);

            yield return new WaitForSeconds(0.2f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.DoubleJump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            yield return PlayerAssert.NotJumping();
            
            yield return new WaitForSeconds(0.3f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.Jump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);

            yield return new WaitForSeconds(0.2f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.DoubleJump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
            yield return PlayerAssert.NotJumping();

            yield return PlayerAssert.IsNotDamaged(0.5f);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator RollAfterDoubleJump()
        {
            yield return GameplayUtils.Load();
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right, 1f, 1.5f);
            
            yield return new WaitForSeconds(0.1f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.Jump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);
            
            yield return new WaitForSeconds(0.3f);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.Jumping(EJumpType.DoubleJump);
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Jump, 1f);
            
            yield return new WaitForSeconds(0.1f);
            
            yield return GameplayUtils.Swipe(Vector2.down * 80);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.InterruptFall, 0.5f);
            
            yield return PlayerAssert.NotJumping();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Roll, 0.5f);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator MoveInDoubleJump()
        {
            yield return GameplayUtils.Load();
            
            _slideAndJumpParameters.JumpHeight = 0.15f;
            
            yield return PlayerAssert.IsGrounded();
            
            var playerTransform = Object.FindObjectOfType<PlayerView>().GetComponent<Transform>();

            yield return new WaitForSeconds(1);

            yield return DoubleJump();
            
            yield return PlayerAssert.IsNotGrounded();

            var startPosition = playerTransform.position.x;
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right, 2f, 2f);
            
            yield return PlayerAssert.IsGrounded();
            
            Assert.Greater(playerTransform.position.x, startPosition);

            yield return DoubleJump();
            
            yield return PlayerAssert.IsNotGrounded();
            
            startPosition = playerTransform.position.x;
            
            yield return GameplayUtils.SmoothSwipe(Vector2.left, 2f, 2f);

            yield return PlayerAssert.IsGrounded();

            Assert.Less(playerTransform.position.x, startPosition);
            
            yield return new WaitForSeconds(2);

            yield break;

            IEnumerator DoubleJump()
            {
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return new WaitForSeconds(0.2f);
                yield return GameplayUtils.Swipe(Vector2.up * 80);
                yield return PlayerAssert.Jumping(EJumpType.DoubleJump, 0.5f);
            }
        }
    }
}