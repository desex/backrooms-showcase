using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.Lights.Actions;

namespace Game.Services.Lights.Impl.Commands.Impl
{
    public abstract class ALightEventAction<TActionData> : ILightEventAction where TActionData : ILightActionData
    {
        public Type ActionDataType => typeof(TActionData);

        public async UniTask Execute(CancellationToken cancellationToken, ILightActionData actionData)
        {
            // Более показать не могу
        }

        protected abstract UniTask Execute(CancellationToken cancellationToken, TActionData commandVo);
    }
}