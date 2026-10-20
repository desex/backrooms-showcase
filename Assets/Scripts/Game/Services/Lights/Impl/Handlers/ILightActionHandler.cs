using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.Lights;
using Game.Utils.Lights.Actions;

namespace Game.Services.Lights.Impl.Handlers
{
    public interface ILightActionHandler
    {
        UniTask Handle(CancellationToken cancellationToken, ELightEvent lightEvent);
        UniTask Handle(CancellationToken cancellationToken, ILightActionData actionData);
    }
}