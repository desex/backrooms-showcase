using Db.HoleGenerator;
using Db.HoleGenerator.Impl;
using Game.Utils;
using UnityEditor;
using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        [MenuItem("Tools/Holes/Hole Generator")]
        public static void ShowWindow()
        {
            GetWindow<HoleGeneratorEditor>(WINDOW_TITLE);
        }
    
        private void OnGUI()
        {
            GUILayout.Label("Welcome to Hole Generator!", EditorStyles.boldLabel);
        
            InitializeFields();
        
            EditorGUILayout.HelpBox(
                $"Параметры для пола находятся в SO {nameof(HoleConstructorParameters)}",
                MessageType.Warning
            );
        
            EditorGUILayout.Space();
            if (GUILayout.Button("Create Hole", GUILayout.Height(30)))
            {
                CreateHole();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Save Hole", GUILayout.Height(30)))
            {
                SaveHolePrefab();
            }
        
            EditorGUILayout.Space();
            if (GUILayout.Button("Clear", GUILayout.Height(30)))
            {
                Clear();
            }
        }

        private void InitializeFields()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Naming", EditorStyles.boldLabel);
            EditorGUILayout.Space();
        
            _holeName = EditorGUILayout.TextField("Holename", _holeName);
            _defaultPathSave = EditorGUILayout.TextField("DefaultPathSaveHole", _defaultPathSave);
        
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hole Parameters", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            _depthHole = EditorGUILayout.Slider("DepthHole", _depthHole, 1f, 150f);
            _widthHole = EditorGUILayout.Slider("WidthHole", _widthHole, 1f, 60f);
            _lengthHole = EditorGUILayout.Slider("LengthHole", _lengthHole, 1f, 60f);
            _heightOfTriggers = EditorGUILayout.Slider("HeightTriggers", _heightOfTriggers, 1f, 60f);
            _levelType = (EGameLevelType)EditorGUILayout.EnumPopup(
                "Game Level Type", 
                _levelType
            );
        
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Up Hole Parameters", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            _heightUpWall = EditorGUILayout.Slider("HeightUpWall", _heightUpWall, 1f, 40f);
        
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hole Configuration", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            _isHoleWithFloors = EditorGUILayout.Toggle("IsHoleWithFloors", _isHoleWithFloors);
            
            //TODO: refactor this for appear checkboxes only when _isHoleWithFloors is true
            EditorGUILayout.HelpBox("To which wall hole is attached", MessageType.Warning);
            _isAttachedToRight = EditorGUILayout.Toggle("IsAttachedToRight", _isAttachedToRight);
            _isAttachedToLeft = EditorGUILayout.Toggle("IsAttachedToLeft", _isAttachedToLeft);
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Water Configuration", EditorStyles.boldLabel);
            _isHoleWithWater = EditorGUILayout.Toggle("IsHoleWithWater", _isHoleWithWater);

            if (_isHoleWithWater)
            {
                _waterOffsetFromTop = EditorGUILayout.Slider("WaterOffsetFromTop", _waterOffsetFromTop, 0f, 10f);
                _holeType = EGeneratorHoleType.WithWater;
            }
            else
            {
                _holeType = EGeneratorHoleType.Deafult;
            }
            
            EditorGUILayout.Space();
            _offsetForPassObstacles = EditorGUILayout.Vector3Field("OffsetForPassObstacles", _offsetForPassObstacles);
        }
    
        private void ShowPopupMessage(string titleMessage, string message)
        {
            EditorUtility.DisplayDialog(
                titleMessage,                           
                message,
                "Ok"                                
            );
        }
    }
}