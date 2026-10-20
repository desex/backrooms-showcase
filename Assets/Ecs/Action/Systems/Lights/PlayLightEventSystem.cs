using System;
using Db.SoundFxBase;
using Ecs.Action.Commands.Lights;
using Game.Services.Analytics;
using Game.Services.Lights;
using Game.Services.Providers;
using Game.Services.Sound;
using Game.Utils.Lights;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;

namespace Ecs.Action.Systems.Lights
{
    //TODO: Return this system to execution when light events will be back
    public class PlayLightEventSystem : ForEachCommandUpdateSystem<PlayLightEventCommand>
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly GameContext _game;
        private readonly IGlobalIlluminationResourcesProvider _lightResourcesProvider;
        private readonly ILightService _lightService;
        private readonly IRandomProvider _randomProvider;
        private readonly ISoundService _soundService;

        public PlayLightEventSystem(
            ISoundService soundService,
            IRandomProvider randomProvider,
            IGlobalIlluminationResourcesProvider lightResourcesProvider,
            ILightService lightService,
            ICommandBuffer commandBuffer,
            IAnalyticsService analyticsService,
            GameContext game
        ) : base(commandBuffer)
        {
            _soundService = soundService;
            _randomProvider = randomProvider;
            _lightResourcesProvider = lightResourcesProvider;
            _lightService = lightService;
            _analyticsService = analyticsService;
            _game = game;
        }

        protected override void Execute(ref PlayLightEventCommand command)
        {
            if (command.LightEvents.Count == 0)
                throw new InvalidOperationException("No light events");

            var chance = _randomProvider.Range(0f, 100);

            if (chance < 100 - _lightResourcesProvider.GlobalIlluminationSettings.PlayingRandomChance)
                return;

            var eventIndex = _randomProvider.Range(0, command.LightEvents.Count);
            var lightEvent = command.LightEvents[eventIndex];
            var soundFxName = GetSoundFxName(lightEvent);
            var lightEntity = _game.LightEntity;

            _lightService.Builder().SetOnStart(() =>
                {
                    _soundService.PlaySound(soundFxName);
                    _analyticsService.SetLightEvent(lightEvent);
                    lightEntity.ReplaceLightEventType(lightEvent);
                }
            ).SetEvent(lightEvent).SetOnEnd(() =>
                {
                    _analyticsService.SetLightEvent(ELightEvent.None);
                    lightEntity.ReplaceLightEventType(ELightEvent.None);
                }
            ).Build();
        }

        private static ESoundFxName GetSoundFxName(ELightEvent lightEvent)
        {
            // Более показать не могу
            return default;
        }
    }
}