using Game.Utils;
using Game.Utils.ColorsUtils;
using Game.Utils.HoleGenerator;
using Game.Utils.MathfExtension;
using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private GameObject CreateWall(
            GameObject wallPrefab, 
            Vector3 wallPosition,
            float wallLength,
            float rotationY,
            string tag
        )
        {
            var wall = InstantiateAndInitializeWall(
                wallPrefab, 
                wallPosition, 
                Quaternion.Euler(0, rotationY, 0),
                new Vector3(1f, _depthHole, wallLength)
            );
        
            SetTag(wall, tag);
        
            AddMeshColliders(wall);

            return wall;
        }

        private void RecalculateWall(GameObject wall)
        {
            var boundsUpperWall = GetObjectBounds(_upperNorthWall);

            var bottomRightCorner = new Vector3(
                boundsUpperWall.max.x,
                boundsUpperWall.min.y,
                boundsUpperWall.center.z
            );

            var wallBounds = GetObjectBounds(wall);

            var topRightCorner = new Vector3(
                wallBounds.max.x,
                wallBounds.max.y,
                wallBounds.max.z
            );
            
            var offset = bottomRightCorner.y - topRightCorner.y;
            
            wall.transform.position += new Vector3(0f, offset, 0f);
        }
    
        private void CreateUpperWall()
        {
            if (_isHoleWithFloors)
            {
                var (leftWallPoint, rightWallPoint) = CalculateWallSpawnPoint();
                
                DebugUtility.Log($"Left wall spawn point: {leftWallPoint} \n Right wall spawn point: {rightWallPoint}", UtilsColors.NotificationColor);
        
                _upperNorthWall = InstantiateAndInitializeWall(
                    _parameters.UpHoleWall,
                    leftWallPoint,
                    Quaternion.identity,
                    Vector3.one
                );

                _upperSouthWall = InstantiateAndInitializeWall(
                    _parameters.UpHoleWall,
                    rightWallPoint,
                    Quaternion.identity,
                    new(-1f, 1f, 1f)
                );
                
                AddMeshColliders(_upperNorthWall);
                AddMeshColliders(_upperSouthWall);
            }
            else
            {
                DebugUtility.Log($"Corners: LowerFarthestRight {_cornersForWalls[EHoleCorners.RightTop]}\n LowerFarthestLeft {_cornersForWalls[EHoleCorners.LeftTop]}", UtilsColors.NotificationColor);
                
                _upperNorthWall = InstantiateAndInitializeWall(
                    _parameters.UpHoleWall,
                    _topMiddlePoint,
                    Quaternion.identity,
                    Vector3.one
                );


                
                _upperSouthWall = InstantiateAndInitializeWall(
                    _parameters.UpHoleWall,
                    _topMiddlePoint,
                    Quaternion.identity,
                    new(-1f, 1f, 1f)
                );
            
                AddMeshColliders(_upperNorthWall);
                AddMeshColliders(_upperSouthWall);
            }
        }
        
        private (Vector3 , Vector3) CalculateWallSpawnPoint()
        {
            var eastMeshFilter = _eastFloor.GetComponentInChildren<MeshFilter>();
            
            var eastBounds = eastMeshFilter.sharedMesh.bounds;
            
            _floorCorners = CalculateBaseCorners(eastBounds);
            
            if (!_isAttachedToRight && !_isAttachedToLeft)
            {
                var calculatedPoint = CalculatePoint();
                return (calculatedPoint, calculatedPoint);
            }
        
            if (_isAttachedToRight && _isAttachedToLeft)
            {
                return (_topMiddlePoint, _topMiddlePoint);
            }
            
            if (_isAttachedToRight)
            {
                var rightWallSpawnPoint = MathfExtension.Round(new Vector3(_floorCorners.TopRight.x - WALL_PIVOT_OFFSET, _floorCorners.TopRight.y, _floorCorners.TopRight.z), 1);
                var leftWallSpawnPoint = MathfExtension.Round(new Vector3(_floorCorners.TopLeft.x + WALL_PIVOT_OFFSET, _floorCorners.TopLeft.y, _floorCorners.TopLeft.z), 1);

                return (leftWallSpawnPoint, rightWallSpawnPoint);
            }
    
            if (_isAttachedToLeft)
            {
                var rightWallSpawnPoint = MathfExtension.Round(new Vector3(_floorCorners.TopRight.x - WALL_PIVOT_OFFSET, _floorCorners.TopRight.y, _floorCorners.TopRight.z), 1);
                var leftWallSpawnPoint = MathfExtension.Round(new Vector3(_floorCorners.TopLeft.x + WALL_PIVOT_OFFSET, _floorCorners.TopLeft.y, _floorCorners.TopLeft.z), 1);

                return (leftWallSpawnPoint, rightWallSpawnPoint);
            }

            return default;

            Vector3 CalculatePoint()
            {
                var wallSpawnPoint = new Vector3((_floorCorners.TopLeft.x + _floorCorners.TopRight.x) / 2f, 0f, _floorCorners.TopRight.z);
                return MathfExtension.Round(wallSpawnPoint, 1);;
            }
        }
        
        private Vector3 GetTransformedPoint(GameObject floor, float x, float y, float z)
        {
            return MathfExtension.Round(floor.transform.TransformPoint(new Vector3(x, y, z)), 1);
        }
        
        private WallCornerParameters CalculateBaseCorners(Bounds eastBounds)
        {
            var topRight = GetTransformedPoint(_westFloor, eastBounds.max.x, eastBounds.min.y, eastBounds.max.z);
            var topLeft = new Vector3(_eastFloor.transform.TransformPoint(new(eastBounds.min.x, 0, 0)).x,
                                      topRight.y,
                                      topRight.z
            );
            var bottomRight = new Vector3(topRight.x,
                                          topRight.y,
                                          _eastFloor.transform.TransformPoint(new(0, 0, eastBounds.min.z)).z
            );
            var bottomLeft = new Vector3(topLeft.x, topLeft.y, bottomRight.z);
            return new WallCornerParameters
            {
                TopRight = topRight,
                TopLeft = topLeft,
                DownRight = bottomRight,
                DownLeft = bottomLeft
            };
        }
    
        private GameObject InstantiateAndInitializeWall(GameObject prefab, Vector3 wallPosition, Quaternion rotation, Vector3 scale)
        {
            var wall = Instantiate(prefab, _root.transform);
            wall.transform.localPosition = wallPosition;
            wall.transform.rotation = rotation;
            wall.transform.localScale = scale;
            return wall;
        }
    
        private Vector3 CalculateWallPosition(GameObject wall, int sign)
        {
            var renderer = wall.GetComponentInChildren<Renderer>();
            var boundsCenter = renderer.bounds.center;
            boundsCenter.y += wall.transform.localScale.y * sign;
            return boundsCenter;
        }
    }
}