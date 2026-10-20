using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Services.Lights.Impl.Commands;
using Game.Services.Lights.Impl.Commands.Impl.Parts;
using Game.Services.Providers;
using Game.Utils.Lights;
using Game.Utils.Lights.Actions;

namespace Game.Services.Lights.Impl.Handlers.Impl
{
    public class LightActionHandler : ILightActionHandler
    {
        private readonly Dictionary<Type, ILightEventAction> _actions = new Dictionary<Type, ILightEventAction>();
        private readonly IGlobalIlluminationResourcesProvider _lightResourcesProvider;

        public LightActionHandler(
            IGlobalIlluminationResourcesProvider lightResourcesProvider,
            DelayLightEventAction delayLightEventAction,
            ChangeColorLightEventAction changeColorLightEventAction
        )
        {
            _lightResourcesProvider = lightResourcesProvider;

            Add(delayLightEventAction);
            Add(changeColorLightEventAction);
        }

        public async UniTask Handle(CancellationToken cancellationToken, ELightEvent lightEvent)
        {
            // Более показать не могу
        }

        public async UniTask Handle(CancellationToken cancellationToken, ILightActionData actionData)
        {
            // Более показать не могу
        }

        private void Add(ILightEventAction action)
        {
            // Более показать не могу
        }
    }
}