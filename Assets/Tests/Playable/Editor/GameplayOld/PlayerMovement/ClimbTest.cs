using System.Collections;
using Db.Score;
using Ecs.Views.Linkable.Impl.Obstacles;
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
    public class ClimbTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Climb_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
        }
        
        //[UnityTest]
        public IEnumerator FastClimbSuccess()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsReadyToClimb();
            yield return PlayerAssert.IsReadyForFastActionMove();
            
            yield return new WaitForSeconds(0.1f);
            
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.IsInFastActionMove();
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Climb);
            
            yield return new WaitForSeconds(0.3f);
            
            yield return PlayerAssert.IsNotReadyToClimb();
            yield return PlayerAssert.IsNotDead();
            yield return PlayerAssert.IsNotDamaged(2);
            
            PlayerAssert.Score(2);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);

            var playerView = Object.FindObjectOfType<PlayerView>();
            yield return null;
            
            //TODO: removed old climb logic and add obstacle pass
            //
            // var climbView = Object.FindObjectOfType<ClimbObstacleView>();
            // yield return null;
            //
            // yield return new WaitForSeconds(2f);
            //
            // Assert.Greater(playerView.transform.position.z, climbView.transform.position.z);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator NormalClimbSuccess()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsReadyToClimb();
            yield return PlayerAssert.IsNotInFastActionMove();
            
            yield return GameplayUtils.Swipe(Vector2.up * 80);
            
            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Climb);
            
            yield return PlayerAssert.IsNotReadyToClimb();
            yield return PlayerAssert.IsNotDead();
            yield return PlayerAssert.IsNotDamaged(2);
            
            PlayerAssert.Score(1);

            yield return PlayerAssert.BodyMoveType(EBodyMoveType.Run);

            yield return new WaitForSeconds(2f);
        }
        
        //[UnityTest]
        public IEnumerator ClimbFail()
        {
            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsGrounded();
            yield return PlayerAssert.IsReadyToClimb();
            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(1f);
        }
        
    }
}