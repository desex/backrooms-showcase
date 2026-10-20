using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Lights.Actions.Impl
{
    [Serializable]
    public class ChangeColorLightActionData : ILightActionData
    {
          public bool IsChangeEnvironmentColor;
          [ShowIf(nameof(IsChangeEnvironmentColor))]
          public Color EnvironmentColor;
          
          public bool IsChangeLampsColor;
          [ShowIf(nameof(IsChangeLampsColor))]
          public Color LampsColor;
          
          public bool IsRandomDuration;
          [Range(0, 10)]
          [HideIf(nameof(IsRandomDuration), false)]
          public float Duration;
          
          [MinMaxSlider(0, 10)]
          [ShowIf(nameof(IsRandomDuration))]
          public Vector2 DurationRange;
          
          public Ease Ease;
    }
}