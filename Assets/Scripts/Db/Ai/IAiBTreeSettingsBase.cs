using System.Collections.Generic;
using BehaviorDesigner.Runtime;

namespace Db.Ai
{
    public interface IAiBTreeSettingsBase
    {
        IReadOnlyDictionary<EAiType, ExternalBehaviorTree> Trees { get; }
    }
}