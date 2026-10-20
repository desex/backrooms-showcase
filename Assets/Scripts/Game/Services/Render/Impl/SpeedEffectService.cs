using Db.Effects;
using DG.Tweening;
using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using Game.Models.Camera;
using Game.Providers;
using Game.Utils.Movement;
using Project.Render.RendererFeatures.Impl;
using UnityEngine;

namespace Game.Services.Render.Impl
{
    public class SpeedEffectService : ARendererFeatureService<SpeedRendererFeature>, ISpeedEffectService
    {
        private readonly GameContext _game;
        private readonly IPlayerCameraHolder _playerCameraHolder;
        private readonly ISpeedEffectParameters _speedEffectParameters;

        private ESpeedEffectAxis _speedEffectAxis;
        private EPassObstacleType _passObstacleType;

        private bool _fovChanged;
        private Tween _tween;

        public SpeedEffectService(
            IRendererFeatureProvider renderFeatureProvider,
            IPlayerCameraHolder playerCameraHolder,
            ISpeedEffectParameters speedEffectParameters,
            GameContext game
        ) : base(renderFeatureProvider, true, false)
        {
            _game = game;
            _playerCameraHolder = playerCameraHolder;
            _speedEffectParameters = speedEffectParameters;
        }

        public void SetSpeedEffectAxis(EPassObstacleType passObstacleType) => _passObstacleType = passObstacleType;

        public void EnableSpeedEffect()
        {
            var tutorialEntity = _game.TutorialEntity;
            var isTutorialActive = tutorialEntity.IsInTutorial;

            if (isTutorialActive)
                return;

            _speedEffectAxis = ESpeedEffectAxis.All;
            var defaultColor = _speedEffectParameters.DefaultColor;
            ChangeSpeedEffectParameters(Color.white, defaultColor, true);

            if (_fovChanged)
                return;

            var targetFov = _speedEffectParameters.TargetFov;
            PlayFovEffect(targetFov);

            _fovChanged = true;
        }
        
        public void ChangeParametersInSpeedBoostFeature(
            float speed,
            float maskSize,
            float maskContrast,
            float smoothness,
            float power
        )
        {
            var rendererFeature = GetRendererFeature();
            
            rendererFeature.Settings.Speed = speed;
            rendererFeature.Settings.MaskSize = maskSize;
            rendererFeature.Settings.MaskContrast = maskContrast;
            rendererFeature.Settings.Smoothness = smoothness;
            rendererFeature.Settings.Power = power;
        }
        
        public ESpeedEffectAxis GetSpeedEffectAxis() => GetRendererFeature().Settings.EffectSide;
        
        public void EnableSpeedEffect(EObstacleTypeForAcceleration obstacleTypeForAcceleration)
        {
            ChangeObstacleTypeSpeedEffect(obstacleTypeForAcceleration);
            ChangeTypeSpeedEffect(true);

            if (_fovChanged)
                return;

            var targetFov = _speedEffectParameters.TargetFov;
            PlayFovEffect(targetFov);

            _fovChanged = true;
        }

        public void DisableSpeedEffect()
        {
            var player = _game.PlayerEntity;
            
            if (player.IsDash || player.IsBerserkMode)
                return;

            ChangeTypeSpeedEffect(false);

            if (!_fovChanged)
                return;

            var targetFov = _speedEffectParameters.DefaultFov;
            PlayFovEffect(targetFov);

            _fovChanged = false;
        }

        private void PlayFovEffect(float targetFov)
        {
            var currentFov = _playerCameraHolder.GetCamerasFov();
            var duration = _speedEffectParameters.Duration;

            _tween.Kill();
            _tween = DOTween.To(() => currentFov,
                                setter =>
                                {
                                    currentFov = setter;
                                    _playerCameraHolder.SetAllCamerasFov(currentFov);
                                },
                                targetFov,
                                duration
            );
        }

        private void ChangeObstacleTypeSpeedEffect(EObstacleTypeForAcceleration obstacleTypeForAcceleration)
        {
            _speedEffectAxis = obstacleTypeForAcceleration switch
            {
                EObstacleTypeForAcceleration.JumpAndSlide => _passObstacleType switch
                {
                    EPassObstacleType.Up => ESpeedEffectAxis.Up,
                    EPassObstacleType.Down => ESpeedEffectAxis.Down,
                    _ => _speedEffectAxis,
                },
                EObstacleTypeForAcceleration.Destructible => ESpeedEffectAxis.LeftAndRight,
                EObstacleTypeForAcceleration.Slide => ESpeedEffectAxis.Up,
                EObstacleTypeForAcceleration.BridgeChunk => ESpeedEffectAxis.Down,
                EObstacleTypeForAcceleration.MonkeyJump => ESpeedEffectAxis.Down,
                EObstacleTypeForAcceleration.ForwardJump => ESpeedEffectAxis.All,
                EObstacleTypeForAcceleration.SlideChunk => ESpeedEffectAxis.Up,
                EObstacleTypeForAcceleration.LeftSideColumn => ESpeedEffectAxis.Left,
                EObstacleTypeForAcceleration.RightSideColumn => ESpeedEffectAxis.Right,
                EObstacleTypeForAcceleration.TurnLeft => ESpeedEffectAxis.Left,
                EObstacleTypeForAcceleration.TurnRight => ESpeedEffectAxis.Right,
                EObstacleTypeForAcceleration.Travelator => ESpeedEffectAxis.All,
                _ => _speedEffectAxis,
            };
        }

        private void ChangeTypeSpeedEffect(bool isActive)
        {
            var targetColor = _speedEffectParameters.TargetColor;
            var defaultColor = _speedEffectParameters.DefaultColor;

            ChangeSpeedEffectParameters(targetColor, defaultColor, isActive);
        }

        private void ChangeSpeedEffectParameters(Color targetColor, Color defaultColor, bool isActive)
        {
            if (!_speedEffectParameters.SpeedShaderParameters.TryGetValue(_speedEffectAxis, out var parameters))
                return;

            var rendererFeature = GetRendererFeature();
            var featureSettings = rendererFeature.Settings;
            featureSettings.Color = isActive ? targetColor : defaultColor;
            featureSettings.EffectSide = _speedEffectAxis;
            
            ChangeActiveFeature(isActive);
        }

        public void ChangeActiveFeature(bool isActive)
        {
            var rendererFeature = GetRendererFeature();
            rendererFeature.SetActive(isActive);
        }
    }
}