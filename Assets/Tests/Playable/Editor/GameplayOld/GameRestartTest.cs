#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using System.Collections;
using Game.UI.GameRestartButton;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay
{
    public class GameRestartTest : AIntegrationTest
    {
        private const string TEST_SCENE = "PlayerRunTest_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator RestartGame()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(3);
            
            yield return AssertUiViewActive<DebugView>();

            var restartButton = Object.FindObjectOfType<DebugView>();
            restartButton.RestartWithoutTutorialButton.onClick?.Invoke();

            yield return new WaitForSeconds(3);
        }
    }
}
#endif