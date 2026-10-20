using System.Collections.Generic;
using Db.HoleGenerator;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils;
using Game.Utils.HoleGenerator;
using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private const string WINDOW_TITLE = "Hole Generator";

        private float _depthHole;
        private float _widthHole;
        private float _lengthHole;
        private float _heightOfTriggers;
        private float _heightUpWall;
        private float _waterOffsetFromTop;
    
        private bool _isAttachedToRight;
        private bool _isAttachedToLeft;
        private bool _isHoleWithFloors = true;
        private bool _isHoleWithWater;
    
        private string _holeName = "NewPit";
        private string _defaultPathSave = "Assets/Prefabs/Obstacles/Holes";
        
        private EGameLevelType _levelType = EGameLevelType.Yellow;
        private EGeneratorHoleType _holeType = EGeneratorHoleType.Deafult;
        
        private LevelMaterialsParameters _yellowMaterialsParameters;
        private LevelMaterialsParameters _poolMaterialsParameters;
        private LevelMaterialsParameters _redMaterialsParameters;
    
        private readonly Dictionary<EHoleCorners, Vector3> _cornersForWalls = new();
        private WallCornerParameters _floorCorners;
    
        private IHoleConstructorParameters _parameters;
        private IHoleGeneratorPrefabsParameters _prefabsParameters;
    
        private GameObject _root;
        private GameObject _northWall;
        private GameObject _southWall;
        private GameObject _westWall;
        private GameObject _eastWall;
    
        private GameObject _northFloor;
        private GameObject _southFloor;
        private GameObject _westFloor;
        private GameObject _eastFloor;

        private GameObject _upperNorthWall;
        private GameObject _upperSouthWall;
    
        private GameObject _upperCeilingNorthWall;
        private GameObject _upperCeilingSouthWall;

        private GameObject _waterObject;
    
        private Transform _endPoint;
        private Transform _cameraTargetPoint;
        private Transform _centerBottomPoint;
        private ObstacleGroupView _rootGroupView;

        private Vector3 _offsetForPassObstacles;
        private Vector3 _eastWallPositionUp;
        private Vector3 _westWallPositionUp;

        private Vector3 _topMiddlePoint;
        private Vector3 _topHoleMiddlePoint;
        private Vector3 _bottomMiddlePoint;

        private const int WALL_PIVOT_OFFSET = 6;
    }
}