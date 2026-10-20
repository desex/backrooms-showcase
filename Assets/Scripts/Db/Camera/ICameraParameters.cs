using DG.Tweening;
using UnityEngine;

namespace Db.Camera
{
    public interface ICameraParameters
    {
        float HorizontalTiltThreshold { get; }
        float HorizontalTiltDuration { get; }
        float UpSurfaceAngle { get; }
        float UpSurfaceRotationDuration { get; }
        Ease UpSurfaceRotationEase { get; }
        float SwayingSpeed { get; }
        Vector2 SwayingAmplitude { get; }
        Ease SwayingEaseType { get; }
    }
}