using System;
using DG.Tweening;
using UnityEngine;

namespace Db.MainMenu
{
    [Serializable]
    public class MonitorAnimationParameters
    {
        [field: Header("Control Panel Animation Parameters")]
        [field: SerializeField] public Vector3 EndScale { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float SkipDuration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}