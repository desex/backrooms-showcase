using System.Collections;
using Ecs.Utils;
using Game.UI.MainMenu;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.UI
{
    public class MainMenuTest : AIntegrationTest
    {
        private const string TEST_SCENE = "MainMenu_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }

        //[UnityTest]
        public IEnumerator VisualMainMenu()
        {
            yield return GameplayUtils.LoadWithoutAutoStart();
            
            yield return GameplayAssert.GameState(EGameState.MainMenu);

            yield return AssertUiViewActive<MainMenuView>();
            
            yield return new WaitForSeconds(3f);

            var mainMenu = Object.FindObjectOfType<MainMenuView>();
            mainMenu.PlayButton.onClick.Invoke();

            yield return GameplayAssert.GameState(EGameState.Game);

            yield return new WaitForSeconds(2f);
        }
    }
}