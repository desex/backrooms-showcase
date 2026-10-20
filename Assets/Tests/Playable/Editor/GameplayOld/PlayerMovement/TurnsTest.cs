using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.Player;
using Db.Score;
using Ecs.Views.Linkable.Impl.Player;
using Game.Services.Providers;
using NUnit.Framework;
using Tests.Helpers.Mock;
using Tests.Playable.Gameplay;
using Tests.Playable.UITest;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;
using Object = UnityEngine.Object;

namespace Tests.Playable.GameplayOld.PlayerMovement
{
    public class PlayerTurnTest : UiTest
    {
        private Mock.PlayerBasicMovementParametersBase _speedParameters;
        private Mock.GenerationResourcesProvider _generationProvider;

        protected override void SubstituteResources()
        {
            var scoreParameters = new Mock.ScoreParameters();
            ResourceSubstitute.AddSubstitute<IScoreParameters>(scoreParameters);
            
            _speedParameters = new Mock.PlayerBasicMovementParametersBase();
            ResourceSubstitute.AddSubstitute<IPlayerBasicMovementParametersBase>(_speedParameters);
        }

        protected override void SubstituteCommonResources()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>("ChunkGenerationSettingsTurnRight");
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);

            _generationProvider = new Mock.GenerationResourcesProvider(chunkGenerationSettings);
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(_generationProvider);
        }

        //[UnityTest]
        public IEnumerator Turn()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(2f);

            yield return CharacterAssert.ReadyToTurn();

            yield return GameplayUtils.Swipe(Vector2.left * 80);

            yield return CharacterAssert.HasTurning();

            yield return CharacterAssert.NotTurning();

            PlayerAssert.Score(1);

            yield return new WaitForSeconds(2f);
        }

        //[UnityTest]
        public IEnumerator DamageAndTurnCurved()
        {
            yield return GameplayUtils.Load();

            yield return CharacterAssert.ReadyToTurn();

            yield return new WaitForSeconds(0.1f);

            yield return GameplayUtils.MakeTurnRight();

            yield return new WaitForSeconds(0.3f);

            yield return GameplayUtils.StartSmoothSwipe(Vector2.right);

            yield return new WaitForSeconds(0.3f);

            yield return GameplayUtils.StopSwipe();

            yield return PlayerAssert.IsDead();
        }

        //[UnityTest]
        public IEnumerator TurnChunkExitWithoutSwipe()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(4f);

            var oldForward = Object.FindObjectOfType<PlayerView>().gameObject.transform.forward;

            yield return GameplayUtils.StartSmoothSwipe(Vector2.left, 2f);

            yield return new WaitForSeconds(3f);

            var currentForward = Object.FindObjectOfType<PlayerView>().gameObject.transform.forward;

            Assert.AreNotEqual(oldForward, currentForward);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator DoubleTurnCheck()
        {
            ModestTree.Assert.IsEqual(true, false); //TODO: delete this when will be repair tests
            
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(2f);

            yield return CharacterAssert.ReadyToTurn();

            yield return GameplayUtils.MakeTurnLeft();

            yield return PlayerAssert.IsDamaged();

            yield return GameplayUtils.MakeTurnLeft();

            //yield return PlayerAssert.IsReadyToMonkeyJump(); //TODO: fix it when will be repair tests
            
            yield return GameplayUtils.MakeJump();

            yield return new WaitForSeconds(0.5f);
        }

        //[UnityTest]
        [Category(TestCategoriesNames.MOVEMENT)]
        public IEnumerator AutomaticTurn_InAllTurnChunks_WhenHoldingSwipe()
        {
            _speedParameters.HorizontalSpeed = 0.5f;

            yield return GameplayUtils.Load();

            // left chunks
            yield return CheckAutomaticTurning(Vector2.left, 2);

            yield return new WaitForSeconds(0.5f);

            // right chunks
            yield return CheckAutomaticTurning(Vector2.right, 2);

            yield return new WaitForSeconds(0.5f);

            // T chunks
            yield return CheckAutomaticTurning(Vector2.left, 2);

            yield return new WaitForSeconds(3f);

            // curva chunk
            yield return CheckAutomaticTurning(Vector2.right, 2);
            yield return CheckAutomaticTurning(Vector2.left, 1);

            yield return new WaitForSeconds(2f);
            
            yield break;

            IEnumerator CheckAutomaticTurning(Vector2 swipeDirection, int countOfTurnings)
            {
                yield return GameplayUtils.StartSmoothSwipe(swipeDirection, 10f);

                for (var i = 0; i < countOfTurnings; i++)
                {
                    yield return PlayerAssert.IsPreTurnReactActive(true, 5f);
                    yield return PlayerAssert.IsTurning(true, 3f);
                    yield return PlayerAssert.IsPreTurnReactActive(false);
                }

                yield return GameplayUtils.StopSwipe();
            }
        }
    }
}