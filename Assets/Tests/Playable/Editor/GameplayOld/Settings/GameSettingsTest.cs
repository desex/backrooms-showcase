using System.Collections;
using Game.UI.MainMenu;
using Game.UI.SettingsPopup;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playable.Gameplay.Settings
{
    public class GameSettingsTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Settings_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
        }
        
        //[UnityTest]
        public IEnumerator OpenCloseSettingsWindow()
        {
            yield return GameplayUtils.LoadWithoutAutoStart();
            
            yield return new WaitForSeconds(2);

            yield return AssertUiViewActive<MainMenuView>();
            var mainMenuView = Object.FindObjectOfType<MainMenuView>();
            
            yield return new WaitForSeconds(2);

            mainMenuView.SettingsButton.onClick?.Invoke();
            
            yield return AssertUiViewActive<SettingsPopupView>();
            var settingsPopupView = Object.FindObjectOfType<SettingsPopupView>();
            
            yield return new WaitForSeconds(2);
            
            settingsPopupView.CloseButton.onClick?.Invoke();
            
            yield return new WaitForSeconds(2);
        }
    }
}