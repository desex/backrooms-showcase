using System;
using System.Collections;
using System.Linq;
using Db.Graphics;
using Ecs.Utils;
using Game.Services.Render;
using Game.Services.Score;
using Game.Services.Sound;
using Game.UI.Death;
using Game.UI.MainMenu;
using Game.Ui.PlayerMovementInput;
using Game.UI.Revive;
using Game.Utils.Render;
using Game.Views.Lamps;
using Project.Render.RendererFeatures.Impl;
using Tests.Playable.UITest;
using Tests.Playable.UITest.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Tests.Playable.Utils
{
    public class GameplayUtils : UiTest
    {
        public static IEnumerator LoadWithoutAutoStart()
        {
            SceneManager.LoadScene("Splash");

            var timer = 0f;
            
            do
            {
                yield return null;

                timer += Time.deltaTime;
                if (timer > 30f)
                    throw new TimeoutException("Load scene timeout. Loading was more than 30s.");
                    
                var contexts = Contexts.SharedInstance;
                var game = contexts?.Game;
                var player = game?.PlayerEntity;
                
                if (player != null)
                    break;

            } while (true);
        }
        
        public static IEnumerator Load()
        {
            yield return LoadWithoutAutoStart();

            yield return PressStartAndWaitGameplay();
        }
        
        public static IEnumerator LoadWithDelay(float delay)
        {
            SceneManager.LoadScene("Splash");
            
            yield return PressStartAndWaitGameplay();

            yield return new WaitForSeconds(delay);
        }

        public static IEnumerator Swipe(Vector2 dir)
        {
            var movementInputView = Object.FindObjectOfType<PlayerMovementInputView>();

            yield return null;

            movementInputView.OnPointerDown(new(EventSystem.current)
            {
                position = GetScreenPositionFromNormalized(0.5f, 0.5f),
            });

            yield return null;

            movementInputView.OnPointerMove(new(EventSystem.current)
            {
                position = GetScreenPositionFromNormalized(dir.normalized),
            });

            yield return null;

            movementInputView.OnPointerUp(new(EventSystem.current)
            {
                position = GetScreenPositionFromNormalized(dir.normalized),
            });

            yield return null;
        }

        public static IEnumerator MakeSlideLeft()
        {
            yield return Swipe(new Vector2(-50f, 0));
        }
        
        public static IEnumerator MakeSlide()
        {
            yield return Swipe(new Vector2(0.5f, -1f));
        }

        public static IEnumerator MakeJump()
        {
            yield return Swipe(new Vector2(0.5f, 1f));
        }
        
        public static IEnumerator MakeTurnLeft()
        {
            yield return Swipe(new Vector2(-1f, 0.5f));
        }
        
        public static IEnumerator MakeTurnRight()
        {
            yield return Swipe(new Vector2(1f, 0.5f));
        }

        public static IEnumerator MakeDoubleJump(float intervalBeforeSecondJump = 0.35f)
        {
            yield return Swipe(Vector2.up * 80);
            
            yield return new WaitForSeconds(intervalBeforeSecondJump);
            
            yield return Swipe(Vector2.up * 80);
        }

        public static void SetPlayerPosition(Vector3 pos)
        {
            Contexts.SharedInstance.Game.PlayerEntity.ReplacePosition(pos);
            Contexts.SharedInstance.Game.PlayerEntity.CharacterController.Value.transform.position = pos;
        }
        
        public static IEnumerator AwaitFramesCount(Action update, float awaitFramesCount)
        {
            for (var i = 0; i < awaitFramesCount; i++)
            {
                yield return null;
                update?.Invoke();
            }
            
            yield return null;
        }

        public static IEnumerator SmoothSwipe(Vector2 direction, float speed = 1f, float distance = 0.7f, Vector2 endPosition = default)
        {
            yield return StartSmoothSwipe(direction, speed, distance);
            yield return StopSwipe(endPosition);
            yield return new WaitForSeconds(0.1f);
        }
        
        public static IEnumerator StartSmoothSwipe(Vector2 direction, float speed = 1f, float distance = 0.7f)
        {
            yield return AssertUiViewActive<PlayerMovementInputView>();
            var input = Object.FindObjectOfType<PlayerMovementInputView>();
            
            var currentPosition = Vector2.one * 0.5f;
            
            input.OnPointerDown(new(EventSystem.current)
            {
                position = GetScreenPositionFromNormalized(currentPosition),
            });
            
            var destinationPosition = currentPosition + direction.normalized * distance;
            
            const float threshold = 0.02f;
            while (Vector2.Distance(destinationPosition, currentPosition) >= threshold)
            {
                currentPosition = Vector2.MoveTowards(currentPosition, destinationPosition, Time.deltaTime * speed);
            
                input.OnPointerMove(new(EventSystem.current)
                {
                    position = GetScreenPositionFromNormalized(currentPosition)
                });
            
                yield return null;
            }
            
            yield return new WaitForSeconds(0.1f);
        }

        public static IEnumerator StopSwipe(Vector2 direction = default)
        {
            yield return AssertUiViewActive<PlayerMovementInputView>();
            
            var input = Object.FindObjectOfType<PlayerMovementInputView>();
            
            if(direction == default)
                direction = new(0.5f, 0.5f);
            
            input.OnPointerUp(new(EventSystem.current)
            {
                position = GetScreenPositionFromNormalized(direction)
            });
            yield return new WaitForSeconds(0.1f);
        }

        public static IEnumerator PressRestartAfterDeath()
        {
            yield return AssertUiViewActive<DeathView>();
            
            var deathView = Object.FindObjectOfType<DeathView>();
            
            yield return new WaitForSeconds(1f);
            
            deathView.RestartButton.onClick.Invoke();
            
            yield return new WaitForSeconds(0.7f);
        }

        public static IEnumerator PressCancelReviveAfterDeath()
        {
            yield return AssertUiViewActive<ReviveView>();
            
            var reviveView = Object.FindObjectOfType<ReviveView>();
            
            yield return new WaitForSeconds(1f);
            
            reviveView.CloseButton.onClick.Invoke();
            
            yield return new WaitForSeconds(0.7f);
        }
        
        public static void ShowAllLamps()
        {
            Object.FindObjectsOfType<LampShowerView>(true)
                  .ToList()
                  .ForEach(lamp => 
                       {
                           lamp.Show();
                           lamp.ShowAllLamps();
                       }
                   );
        }   
        
        public static void HideAllLamps()
        {
            Object.FindObjectsOfType<LampShowerView>(true)
                  .ToList()
                  .ForEach(lamp => lamp.Hide());
        }

        protected override void SubstituteResources() { }

        protected override void SubstituteCommonResources() { }

        private static Vector2 GetScreenPositionFromNormalized(Vector2 normalized)
        {
            return new(Screen.width * normalized.x, Screen.height * normalized.y);
        }
        
        private static Vector2 GetScreenPositionFromNormalized(float x, float y)
        {
            return new(Screen.width * x, Screen.height * y);
        }

        public static void StartTvLinesImmediately()
        {
            var tvService = DiUtils.Container.Resolve<ITvLinesService>();
            tvService.StartTvLinesImmediately(EPipelineAssetType.Default);
        }
        
        public static void StartTvLinesSmoothly(float smoothTime = 3f)
        {
            var tvService = DiUtils.Container.Resolve<ITvLinesService>();
            tvService.StartTvLinesSmoothly(EPipelineAssetType.Default, smoothTime);
        }
        
        public static void StopTvLinesImmediately()
        {
            var tvService = DiUtils.Container.Resolve<ITvLinesService>();
            tvService.StopTvLinesImmediately(EPipelineAssetType.Default);
        }
        
        public static void StopTvLinesSmoothly(float smoothTime = 3f)
        {
            var tvService = DiUtils.Container.Resolve<ITvLinesService>();
            tvService.StopTvLinesSmoothly(EPipelineAssetType.Default, smoothTime);
        }

        public static void StartGlitchesImmediately()
        {
            var glitchService = DiUtils.Container.Resolve<IGlitchService>();
            glitchService.StartGlitchesImmediately();
        }

        public static void StopGlitchesImmediately()
        {
            var glitchService = DiUtils.Container.Resolve<IGlitchService>();
            glitchService.StopGlitchesImmediately();
        }

        public static void ShowGlitches(float duration = 1.5f, float fadeTime = 0.3f)
        {
            var glitchService = DiUtils.Container.Resolve<IGlitchService>();
            glitchService.StartGlitches(duration, fadeTime);
        }

        public static TvLinesRendererFeature GetTvLines()
        {
            var urpSettings = DiUtils.Container.Resolve<IUrpSettingsBase>();
            var rendererData = urpSettings.DefaultRendererData;

            return rendererData.rendererFeatures.Find(feature => feature is TvLinesRendererFeature) as TvLinesRendererFeature;
        }
        
        public static IEnumerator PressStartAndWaitGameplay()
        {
            yield return AssertUiViewActive<MainMenuView>();

            var mainMenu = Object.FindObjectOfType<MainMenuView>();
            mainMenu.PlayButton.onClick.Invoke();

            yield return GameplayAssert.GameState(EGameState.Game);
        }

        public static void StopAllSounds()
        {
            var soundService = DiUtils.Container.Resolve<ISoundService>();
            soundService.StopAllSounds();
        }
        
        public static int GetBestDistance()
        {
            var scoreService = DiUtils.Container.Resolve<IScoreService>();
            
            return scoreService.BestPassedDistance;
        }
    }
}