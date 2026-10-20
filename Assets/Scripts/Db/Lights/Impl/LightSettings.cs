using System;
using System.Collections.Generic;
using System.Linq;
using Game.Utils.Lights;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Lights.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(LightSettings), fileName = nameof(LightSettings))]
    public class LightSettings : SerializedScriptableObject, ILightSettings
    {
        [PropertySpace(10)]
        [Header("Events")]
        [OdinSerialize]
        private Dictionary<ELightEvent, LightActionsContainer> _lightEventActions;

        [field: PropertySpace(10)]
        [field: Range(0, 100)]
        [field: SerializeField] public int PlayingRandomChance { get; private set; } = 15;

        [field: PropertySpace(10)]
        [field: Header("Lamps")]
        [field: SerializeField] public Material LampMaterial { get; private set; }
        
        public LightActionsContainer GetLightEvents(ELightEvent lightEvent)
        {
            // Более показать не могу
            return default;
        }
    }
}