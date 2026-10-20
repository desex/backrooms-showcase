using System;
using Game.Utils.HoleGenerator;
using Game.Utils.MathfExtension;
using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private GameObject CreateFloor(GameObject edgePrefab, Transform parent, EWallSide wallSide)
        {
            var floor = Instantiate(edgePrefab, parent);
            var edgeSize = GetObjectSize(floor);
            var settings = _parameters.FloorsSettings[wallSide];
            
            var scaledWidth = edgeSize.x * settings.Width;
            var scaledLength = edgeSize.z * settings.Length;
            var scaledWidth2 = edgeSize.x * settings.Length;
            var scaledLength2 = edgeSize.z * settings.Width;
            
            floor.transform.localScale = new Vector3(settings.Width, 1f, settings.Length);
            
            var basePosition = GetBasePosition(wallSide) + Vector3.up * _depthHole;
            var offset = CalculatePositionOffset(wallSide, scaledWidth, scaledLength, scaledWidth2, scaledLength2);

            floor.transform.localPosition = MathfExtension.Round(new Vector3(
                (basePosition + offset).x,
                0f,
                (basePosition + offset).z
            ), 1);

            SetTag(floor, $"HoleFloor{wallSide}");
            floor.name = $"{wallSide}Floor";
            
            floor.AddComponent<MeshCollider>();

            return floor;
        }

        private Vector3 GetBasePosition(EWallSide wallSide)
        {
            return wallSide switch
            {
                EWallSide.North => _cornersForWalls[EHoleCorners.LowerMiddleLeft],
                EWallSide.South => _cornersForWalls[EHoleCorners.RightTop],
                EWallSide.East => _cornersForWalls[EHoleCorners.LowerMiddleRight],
                EWallSide.West => _cornersForWalls[EHoleCorners.LeftTop],
                _ => throw new ArgumentOutOfRangeException(nameof(wallSide), wallSide, null)
            };
        }

        private Vector3 CalculatePositionOffset(
            EWallSide wallSide,
            float scaledWidth,
            float scaledLength,
            float scaledWidth2,
            float scaledLength2
        )
        {
            return wallSide switch
            {
                EWallSide.North => new Vector3(-scaledLength2 / 2, 0, scaledWidth2 / 2),
                EWallSide.South => new Vector3(scaledLength2 / 2, 0, -scaledWidth2 / 2),
                EWallSide.East => new Vector3(-scaledWidth / 2, 0, -scaledLength / 2),
                EWallSide.West => new Vector3(scaledWidth / 2, 0, scaledLength / 2),
                _ => Vector3.zero,
            };
        }

        private void ModifyFloorScale(GameObject floor)
        {
            if (floor != null)
                ModifyAndSaveMaterial(floor, GetFloorScale(floor));
        }

        private Vector2 GetFloorScale(GameObject floor) =>
            floor != null ? new Vector2(floor.transform.localScale.x, floor.transform.localScale.z) : Vector2.zero;
    }
}