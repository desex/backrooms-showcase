using Game.Utils.HoleGenerator;
using Game.Utils.MathfExtension;
using PdUtils;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private void SetTag(GameObject gameObject, string tag)
        {
            if (InternalEditorUtility.tags.Contains(tag))
            {
                gameObject.tag = tag;
            }
            else
            {
                Debug.Assert(!InternalEditorUtility.tags.Contains(tag), $"Тег '{tag}' не найден! Добавь его в Tags and Layers.");
            }
        }
    
        private void AddMeshColliders(GameObject wall)
        {
            foreach (var child in wall.GetComponentsInChildren<Transform>())
            {
                child.gameObject.AddComponent<MeshCollider>();
            }
        }

        private Vector3 GetObjectSize(GameObject obj)
        {
            var dimensions = GetObjectDimensionsInternal(obj);
            return dimensions.size;
        }

        private Bounds GetObjectBounds(GameObject obj)
        {
            return GetObjectDimensionsInternal(obj);
        }

        private Bounds GetObjectDimensionsInternal(GameObject obj)
        {
            var meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
                return meshFilter.sharedMesh.bounds;

            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
                return renderer.bounds;

            var rendererParent = obj.GetComponentInChildren<Renderer>();
            if (rendererParent != null)
                return rendererParent.bounds;

            var collider = obj.GetComponent<Collider>();
            
            return collider != null
                ? collider.bounds
                : new Bounds(obj.transform.position, Vector3.one);
        }
    
        private void GetCornersFromBounds(MeshCollider meshCollider)
        {
            _cornersForWalls.Clear();

            if (meshCollider == null)
                return;
            
            meshCollider.sharedMesh.RecalculateBounds();
            
            var vertices = meshCollider.sharedMesh.vertices;
            var manualBounds = new Bounds();

            foreach (var vertex in vertices)
            {
                var worldVertex = meshCollider.transform.TransformPoint(vertex);
                manualBounds.Encapsulate(worldVertex);
            }

            var centerUnrounded = manualBounds.center;
            var center = MathfExtension.Round(centerUnrounded, 1);
            var extentsUnrounded = manualBounds.extents;
            var extents = MathfExtension.Round(extentsUnrounded, 1);
            
            var corners = new Vector3[4];
            corners[0] = new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z);
            corners[1] = new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z);
            corners[2] = new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z);
            corners[3] = new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z);
            
            _cornersForWalls.Add(EHoleCorners.LowerMiddleLeft, corners[0]);
            _cornersForWalls.Add(EHoleCorners.LeftTop, corners[1]);
            _cornersForWalls.Add(EHoleCorners.RightTop, corners[2]);
            _cornersForWalls.Add(EHoleCorners.LowerMiddleRight, corners[3]);
        }
    
        private void RecalculateRootPivot()
        {
            var rootTransform = _root.transform;
            using var _ = ListPool<Transform>.Get(out var allChildren);

            for (var i = 0; i < rootTransform.childCount; i++)
            {
                allChildren.Add(rootTransform.GetChild(i));
            }

            var newPivot = _bottomMiddlePoint;
        
            var tempParent = new GameObject("HoleTempParent")
            {
                transform =
                {
                    position = newPivot,
                },
            };

            foreach (var child in allChildren)
            {
                child.transform.parent = tempParent.transform;
            }

            _root.transform.position = tempParent.transform.position;

            foreach (var child in allChildren)
            {
                child.transform.parent = rootTransform;
            }
        
            DestroyImmediate(tempParent);

            _root.transform.position = Vector3.zero;
        }

        private void CalculatePoints()
        {
            _topHoleMiddlePoint = MathfExtension.Round(new Vector3((_cornersForWalls[EHoleCorners.RightTop] - _cornersForWalls[EHoleCorners.LeftTop]).x / 2f, 0f, _cornersForWalls[EHoleCorners.LeftTop].z), 1);
            if (!_isAttachedToLeft && !_isAttachedToRight)
            {
                _topMiddlePoint = _topHoleMiddlePoint;
                _bottomMiddlePoint = new(_topMiddlePoint.x, 0f, 0f);
                return;
            }

            var middlePointWithAttachedToWallsX = MathfExtension.Round((_floorCorners.TopLeft + _floorCorners.TopRight).x / 2f, 1);
            var middleBottomPointWithAttachedToWallsZ = MathfExtension.Round(_floorCorners.DownLeft.z , 1);
            var middleTopPointWithAttachedToWallsZ = MathfExtension.Round(_floorCorners.TopRight.z , 1);

            _topMiddlePoint = new (middlePointWithAttachedToWallsX, 0f, middleTopPointWithAttachedToWallsZ);
            _bottomMiddlePoint = new (middlePointWithAttachedToWallsX, 0f, middleBottomPointWithAttachedToWallsZ);
        }
    }
}