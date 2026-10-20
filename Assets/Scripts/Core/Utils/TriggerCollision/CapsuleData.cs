using Unity.Mathematics;

namespace Core.Utils.TriggerCollision
{
    public struct CapsuleData
    {
        public float Radius;
        public float3 Point1;
        public float3 Point2;
        
        private float _height;
        private float3 _center;
        private float3 _upDirection;
        private float _halfHeight;

        public CapsuleData(in Capsule capsule)
        {
            Radius = capsule.Radius;
            _height = capsule.Height;
            _center = capsule.Center;
            _upDirection = math.mul(capsule.Rotation, math.up());
            _halfHeight = _height * 0.5f - Radius;
            Point1 = _center + _upDirection * _halfHeight;
            Point2 = _center - _upDirection * _halfHeight;
        }

        public static CapsuleData InterpolateCapsulesData(
            in CapsuleData from, 
            in CapsuleData to, 
            float tParameter
        )
        {
            var capsuleData = new CapsuleData
            {
                Radius = from.Radius,
                _height = from._height,
                _center = math.lerp(from._center, to._center, tParameter),
                _upDirection = math.normalize(math.lerp(from._upDirection, to._upDirection, tParameter)),
                _halfHeight = from._halfHeight,
            };

            capsuleData.Point1 = capsuleData._center + capsuleData._upDirection * capsuleData._halfHeight;
            capsuleData.Point2 = capsuleData._center - capsuleData._upDirection * capsuleData._halfHeight;

            return capsuleData;
        }
    }
}