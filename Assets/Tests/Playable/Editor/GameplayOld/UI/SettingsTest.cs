using System.Collections;
using Ecs.Utils;
using Game.DaoData;
using Game.UI.MainMenu;
using Game.UI.SettingsPopup;
using NUnit.Framework;
using PdUtils.Dao;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.UI
{
    public class SettingsTest : AIntegrationTest
    {
        private const string TEST_SCENE = "MainMenu_TestScene";
        
        protected override void SubstituteResources()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            var settingsDao = new Mock.Dao<SettingsData>();
            settingsDao.Save(new (){IsNotMutedVolume = false, IsVibrationEnabled = false, Sensitivity = 1});
            ResourceSubstitute.AddSubstitute<IDao<SettingsData>>(settingsDao);
        }
        
        //[UnityTest]
        [Category(TestCategoriesNames.UI)]
        [Category(TestCategoriesNames.SETTINGS)]
        public IEnumerator VisualSettingsClose()
        {
            yield return GameplayUtils.LoadWithoutAutoStart();
            
            yield return GameplayAssert.GameState(EGameState.MainMenu);

            yield return AssertUiViewActive<MainMenuView>();
            
            yield return new WaitForSeconds(3f);

            var mainMenu = Object.FindObjectOfType<MainMenuView>();
            mainMenu.SettingsButton.onClick.Invoke();

            yield return AssertUiViewActive<SettingsPopupView>();

            var settingsPopup = Object.FindObjectOfType<SettingsPopupView>();
            yield return new WaitForSeconds(0.5f);
            settingsPopup.CloseButton.onClick.Invoke();
            
            yield return new WaitForSeconds(0.5f);
            
            mainMenu.SettingsButton.onClick.Invoke();
            
            yield return new WaitForSeconds(0.5f);
            
            settingsPopup.BackgroundButton.onClick.Invoke();

            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.UI)]
        [Category(TestCategoriesNames.SETTINGS)]
        public IEnumerator SwitchWorkTest()
        {
            yield return GameplayUtils.LoadWithoutAutoStart();
            
            yield return GameplayAssert.GameState(EGameState.MainMenu);

            yield return AssertUiViewActive<MainMenuView>();
            
            yield return new WaitForSeconds(3f);

            var mainMenu = Object.FindObjectOfType<MainMenuView>();
            mainMenu.SettingsButton.onClick.Invoke();

            yield return AssertUiViewActive<SettingsPopupView>();

            var settingsPopup = Object.FindObjectOfType<SettingsPopupView>();
            var soundToggle = settingsPopup.SoundToggle;
            var vibrationToggle = settingsPopup.VibrationToggle;
            
            yield return new WaitForSeconds(0.5f);
            
            Assert.AreEqual(false, soundToggle.IsActive);
            Assert.AreEqual(false, vibrationToggle.IsActive);

            soundToggle.Toggle.isOn = true;
            vibrationToggle.Toggle.isOn = true;
            
            settingsPopup.CloseButton.onClick.Invoke();
            
            yield return new WaitForSeconds(0.5f);
            
            mainMenu.SettingsButton.onClick.Invoke();
            
            yield return new WaitForSeconds(0.5f);
            
            Assert.AreEqual(true, soundToggle.IsActive);
            Assert.AreEqual(true, vibrationToggle.IsActive);
        }
    }
}