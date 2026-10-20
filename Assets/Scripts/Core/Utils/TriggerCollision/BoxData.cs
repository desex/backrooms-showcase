using Unity.Mathematics;

namespace Core.Utils.TriggerCollision
{
    public struct BoxData
    {
        public MathBox Box;
        public float3 Center;
        public quaternion Rotation;

        public static implicit operator BoxData(in BoxActionData boxActionData)
        {
            var boxData = new BoxData
            {
                Box = boxActionData.Box,
                Center = boxActionData.GlobalPosition,
                Rotation = boxActionData.GlobalRotation,
            };

            return boxData;
        }
    }
}