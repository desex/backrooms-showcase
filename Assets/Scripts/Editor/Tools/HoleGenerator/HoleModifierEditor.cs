using System.Collections.Generic;
using Core.Utils;
using Db.HoleGenerator;
using Db.HoleGenerator.Impl;
using Game.Utils.HoleGenerator;
using UnityEditor;
using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public class HoleModifierEditor : EditorWindow
    {
        private const string WINDOW_TITLE = "Hole Modifier";
        private const string HOLE_PREFAB_PATH = "Assets/Prefabs/Obstacles/Holes";
    
        private float _depthHole;
        private float _widthHole;
        private float _lengthHole;
        private float _heightOfTriggers;
    
        private bool _hasLeftWall;
        private bool _hasRightWall;
        private bool _isHoleWithFloors = true;

        private GameObject _holePrefab;
    
        private GameObject _northWall;
        private GameObject _southWall;
        private GameObject _westWall;
        private GameObject _eastWall;
    
        private GameObject[] _walls;
    
        private GameObject _northFloor;
        private GameObject _southFloor;
        private GameObject _westFloor;
        private GameObject _eastFloor;
    
        private GameObject _bottom;
    
        private Vector3 _offsetForPassObstacles;
        private Vector3 _eastWallPositionUp;
        private Vector3 _westWallPositionUp;
    
        private readonly Dictionary<EHoleCorners, Vector3> _cornersForWalls = new();
        private HoleConstructorParameters _parameters;
        
        private EGeneratorHoleType _holeType = EGeneratorHoleType.Deafult;
        
        //TODO: commented not finished feature with hole modifier
        // [MenuItem("Tools/Holes/Hole Modifier")]
         public static void ShowWindow()
         {
             GetWindow<HoleModifierEditor>(WINDOW_TITLE);
         }

        private void OnGUI()
        {
            GUILayout.Label("Welcome to Hole Modifier!", EditorStyles.boldLabel);
        
            InitializeFields();

            EditorGUILayout.Space();
            if (GUILayout.Button("Select hole prefab", GUILayout.Height(30)))
            {
                SelectHolePrefab();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Change Depth", GUILayout.Height(30)))
            {
                ChangeDepth();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Change Width", GUILayout.Height(30)))
            {
                ChangeWidth();

                ChangeGameObjectPoints(_westWall, _westFloor);
            }
        
            EditorGUILayout.Space();
            if(GUILayout.Button("Change Length", GUILayout.Height(30)))
            {
                ChangeLength();

                ChangeGameObjectPoints(_westWall, _westFloor);
            }
        
            EditorGUILayout.Space();
            if(GUILayout.Button("Change Floor", GUILayout.Height(30)))
            {
                GetCornersFromBounds(_bottom.GetComponent<MeshCollider>());
            
                UpdateFloorsHoles();

                ChangeGameObjectPoints(_westWall, _westFloor);
            
                ShowPopupMessage("Префаб", "Успешно изменили полы вокруг ямы");
            }
        
            EditorGUILayout.Space();
            if (GUILayout.Button("Change Pass Obstacle", GUILayout.Height(30)))
            {
                CreatePassObstacle();

                ChangeGameObjectPoints(_westWall, _westFloor);
            
                ShowPopupMessage("Префаб", "Пасс обстакл подогнан под размеры ямы");
            }
        }
    
        private void UpdateFloorsHoles()
        {
            if (!_hasLeftWall)
            {
                ChangeEdge(_northFloor, EWallSide.North);
            }

            if (!_hasRightWall)
            {
                ChangeEdge(_southFloor, EWallSide.South);
            }
        
            ChangeEdge(_eastFloor, EWallSide.East); 
            ChangeEdge(_westFloor, EWallSide.West);
        }
    
        private void ChangeGameObjectPoints(GameObject wall, GameObject floor)
        {
            foreach (var transformPoint in _holePrefab.GetComponentsInChildren<Transform>())
            {
                if (transformPoint.CompareTag("HoleEndPoint"))
                {
                    var targetPosition = _isHoleWithFloors
                        ? CalculateFloorPosition(floor)
                        : CalculateWallPosition(wall);

                    transformPoint.transform.localPosition = targetPosition;
                }

                if (transformPoint.CompareTag("HoleCollectablesSpawnPoint"))
                {
                    var targetPosition = _isHoleWithFloors
                        ? CalculateFloorPosition(floor)
                        : CalculateWallPosition(wall);
                
                    transformPoint.transform.localPosition = targetPosition;
                }
            }
        }
    
        private Vector3 CalculateWallPosition(GameObject wall)
        {
            var renderer = wall.GetComponentInChildren<Renderer>();
            var centerPosition = renderer.bounds.center;
            centerPosition.y += wall.transform.localScale.y;
            return centerPosition;
        }

        private Vector3 CalculateFloorPosition(GameObject floor)
        {
            var renderer = floor.GetComponent<Renderer>();
            var centerPosition = renderer.bounds.center;
            centerPosition.z += floor.transform.localScale.z;
            return centerPosition;
        }
    
        private void CreatePassObstacle()
        {
            var allObjects = _holePrefab.GetComponentsInChildren<Transform>();

            GameObject obstacleContainer = null;
        
            foreach (var renderer in allObjects)
            {
                if (!renderer.CompareTag("RootHolePassObstacle"))
                    continue;
            
                obstacleContainer = renderer.gameObject;
                break;
            }

            if (obstacleContainer == null)
            {
                Debug.LogError("HolePassObstacle container not found!");
                return;
            }

            obstacleContainer.transform.localPosition = _offsetForPassObstacles + CalculateCenterHole();
        
            var firstObstacleConfig = _parameters.PassObstacleHole[_holeType][0];
            var previousCollider = GetOrAddBoxCollider(obstacleContainer.transform.GetChild(0).gameObject);

            SetupCollider(previousCollider, firstObstacleConfig.Depth);
        
            for (var i = 1; i < obstacleContainer.transform.childCount; i++)
            {
                var child = obstacleContainer.transform.GetChild(i);

                previousCollider = child.gameObject.CompareTag("HolePassObstacle")
                    ? ProcessHolePassObstacle(child.gameObject, previousCollider, _parameters.PassObstacleHole[_holeType][i])
                    : ProcessObjectsIncludedInHole(child.gameObject, previousCollider, _parameters.PassObstacleHole[_holeType][i]);
            }
        }
    
        private Vector3 CalculateCenterHole() 
        {
            var sum = Vector3.zero;
    
            foreach (GameObject wall in _walls) {
                sum += wall.transform.position;
            }
        
            sum.y = 0;
    
            return sum / _walls.Length;
        }
    
        private void SetupCollider(BoxCollider collider, float depth)
        {
            collider.size = new Vector3(_widthHole * 2, depth, _lengthHole * 2);
            collider.center = Vector3.zero;
        }
    
        private BoxCollider GetOrAddBoxCollider(GameObject gameObject)
        {
            var collider = gameObject.GetComponent<BoxCollider>();
            return collider != null ? collider : gameObject.AddComponent<BoxCollider>();
        }
    
        private float CalculateVerticalOffset(BoxCollider upperCollider, BoxCollider lowerCollider, float distanceBetween)
        {
            return upperCollider.gameObject.transform.position.y
                 - (upperCollider.size.y / 2 + lowerCollider.size.y / 2)
                 - distanceBetween
                 - upperCollider.size.y / 2;
        }

        private BoxCollider ProcessHolePassObstacle(GameObject obstacle, BoxCollider previousCollider, ObstaclePassSettingsParameters config)
        {
            var targetCollider = obstacle.GetComponentInChildren<BoxCollider>(includeInactive: true);
            if (targetCollider == null || !targetCollider.CompareTag("HolePassObstacleReadyTrigger"))
            {
                Debug.LogError("HolePassObstacleReadyTrigger not found in HolePassObstacle!");
                return previousCollider;
            }

            var originalColliderSize = targetCollider.size;
            SetupCollider(targetCollider, config.Depth);

            var verticalOffset = (previousCollider.size.y / 2 + targetCollider.size.y / 2) + config.DistanceBetweenObjects;
            obstacle.transform.localPosition = previousCollider.center + Vector3.down * verticalOffset;

            var sizeDifference = (targetCollider.size - originalColliderSize) / 2;
            foreach (Transform child in obstacle.transform)
            {
                if (child != targetCollider.transform)
                {
                    child.localPosition += sizeDifference;
                }
            }

            return targetCollider;
        }

        private BoxCollider ProcessObjectsIncludedInHole(
            GameObject obstacle,
            BoxCollider previousCollider,
            ObstaclePassSettingsParameters config
        )
        {
            var currentCollider = GetOrAddBoxCollider(obstacle);

            if (previousCollider.CompareTag("HolePassObstacleReadyTrigger"))
            {
                SetupCollider(currentCollider, config.Depth);

                var verticalOffset =
                    CalculateVerticalOffset(previousCollider, currentCollider, config.DistanceBetweenObjects);
                currentCollider.center = previousCollider.center + new Vector3(0f, verticalOffset, 0f);
            }
            else
            {
                SetupCollider(currentCollider, config.Depth);
                var verticalOffset = (previousCollider.size.y / 2 + currentCollider.size.y / 2)
                                   + config.DistanceBetweenObjects;
                currentCollider.center = previousCollider.center + Vector3.down * verticalOffset;
            }

            return currentCollider;
        }

        private void ChangeEdge(GameObject edge, EWallSide eWallSide)
        {
            var edgeSize = GetMeshSize(edge);

            var dictionary = _parameters.FloorsSettings;
        
            var scaledWidth = edgeSize.x * dictionary[eWallSide].Width;
            var scaledLength = edgeSize.z * dictionary[eWallSide].Length;
        
            var scaledWidth2 = edgeSize.x * dictionary[eWallSide].Length;
            var scaledLength2 = edgeSize.z * dictionary[eWallSide].Width;
        
            edge.transform.localScale = new Vector3(dictionary[eWallSide].Width, 0f, dictionary[eWallSide].Length);

            switch (eWallSide)
            {
                case EWallSide.North:
                    var northPos = _cornersForWalls[EHoleCorners.LowerMiddleLeft] + Vector3.up * _depthHole;
                    var endNorthPos = northPos + new Vector3(-scaledLength2 /2, 0, scaledWidth2 / 2);
                    edge.transform.localPosition = new Vector3(endNorthPos.x, 0f, endNorthPos.z);
                    break;
                case EWallSide.South:
                    var southPos = _cornersForWalls[EHoleCorners.RightTop] + Vector3.up * _depthHole;
                    var endSouthPos = southPos + new Vector3(scaledLength2/2, 0, -scaledWidth2 / 2);
                    edge.transform.localPosition = new Vector3(endSouthPos.x, 0f, endSouthPos.z);
                    break;
                case EWallSide.East:
                    var eastPos = _cornersForWalls[EHoleCorners.LowerMiddleRight] + Vector3.up * _depthHole;
                    var endEastPos = eastPos + new Vector3(-scaledWidth/2, 0, -scaledLength / 2);
                    edge.transform.localPosition = new Vector3(endEastPos.x, 0f, endEastPos.z);
                    break;
                case EWallSide.West:
                    var westPos = _cornersForWalls[EHoleCorners.LeftTop] + Vector3.up * _depthHole;
                    var endWestPos = westPos + new Vector3(scaledWidth/2, 0, scaledLength / 2);
                    edge.transform.localPosition = new Vector3(endWestPos.x, 0f, endWestPos.z);
                    break;
            }

            ModifyAndSaveMaterial(edge, new(scaledWidth, scaledLength));
        }

        private void ChangeLength()
        {
            var previousScale = _bottom.transform.localScale;
        
            var currentScale = new Vector3(previousScale.x, previousScale.y, _lengthHole);

            _bottom.transform.localScale = currentScale;
        
            GetCornersFromBounds(_bottom.GetComponent<MeshCollider>());
        
            var previousScaleNorthWall = _northWall.transform.localScale;
            var currentScaleNorthWall = new Vector3(previousScaleNorthWall.x, previousScaleNorthWall.y, _lengthHole);
            ChangeWall(_northWall, EHoleCorners.RightTop, currentScaleNorthWall);
        
            var previousScaleSouthWall = _southWall.transform.localScale;
            var currentScaleSouthWall = new Vector3(previousScaleSouthWall.x, previousScaleSouthWall.y, _lengthHole);
            ChangeWall(_southWall, EHoleCorners.LowerMiddleLeft, currentScaleSouthWall);
        
            var currentPositionEastWall = _cornersForWalls[EHoleCorners.LowerMiddleRight];
            _eastWall.transform.localPosition = currentPositionEastWall;

            var currentPositionWestWall = _cornersForWalls[EHoleCorners.LeftTop];
            _westWall.transform.position = currentPositionWestWall;
        
            UpdateFloorsHoles();
        
            ShowPopupMessage("Префаб", "Яма успешно изменена в длину");
        }

        private void ChangeWidth()
        {
            var previousScale = _bottom.transform.localScale;
        
            var currentScale = new Vector3(_widthHole, previousScale.y, previousScale.z);

            _bottom.transform.localScale = currentScale;
        
            GetCornersFromBounds(_bottom.GetComponent<MeshCollider>());

            var previouslyScaleEastWall = _eastWall.transform.localScale;
            var currentScaleEastWall = new Vector3(previouslyScaleEastWall.x, previouslyScaleEastWall.y, _widthHole);
            ChangeWall(_eastWall, EHoleCorners.LowerMiddleRight, currentScaleEastWall);
        
            var previouslyScaleWestWall = _westWall.transform.localScale;
            var currentScaleWestWall = new Vector3(previouslyScaleEastWall.x, previouslyScaleWestWall.y, _widthHole);
            ChangeWall(_westWall, EHoleCorners.LeftTop, currentScaleWestWall);
        
            var currentPositionEastWall = _cornersForWalls[EHoleCorners.RightTop];
            _northWall.transform.localPosition = currentPositionEastWall;

            var currentPositionWestWall = _cornersForWalls[EHoleCorners.LowerMiddleLeft];
            _southWall.transform.position = currentPositionWestWall;
        
            UpdateFloorsHoles();
        
            ShowPopupMessage("Префаб", "Яма успешно изменена в ширину");
        }

        private void ChangeDepth()
        {
            var previousPosition = _bottom.transform.localPosition;
            var currentPosition = new Vector3(previousPosition.x, -_depthHole * 2f, previousPosition.z);
            _bottom.transform.localPosition = currentPosition;
        
            GetCornersFromBounds(_bottom.GetComponent<MeshCollider>());
        
            var previousScaleEastWall = _eastWall.transform.localScale;
            var currentScaleEastWall = new Vector3(previousScaleEastWall.x, _depthHole, previousScaleEastWall.z);
            ChangeWall(_eastWall, EHoleCorners.LowerMiddleRight, currentScaleEastWall);
        
            var previousScaleWestWall = _westWall.transform.localScale;
            var currentScaleWestWall = new Vector3(previousScaleWestWall.x, _depthHole, previousScaleWestWall.z);
            ChangeWall(_westWall, EHoleCorners.LeftTop, currentScaleWestWall);
        
            var previousScaleNorthWall = _northWall.transform.localScale;
            var currentScaleNorthWall = new Vector3(previousScaleNorthWall.x, _depthHole, previousScaleNorthWall.z);
            ChangeWall(_northWall, EHoleCorners.RightTop, currentScaleNorthWall);
        
            var previousScaleSouthWall = _southWall.transform.localScale;
            var currentScaleSouthWall = new Vector3(previousScaleSouthWall.x, _depthHole, previousScaleSouthWall.z);
            ChangeWall(_southWall, EHoleCorners.LowerMiddleLeft, currentScaleSouthWall);
        
            ShowPopupMessage("Префаб", "Яма успешно изменена в глубину");
        }
    
        private Vector3 GetMeshSize(GameObject obj)
        {
            var meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                return meshFilter.sharedMesh.bounds.size;
            }
        
            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                return renderer.bounds.size;
            }
        
            Debug.LogError("No MeshFilter or Renderer found! Using default size (1,1,1)");
            return Vector3.one;
        }

        private void ChangeWall(GameObject wall, EHoleCorners eHoleCorner, Vector3 scale)
        {
            wall.transform.localScale = scale;
            wall.transform.localPosition = _cornersForWalls[eHoleCorner];
        
            ModifyAndSaveMaterial(wall, new Vector2(scale.z, scale.y));
        }

        private void GetHolesComponents()
        {
            var holesGameObject = _holePrefab.GetComponentsInChildren<MeshCollider>();

            foreach (var meshCollider in holesGameObject)
            {
                switch (meshCollider.gameObject.tag)
                {
                    case "HoleWallNorth":
                        _northWall = meshCollider.gameObject;
                        break;
                    case "HoleWallSouth":
                        _southWall = meshCollider.gameObject;
                        break;
                    case "HoleWallWest":
                        _westWall = meshCollider.gameObject;
                        break;
                    case "HoleWallEast":
                        _eastWall = meshCollider.gameObject;
                        break;
                    case "HoleFloor":
                        _bottom = meshCollider.gameObject;
                        break;
                    case "HoleFloorNorth":
                        _northFloor = meshCollider.gameObject;
                        break;
                    case "HoleFloorSouth":
                        _southFloor = meshCollider.gameObject;
                        break;
                    case "HoleFloorWest":
                        _westFloor = meshCollider.gameObject;
                        break;
                    case "HoleFloorEast":
                        _eastFloor = meshCollider.gameObject;
                        break;
                }
            }
        }
    
        private void InitializeFields()
        {
            EditorGUILayout.Space();
        
            _depthHole = EditorGUILayout.FloatField("DepthHole", _depthHole);
            _widthHole = EditorGUILayout.FloatField("WidthHole", _widthHole);
            _lengthHole = EditorGUILayout.FloatField("LengthHole", _lengthHole);
            _heightOfTriggers = EditorGUILayout.FloatField("HeigthTriggers", _heightOfTriggers);
        
            EditorGUILayout.Space();
            _offsetForPassObstacles = EditorGUILayout.Vector3Field("OffsetForPassObstacles", _offsetForPassObstacles);
        
            EditorGUILayout.Space();
            _hasLeftWall = EditorGUILayout.Toggle("HasLeftWall", _hasLeftWall);
            _hasRightWall = EditorGUILayout.Toggle("HasRightWall", _hasRightWall);
            _isHoleWithFloors = EditorGUILayout.Toggle("IsHoleWithFloors", _isHoleWithFloors);
        }

        private void SelectHolePrefab()
        {
            var prefabPath = EditorUtility.OpenFilePanel("Select Prefab", HOLE_PREFAB_PATH, "prefab");
        
            if(!string.IsNullOrEmpty(prefabPath))
            {
                var relativePath = "Assets" + prefabPath.Substring(Application.dataPath.Length);
                _holePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(relativePath);
            
                if(_holePrefab == null)
                    Debug.LogError($"Failed to load prefab at: {relativePath}");

                if (_holePrefab.CompareTag("Hole"))
                {
                    ShowPopupMessage("Префаб найден!", "Префаб загружен!");
                    GetHolesComponents();
                    _walls = new GameObject[] { _northWall, _southWall, _westWall, _eastWall };
                }
                else
                {
                    ShowPopupMessage("Префаб не соответствует требованиям!", "Выберите другой префаб");
                    _holePrefab = null;
                }
            }
            else
            {
                Debug.LogError("Failed to select prefab");
            }
        }

        private void ModifyAndSaveMaterial(GameObject gameObject, Vector2 tiling)
        {
            if (gameObject.TryGetComponent(out MaterialTilingApplier materialTilingApplier))
            {
                materialTilingApplier.SetTiling(tiling);
            }
            else
            {
                var materialTilingApplierForNew = gameObject.AddComponent<MaterialTilingApplier>();
                materialTilingApplierForNew.SetTiling(tiling);
            }
        }
    
        private void GetCornersFromBounds(MeshCollider meshCollider)
        {
            _cornersForWalls.Clear();
        
            if (meshCollider != null)
            {
                meshCollider.sharedMesh.RecalculateBounds();
            
                var vertices = meshCollider.sharedMesh.vertices;
                var manualBounds = new Bounds();

                foreach (var vertex in vertices)
                {
                    var worldVertex = meshCollider.transform.TransformPoint(vertex);
                    manualBounds.Encapsulate(worldVertex);
                }

                var center = manualBounds.center;
                var extents = manualBounds.extents;
            
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
        }

        private void ShowPopupMessage(string titleMessage, string message)
        {
            EditorUtility.DisplayDialog(
                titleMessage,                           
                message,
                "Ok"                                
            );
        }

        private void FindHolesParameters()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(HoleConstructorParameters)}");
        
            if (guids.Length == 0)
            {
                Debug.LogError($"Не найдено SO типа {nameof(HoleConstructorParameters)}!");
            }
        
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _parameters = AssetDatabase.LoadAssetAtPath<HoleConstructorParameters>(path);
        }
    }
}
