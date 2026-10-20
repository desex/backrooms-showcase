using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Camera.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CameraParameters), fileName = nameof(CameraParameters))]
    public class CameraParameters : ScriptableObject, ICameraParameters
    {
        [SerializeField] private float horizontalTiltThreshold;
        [SerializeField] private float horizontalTiltDuration;
        
        [field: Header("Up Surface Rotation Animation")]
        [field: SerializeField] public float UpSurfaceAngle { get; private set; }
        [field: SerializeField] public float UpSurfaceRotationDuration { get; private set; }
        [field: SerializeField] public Ease UpSurfaceRotationEase { get; private set; }
        
        [field: BoxGroup("Swaying parameters")]
        [field: SerializeField]
        public float SwayingSpeed { get; private set; }
        
        [field: BoxGroup("Swaying parameters")]
        [field: MinMaxSlider(-5f, 5f, true)]
        [field: SerializeField]
        public Vector2 SwayingAmplitude { get; private set; }

        [field: BoxGroup("Swaying parameters")]
        [field: SerializeField]
        public Ease SwayingEaseType { get; private set; }

        public float HorizontalTiltThreshold => horizontalTiltThreshold;
        public float HorizontalTiltDuration => horizontalTiltDuration;
    }
}