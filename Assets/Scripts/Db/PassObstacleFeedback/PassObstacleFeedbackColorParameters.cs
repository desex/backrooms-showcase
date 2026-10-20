using System.Collections.Generic;
using Game.Utils.Lights;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.PassObstacleFeedback
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PassObstacleFeedbackColorParameters), fileName = nameof(PassObstacleFeedbackColorParameters))]
    public class PassObstacleFeedbackColorParameters : SerializedScriptableObject, IPassObstacleFeedbackParameters
    {
        [OdinSerialize] public ColorParameters DefaultColorParameters { get; set; }

        [field: PropertySpace(10)]
        [OdinSerialize] 
        private Dictionary<ELightEvent, ColorParameters> _colorParameters;

        public IReadOnlyDictionary<ELightEvent, ColorParameters> ColorParameters => _colorParameters;
    }
}