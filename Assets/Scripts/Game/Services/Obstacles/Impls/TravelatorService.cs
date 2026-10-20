using System;
using Db.Obstacles;
using DG.Tweening;
using Ecs.Utils.Travelator;
using Game.Services.Player;
using Game.Services.Render;
using Game.Utils.Movement;

namespace Game.Services.Obstacles.Impls
{
    public class TravelatorService : ITravelatorService
    {
        private readonly ISpeedEffectService _speedEffectService;
        private readonly ITravelatorParameters _travelatorParameters;
        private readonly IPlayerSpeedService _playerSpeedService;
        private readonly GameContext _game;
        private Tween _tween;
        private Tween _chaserTween;
        
        public TravelatorService(
            ISpeedEffectService speedEffectService,
            GameContext game, 
            ITravelatorParameters travelatorParameters, 
            IPlayerSpeedService playerSpeedService
        )
        {
            _speedEffectService = speedEffectService;
            _game = game;
            _travelatorParameters = travelatorParameters;
            _playerSpeedService = playerSpeedService;
        }
        
        public void ActivateTravelator(ETravelatorDirection direction, ETravelatorSize size)
        {
            var chaserSpeedAdjustmentFactor = GetChaserSpeedAdjustmentFactor(direction, size);
            
            switch (direction)
            {
                case ETravelatorDirection.Backward:
                    ChaserSpeedAdjustment(1, chaserSpeedAdjustmentFactor);
                    ActivateDecelerationOnTravelator(_travelatorParameters.SpeedDecelerationFactorDuration);
                    break;
                
                case ETravelatorDirection.Forward:
                    ChaserSpeedAdjustment(-1, chaserSpeedAdjustmentFactor);
                    ActivateAccelerationOnTravelator(_travelatorParameters.SpeedAccelerationFactorDuration);
                    break;
            }
        }
        
        public void DeactivateTravelator(Action onTravelatorExit, ETravelatorDirection direction, ETravelatorSize size)
        {
            switch (direction)
            {
                case ETravelatorDirection.Backward:
                    ActivateAccelerationOnTravelator(_travelatorParameters.ExitTravelatorDuration, onTravelatorExit);
                    break;
                
                case ETravelatorDirection.Forward:
                    ActivateDecelerationOnTravelator(_travelatorParameters.ExitTravelatorDuration, onTravelatorExit);
                    break;
            }
        }
        
        public void KillTravelatorWork()
        {
            _tween?.Kill();
            _chaserTween?.Kill();
        }
        
        private void ActivateAccelerationOnTravelator(float tweenDuration, Action onComplete = null)
        {
            var player = _game.PlayerEntity;
            
            if (!player.HasForwardSpeed)
                return;
            
            _speedEffectService.EnableSpeedEffect(EObstacleTypeForAcceleration.Travelator);
            
            _tween?.Kill();
            
            var startForwardSpeed = _playerSpeedService.GetForwardSpeed(player);
            
            _tween = DOTween.To(() => 0f,
                                x =>
                                {
                                    if (player.HasReadyToObstaclePass || player.HasSlideChunk)
                                    {
                                        _chaserTween?.Kill();
                                        _tween.Kill();
                                        return;
                                    }
                                    
                                    var currentForwardSpeed = _playerSpeedService.GetForwardSpeed(player);
                                    player.ReplacePlayerOnTravelatorParameters(new TravelatorData(x, startForwardSpeed));
                                    player.ReplaceForwardSpeed(new ForwardSpeed(currentForwardSpeed, currentForwardSpeed));
                                },
                                _travelatorParameters.SpeedAccelerationFactor, tweenDuration)
                            .OnComplete(() => onComplete?.Invoke())
                            .OnKill(() => onComplete?.Invoke())
                            .SetEase(_travelatorParameters.EaseAcceleration);
        }
        
        private void ActivateDecelerationOnTravelator(float tweenDuration, Action onComplete = null)
        {
            var player = _game.PlayerEntity;
            
            if (!player.HasForwardSpeed)
                return;
            
            _speedEffectService.DisableSpeedEffect();
            
            _tween?.Kill();
            
            var startForwardSpeed = _playerSpeedService.GetForwardSpeed(player);
            
            _tween = DOTween.To(() => 0f,
                                x =>
                                {
                                    var currentForwardSpeed = _playerSpeedService.GetForwardSpeed(player);
                                    player.ReplacePlayerOnTravelatorParameters(new TravelatorData(-x, startForwardSpeed));
                                    player.ReplaceForwardSpeed(new ForwardSpeed(currentForwardSpeed, currentForwardSpeed));
                                },
                                _travelatorParameters.SpeedDecelerationFactor, tweenDuration)
                            .OnComplete(() => onComplete?.Invoke())
                            .OnKill(() => onComplete?.Invoke())
                            .SetEase(_travelatorParameters.EaseDeceleration);
        }
        
        private float GetChaserSpeedAdjustmentFactor(ETravelatorDirection direction, ETravelatorSize size)
        {
            var chaserSpeedAdjustmentFactor = 0f;
            
            switch (size)
            {
                case ETravelatorSize.Long:
                    chaserSpeedAdjustmentFactor = direction == ETravelatorDirection.Forward 
                        ? _travelatorParameters.ChaserDecelerationFactorOnLongTravelator 
                        : _travelatorParameters.ChaserAccelerationFactorOnLongTravelator;
                    break;
                
                case ETravelatorSize.Small:
                    chaserSpeedAdjustmentFactor = direction == ETravelatorDirection.Forward 
                        ? _travelatorParameters.ChaserDecelerationFactorOnShortTravelator 
                        : _travelatorParameters.ChaserAccelerationFactorOnShortTravelator;
                    break;
            }
            
            return chaserSpeedAdjustmentFactor;
        }
        
        private void ChaserSpeedAdjustment(int sign, float chaserSpeedAdjustmentFactor)
        {
            _chaserTween = DOTween.To(
                                       () => 0f,
                                       x =>
                                       {
                                           _game.ChaserEntity.ReplaceChaserSpeedAdjustmentFactor(x * sign);
                                       },
                                       chaserSpeedAdjustmentFactor,
                                       _travelatorParameters.SpeedAccelerationFactorDuration
                                   )
                                  .SetEase(_travelatorParameters.EaseAcceleration);
        }
    }
}