using System.Collections;
using Db.Player;
using Ecs.Utils;
using Game.UI.Death;
using Game.UI.MainMenu;
using Project.Ui.Loading.Views;
using Tests.Helpers.Mock;
using Tests.Playable.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay.Lose
{
    public class LoseTest : AIntegrationTest
    {
        private const string TEST_SCENE = "Lose_TestScene";
        private const string TEST_CURVED_CORRIDOR_SCENE = "LoseCurvedCoridor_TestScene";
        
        protected override void SubstituteResources()
        {
            var playerHealthParameters = new Mock.PlayerHealthParametersBase()
            {
                TimeBetweenInjuries = 999f,
            };
            
            ResourceSubstitute.ReplaceSubstitute<IPlayerHealthParametersBase>(playerHealthParameters);
        }

        //[UnityTest]
        public IEnumerator DeathScreenCheckAndRestart()
        {
            //ScenesBase.SetScene(TEST_SCENE);
            
            yield return GameplayUtils.Load();

            yield return PlayerAssert.IsDamaged();
            
            yield return new WaitForSeconds(2f);

            yield return PlayerAssert.IsDead();
            
            yield return new WaitForSeconds(5.5f);
            
            yield return AssertUiViewActive<DeathView>();

            var deathView = Object.FindObjectOfType<DeathView>();
            
            yield return new WaitForSeconds(1f);
            
            deathView.RestartButton.onClick.Invoke();
            
            yield return AssertUiViewActive<ReloadingView>(true);
            
            yield return AssertUiViewInactive<MainMenuView>();
            
            yield return AssertUiViewInactive<ReloadingView>(true);

            yield return PlayerAssert.IsNotDamaged();
            
            yield return AssertUiViewInactive<MainMenuView>();

            yield return GameplayAssert.GameState(EGameState.Game);

            yield return new WaitForSeconds(1f);
        }

        //[UnityTest]
        public IEnumerator NonImmediatelyDeathInCurvedCorridor()
        {
            //ScenesBase.SetScene(TEST_CURVED_CORRIDOR_SCENE);

            yield return GameplayUtils.Load();
            
            yield return PlayerAssert.IsDamaged();

            yield return PlayerAssert.IsNotDead();

            yield return PlayerAssert.IsDead();
        }
    }
}