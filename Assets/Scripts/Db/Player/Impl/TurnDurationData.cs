using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Player.Impl
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class TurnDurationData
    {
        [field: MinMaxSlider(0, 1, true)]
        [field: SerializeField]
        public Vector2 Duration { get; private set; } = new(0.3f, 1f);
    }
}