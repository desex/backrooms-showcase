using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.Lights.Actions;

namespace Game.Services.Lights.Impl.Commands
{
    public interface ILightEventAction
    {
        Type ActionDataType { get; }

        UniTask Execute(CancellationToken cancellationToken, ILightActionData actionData);
    }
}