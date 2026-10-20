using Game.Utils.HoleGenerator;
using UnityEditor;
using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor : EditorWindow
    {
        private void CreateHole()
        {
            CreateRootObject();
            CreateBottomPart();
            CreateWalls();
            CreateFloorsIfNeeded();
            CreateWaterIfNeed();
            CreateUpperParts();
            CreateAdditionalComponents();
            InitializeMaterials();
            ChangeMaterialForLevel();
            FinalizeInitialization();
            
            RecalculateWall(_eastWall);
            RecalculateWall(_westWall);
            RecalculateWall(_northWall);
            RecalculateWall(_southWall);
        }

        private void CreateRootObject()
        {
            _root = new GameObject(_holeName);
            SetTag(_root, "Hole");
        }

        private void CreateBottomPart()
        {
            var bottomPrefab = _parameters.HoleBottom;
            CreateBottom(bottomPrefab, _root.transform, "HoleFloor");
        }

        private void CreateWalls()
        {
            var holeWall = _parameters.HoleWall;

            _northWall = CreateWall(holeWall,
                                    _cornersForWalls[EHoleCorners.RightTop],
                                    _lengthHole,
                                    0,
                                    "HoleWallNorth"
            );
            _southWall = CreateWall(holeWall,
                                    _cornersForWalls[EHoleCorners.LowerMiddleLeft],
                                    _lengthHole,
                                    180,
                                    "HoleWallSouth"
            );

            _westWall = CreateWall(holeWall,
                                   _cornersForWalls[EHoleCorners.LeftTop],
                                   _widthHole,
                                   -90,
                                   "HoleWallWest"
            );
            _eastWall = CreateWall(holeWall,
                                   _cornersForWalls[EHoleCorners.LowerMiddleRight],
                                   _widthHole,
                                   -270,
                                   "HoleWallEast"
            );

            ApplyWallMaterials();
        }

        private void ApplyWallMaterials()
        {
            if (_isAttachedToRight)
                SubstitutionMaterialOnGameObject(_northWall, _parameters.UpHoleWallYellowMaterial);

            if (_isAttachedToLeft)
                SubstitutionMaterialOnGameObject(_southWall, _parameters.UpHoleWallYellowMaterial);
        }

        private void CreateFloorsIfNeeded()
        {
            if (!_isHoleWithFloors) return;

            var holeEdge = _parameters.HoleFloor;

            if (!_isAttachedToRight)
                _southFloor = CreateFloor(holeEdge, _root.transform, EWallSide.South);

            if (!_isAttachedToLeft)
                _northFloor = CreateFloor(holeEdge, _root.transform, EWallSide.North);

            _eastFloor = CreateFloor(holeEdge, _root.transform, EWallSide.East);
            _westFloor = CreateFloor(holeEdge, _root.transform, EWallSide.West);
        }

        private void CreateWaterIfNeed()
        {
            if (!_isHoleWithWater)
                return;
            
            CreateWaterObject();

            var waterObjectPositionY = _centerBottomPoint.position.y + _depthHole * 2 - _waterOffsetFromTop;
            var newPositionWithOffset = new Vector3(_centerBottomPoint.position.x, waterObjectPositionY, _centerBottomPoint.position.z);
            _waterObject.transform.position = newPositionWithOffset;

            //5 is specific value, need div on 10 cuz its minimal bounds in model but need half of it, +0.1f for not see edges
            var scaleForWater = new Vector3(_widthHole / 5 + 0.1f, 1, _lengthHole / 5 + 0.1f);
            _waterObject.transform.localScale = scaleForWater;
        }

        private void CreateUpperParts()
        {
            CreateUpperWall();
            CalculatePoints();
            CreateUpperCeiling();
        }

        private void CreateAdditionalComponents()
        {
            CreatePassObstacle(_root.transform, new Vector3(_widthHole, 0f, _lengthHole));
            CreateGameObjectPoints();
            CreateGhostTriggers(_eastWall);
            CreatePassObstacleEffectsStartAndStop();
        }

        private void FinalizeInitialization()
        {
            PreInitialize();
            PostInitialize();
            RecalculateRootPivot();
        }

        private void Clear()
        {
            var hole = GameObject.Find($"{_holeName}");
            DestroyImmediate(hole);
        }
        
        private void OnEnable()
        {
            FindHolesParameters();
            FindGeneratorPrefabsSo();
        }
    }
}
