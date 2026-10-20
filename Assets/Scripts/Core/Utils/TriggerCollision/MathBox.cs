using Unity.Mathematics;

namespace Core.Utils.TriggerCollision
{
    public struct MathBox
    {
        public float3 HalfExtent;
        private float3 _center;
        private float3 _rotation;

        public static implicit operator MathBox(in Box box)
        {
            var mathBox = new MathBox
            {
                _center = box.Center,
                HalfExtent = box.Size / 2,
                _rotation = box.Rotation,
            };

            return mathBox;
        }

        public static implicit operator Box(in MathBox mathBox)
        {
            var box = new Box
            {
                Center = mathBox._center,
                Size = mathBox.HalfExtent * 2,
                Rotation = mathBox._rotation,
            };

            return box;
        }
    }
}