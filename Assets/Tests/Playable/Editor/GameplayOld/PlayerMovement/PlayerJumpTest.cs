using System.Collections;
using Db.Player;
using Ecs.Views.Linkable.Impl.Player;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class PlayerJumpTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Jump_TestScene";

        private Mock.PlayerSlideAndJumpParameters _playerSlideAndJumpParameters;
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);

            _playerSlideAndJumpParameters = new Mock.PlayerSlideAndJumpParameters();
            ResourceSubstitute.AddSubstitute<IPlayerSlideAndJumpParameters>(_playerSlideAndJumpParameters);
        }

        //[UnityTest]
        public IEnumerator PlayerJump()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();

            yield return new WaitForSeconds(1);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            yield return PlayerAssert.IsNotGrounded();

            yield return PlayerAssert.IsGrounded();
        }
        
        //[UnityTest]
        public IEnumerator MoveInJump()
        {
            yield return GameplayUtils.Load();
            
            _playerSlideAndJumpParameters.JumpHeight = 0.15f;
            
            yield return PlayerAssert.IsGrounded();
            
            var playerTransform = Object.FindObjectOfType<PlayerView>().GetComponent<Transform>();

            yield return new WaitForSeconds(1);

            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.IsNotGrounded();

            var startPosition = playerTransform.position.x;
            
            yield return GameplayUtils.SmoothSwipe(Vector2.right, 2f, 2f);
            
            yield return PlayerAssert.IsGrounded();
            Debug.Log($"playerTransform.position.x = {playerTransform.position.x} startPosition = {startPosition} !!"); //TODO: delete
            Assert.Greater(playerTransform.position.x, startPosition);
            
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.IsNotGrounded();
            
            startPosition = playerTransform.position.x;
            
            yield return GameplayUtils.SmoothSwipe(Vector2.left, 2f, 2f);

            yield return PlayerAssert.IsGrounded();
            Debug.Log($"playerTransform.position.x = {playerTransform.position.x} startPosition = {startPosition} !!"); //TODO: delete
            Assert.Less(playerTransform.position.x, startPosition);
            
            yield return new WaitForSeconds(2);
        }
    }
}