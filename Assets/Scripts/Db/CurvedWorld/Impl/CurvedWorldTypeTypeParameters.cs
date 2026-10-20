using System.Collections.Generic;
using Db.CurvedWorld.Impl.Events;
using Db.GameLevelsSettings.Impls.Sequence;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.CurvedWorld.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CurvedWorldTypeTypeParameters), fileName = nameof(CurvedWorldTypeTypeParameters))]
    public class CurvedWorldTypeTypeParameters : SerializedScriptableObject, ICurvedWorldTypeParameters
    {
        [field: SerializeField]
        [field: Range(1, 100)]
        public float CountCurvedWorldEventInLevel { get; private set; }
        
        [field: SerializeField] 
        private Dictionary<SequenceParametersData, CurvedWorldEventGroup> _curvedWorldTypeParameters;
        
        public IReadOnlyDictionary<SequenceParametersData, CurvedWorldEventGroup> Parameters => _curvedWorldTypeParameters;
    }
}