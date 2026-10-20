using System.Collections;
using Ecs.Views.Linkable.Impl.Player;
using Game.UI.Revive;
using Game.Utils.Movement;
using NUnit.Framework;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = ModestTree.Assert;

namespace Tests.Playable.Gameplay.Lose
{
    public class ReviveLogicTest : AIntegrationTest
    {
        private const string TEST_SCENE = "ReviveLogic_TestScene";

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[Category(TestCategoriesNames.REVIVE)]
        public IEnumerator ReviveLogic()
        {
            yield return GameplayUtils.Load();

            yield return AssertReviveAfterLongSlide();
            
            yield return AssertReviveAfterMonkeyJump();
            
            yield return AssertReviveAfterForwardJump();

            yield return AssertReviveAfterClimb();

            yield return AssertReviveAfterWallRun();
            
            yield return AssertReviveAfterTurnLeft();

            yield return new WaitForSeconds(2f);
        }

        private static IEnumerator AssertReviveAfterLongSlide()
        {
            yield return DieReviveAndTeleport();
            
            yield return GameplayUtils.MakeSlide();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.LongSlide);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }
        
        private static IEnumerator AssertReviveAfterMonkeyJump()
        {
            Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            
            yield return DieReviveAndTeleport();
            
            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests

            yield return GameplayUtils.Swipe(new(0, 1f));
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.MonkeyJump);
            //yield return PlayerAssert.IsNotReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }
        
        private static IEnumerator AssertReviveAfterForwardJump()
        {
            yield return DieReviveAndTeleport();

            yield return GameplayUtils.Swipe(new(0, 1f));
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.ForwardJump);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }

        private static IEnumerator AssertReviveAfterWallRun()
        {
            yield return PlayerAssert.IsDead();
            
            yield return DieReviveAndTeleport();

            yield return GameplayUtils.SmoothSwipe(Vector2.left, 1f, 2.5f);
            yield return PlayerAssert.IsNotReadyToWallRun();
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }
        
        private static IEnumerator AssertReviveAfterTurnLeft()
        {
            yield return CharacterAssert.ReadyToTurn();
            
            yield return DieReviveAndTeleport();

            yield return CharacterAssert.ReadyToTurn();

            yield return GameplayUtils.Swipe(Vector2.left * 80);

            yield return CharacterAssert.HasTurning();

            yield return CharacterAssert.NotTurning();

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }
        
        private static IEnumerator AssertReviveAfterClimb()
        {
            yield return PlayerAssert.IsReadyToClimb();
            
            yield return DieReviveAndTeleport();
            
            yield return PlayerAssert.IsReadyToClimb();

            yield return GameplayUtils.Swipe(new(0, 1f));
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Climb);
            yield return PlayerAssert.IsNotReadyToClimb();
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);
        }

        private static IEnumerator DieReviveAndTeleport()
        {
            yield return PlayerAssert.IsDead();
            
            var player = Object.FindObjectOfType<PlayerView>().transform;
            var oldPosition = player.position;

            yield return AssertUiViewActive<ReviveView>();
            
            var reviveView = Object.FindObjectOfType<ReviveView>();
            yield return new WaitForSeconds(1f);
            //reviveView.ReviveButton.onClick.Invoke();
            
            yield return PlayerAssert.IsNotDead();
            
            yield return new WaitForSeconds(0.1f);
            
            var newPosition = player.position;
            Assert.IsNotEqual(oldPosition, newPosition);
        }
    }
}