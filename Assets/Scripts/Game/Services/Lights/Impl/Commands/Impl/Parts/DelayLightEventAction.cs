using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.Lights.Actions.Impl;

namespace Game.Services.Lights.Impl.Commands.Impl.Parts
{
    public class DelayLightEventAction : ALightEventAction<DelayLightActionData>
    {
        protected override async UniTask Execute(CancellationToken cancellationToken, DelayLightActionData actionData)
        {
            // Более показать не могу
        }
    }
}