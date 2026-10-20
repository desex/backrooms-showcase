using System;
using DG.Tweening;
using UnityEngine;

namespace Db.MainMenu
{
    [Serializable]
    public class CassetteAnimationParameters
    {
        [field: Header("General")]
        [field: Space]
        [field: SerializeField] public float DelayAfter { get; private set; }
        
        [field: Header("Insert Animation Parameters")]
        [field: SerializeField] public Vector3 InsertEndScale { get; private set; }
        [field: SerializeField] public Vector3 InsertEndPosition { get; private set; }
        [field: SerializeField] public float InsertDuration { get; private set; }
        [field: SerializeField] public float DelayBeforeMove { get; private set; }
        [field: SerializeField] public Ease InsertScaleEase { get; private set; }
        [field: SerializeField] public Ease InsertPositionEase { get; private set; }
        
        [field: Space]
        [field: Header("Insert Cassette Fade Parameters")]
        [field: SerializeField] public float FadeDelay { get; private set; }
        [field: SerializeField] public float FadeEndValue { get; private set; }
        [field: SerializeField] public float FadeDuration { get; private set; }
        [field: SerializeField] public Ease FadeEase { get; private set; }
        
        [field: Space]
        [field: Header("Curtain Animation Parameters")]
        [field: SerializeField] public float CurtainDelay { get; private set; } 
        [field: SerializeField] public Vector3 CurtainStartRotation { get; private set; }
        [field: SerializeField] public Vector3 CurtainEndRotation { get; private set; }
        [field: SerializeField] public float CurtainRotationDuration { get; private set; }
        [field: SerializeField] public Ease CurtainRotationEase { get; private set; }
    }
}