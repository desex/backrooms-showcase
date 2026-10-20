using System.Collections;
using Db.Player;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.PlayerMovement
{
    public class MoveLeftRightTest : AIntegrationTest
    {
        private const string TEST_SCENE = "MoveLeftRight_TestScene";
        
        private Mock.PlayerBasicMovementParametersBase _movementParameters;

        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);

            _movementParameters = new();
            ResourceSubstitute.ReplaceSubstitute<IPlayerBasicMovementParametersBase>(_movementParameters);

            var playerInputParameters = new Mock.PlayerInputParametersBase();
            ResourceSubstitute.ReplaceSubstitute<IPlayerInputParametersBase>(playerInputParameters);
        }
        
        //[UnityTest]
        public IEnumerator MoveLeftRight()
        {
            yield return GameplayUtils.Load(); 
            
            const int swipeCycles = 3;

            for (var i = 0; i < swipeCycles; i++)
            {
                yield return new WaitForSeconds(1f);
                yield return GameplayUtils.StartSmoothSwipe(Vector2.right);
                yield return PlayerAssert.MovingHorizontally(true);
                yield return GameplayUtils.StopSwipe();
                
                yield return new WaitForSeconds(1f);
                
                yield return GameplayUtils.StartSmoothSwipe(Vector2.left);
                yield return PlayerAssert.MovingHorizontally(true);
                yield return GameplayUtils.StopSwipe();
                
            }
            
            yield return new WaitForSeconds(1f);
        }
        
        //[UnityTest]
        public IEnumerator JoystickDeadZone()
        {
            yield return GameplayUtils.Load();
            
            yield return new WaitForSeconds(2f);
            
            yield return GameplayUtils.StartSmoothSwipe(Vector2.right, 1f, 0.01f);
            yield return PlayerAssert.MovingHorizontally(false);
            yield return GameplayUtils.StopSwipe();
            
            yield return new WaitForSeconds(1f);
            
            yield return GameplayUtils.StartSmoothSwipe(Vector2.left);
            yield return PlayerAssert.MovingHorizontally(true, 0.5f);
            yield return GameplayUtils.StopSwipe();

            yield return new WaitForSeconds(1f);
        }
    }
}