using System.Collections.Generic;
using Db.SoundFxBase;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Render;
using Game.Services.Sound;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Low, 2000, nameof(EFeatures.Initialization))]
    public class StartGameSystem : ForEachCommandUpdateSystem<StartGameCommand>
    {
        private readonly ISoundService _soundService;
        private readonly ISpeedEffectService _speedEffectService;
        private readonly List<IUiInitializable> _uiInitializables;

        public StartGameSystem(
            ICommandBuffer commandBuffer,
            List<IUiInitializable> uiInitializables,
            ISoundService soundService,
            ISpeedEffectService speedEffectService
        ) : base(commandBuffer)
        {
            _uiInitializables = uiInitializables;
            _soundService = soundService;
            _speedEffectService = speedEffectService;
        }

        protected override void Execute(ref StartGameCommand command)
        {
            foreach (var uiInitializable in _uiInitializables)
                uiInitializable.Initialize();

            _speedEffectService.ChangeActiveFeature(false);
            _soundService.PlaySound(ESoundFxName.BackgroundAmbientMenu);
        }
    }
}