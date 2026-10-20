using System;
using Game.Models.Camera;
using UnityEngine;

namespace Game.Utils.Camera
{
    [Serializable]
    public struct CameraAnchor
    {
        public ECameraType[] cameraTypes;
        public Transform follow;
        public Transform lookAt;
    }
}