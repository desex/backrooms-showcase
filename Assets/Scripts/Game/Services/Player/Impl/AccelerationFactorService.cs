using Db.Player;
using DG.Tweening;
using PdUtils.Interfaces;

namespace Game.Services.Player.Impl
{
    public class AccelerationFactorService : IAccelerationFactorService, IUiInitializable
    {
        private readonly GameContext _game;
        private readonly IPlayerAccelerationParametersBase _playerAccelerationParametersBase;

        private float _accelerationFactor;

        private Tween _accelerationFactorTween;

        public AccelerationFactorService(
            IPlayerAccelerationParametersBase playerAccelerationParametersBase,
            GameContext game
        )
        {
            _playerAccelerationParametersBase = playerAccelerationParametersBase;
            _game = game;
        }

        public void SetAccelerationFactor(float factor)
        {
            _accelerationFactor = factor;
        }

        public float GetAccelerationFactor()
        {
            return _accelerationFactor;
        }

        public void InterpolateAccelerationFactor()
        {
            var character = _game.PlayerEntity;

            var animancer = character.Animancer.Value;
            var curve = _playerAccelerationParametersBase.AccelerationCurve;
            var maxDuration = _playerAccelerationParametersBase.AccelerationDuration;

            _accelerationFactorTween?.Kill();

            _accelerationFactorTween = DOTween.To(
                    () => 0f,
                    _ =>
                    {
                        var speed = animancer.States.Current.Speed;
                        var currentTime = (maxDuration - character.SpeedAcceleration.Duration) * speed;
                        var factor = curve.Evaluate(currentTime);

                        _accelerationFactor = factor;
                    },
                    maxDuration,
                    maxDuration
                )
                .SetEase(Ease.Linear);
        }

        public void Initialize()
        {
            _accelerationFactor = _playerAccelerationParametersBase.AccelerationFactor;
        }
    }
}