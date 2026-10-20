using System;
using Db.Effects;
using Game.UI.Effects;
using PdUtils.Interfaces;

namespace Game.Services.Effects.Impl
{
    public class UiSpeedService : IUiInitializable, IDisposable
    {
        private readonly GameContext _game;
        private readonly IUiEffectController _uiEffectsController;
        private IDisposable _disposable;

        public UiSpeedService(IUiEffectController uiEffectController, GameContext game)
        {
            _uiEffectsController = uiEffectController;
            _game = game;
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void StopSpeedEffect(GameEntity entity)
        {
            // Более показать не могу
        }
    }
}