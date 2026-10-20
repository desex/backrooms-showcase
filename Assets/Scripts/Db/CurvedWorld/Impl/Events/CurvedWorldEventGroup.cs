using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.CurvedWorld.Impl.Events
{
    [Serializable]
    public class CurvedWorldEventGroup
    {
        [field: OdinSerialize] private Dictionary<ACurvedWorldTypeParametersBase, float> _chanceTriggeringEvents;
        
        public IReadOnlyDictionary<ACurvedWorldTypeParametersBase, float> ChanceTriggeringEvents => _chanceTriggeringEvents;
    }
}