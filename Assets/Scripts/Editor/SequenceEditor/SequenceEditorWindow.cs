using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Utils.ItemCollectables;
using Game.Utils.Chunks;
using Game.Utils.Sequence;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace Editor.SequenceEditor
{
    public class SequenceEditorWindow : EditorWindow
    {
        private class CollectableGroup
        {
            public ECollectableType Type;
            public List<Vector3> Offsets;
            public List<bool> OffsetFoldouts;
            public bool Foldout;

            public CollectableGroup(ECollectableType type, List<Vector3> offsets)
            {
                Type = type;
                Offsets = offsets;
                OffsetFoldouts = Enumerable.Repeat(true, offsets.Count).ToList();
                Foldout = true;
            }
        }

        private const double BUILD_TIME_STEP = 0.1;

        private static readonly Color _windowBackgroundColor = new Color32(0x14, 0x14, 0x0F, 0xFF);
        private static readonly Color _textColor = new Color32(0xF2, 0x68, 0x00, 0xFF);

        private Vector2 _scrollPos;
        private List<bool> _wallFoldouts = new();
        private List<bool> _obstacleFoldouts = new();
        private List<CollectableGroup> _collectableGroups = new();
        private bool _collectablesFoldout = true;

        private SequenceParameters _sequenceSettings;
        private ChunkGenerationSettings _chunkGenSettings;
        private EChunkType _chunkType;
        private bool _shouldSpawnCollectables;
        private List<WallPairTypes> _wallsSettings;
        private List<SequenceObstacle> _obstaclesSettings;

        private bool _autoBuildEnabled;
        private double _lastBuildTime;
        private readonly Color _backgroundColor = new Color32(0xF2, 0x68, 0x00, 0xFF);

        [MenuItem("Tools/Sequence Editor")]
        public static void OpenWindow()
        {
            var window = GetWindow<SequenceEditorWindow>();
            window.titleContent = new GUIContent("Sequence Editor");
            window.Show();
        }

        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;
            _lastBuildTime = EditorApplication.timeSinceStartup;

            if (_chunkGenSettings == null)
            {
                var res = Resources.Load<ChunkGenerationSettings>("GenerationSettings_Pool_Default");
                
                if (res != null) 
                    _chunkGenSettings = res;
            }
        }

        private void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (_sequenceSettings == null || !_autoBuildEnabled) 
                return;

            var now = EditorApplication.timeSinceStartup;
            
            if (now - _lastBuildTime < BUILD_TIME_STEP)
                return;

            _lastBuildTime = now;

            var flatOffsets = _collectableGroups
                .SelectMany(group => group.Offsets)
                .ToList();

            SequenceBuilder.BuildSequence(
                _sequenceSettings,
                _chunkGenSettings,
                _chunkType,
                _wallsSettings,
                _obstaclesSettings,
                _shouldSpawnCollectables,
                flatOffsets
            );
        }

        private void OnGUI()
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, position.height), _windowBackgroundColor);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));

            EditorGUILayout.Space();

            var titleStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = _textColor,
                },
            };

            EditorGUILayout.LabelField("SEQUENCE EDITOR TOOL", titleStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();

            _chunkGenSettings = (ChunkGenerationSettings)EditorGUILayout.ObjectField(
                "ChunkGenerationSettings",
                _chunkGenSettings,
                typeof(ChunkGenerationSettings),
                false
            );

            EditorGUILayout.Space();

            var newSeq = (SequenceParameters)EditorGUILayout.ObjectField(
                "SequenceParameters",
                _sequenceSettings,
                typeof(SequenceParameters),
                false
            );

            if (newSeq != _sequenceSettings)
            {
                _sequenceSettings = newSeq;
               
                if (_sequenceSettings) 
                    InitializeFromSettings();
            }

            if (_sequenceSettings)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(
                    "SAVE",
                    GUILayout.Height(EditorGUIUtility.singleLineHeight * 2),
                    GUILayout.Width(100)
                ))
                    BuildSave();

                var backgroundSwap = GUI.backgroundColor;
               
                GUI.backgroundColor = _autoBuildEnabled 
                    ? Color.green 
                    : Color.gray;

                _autoBuildEnabled = GUILayout.Toggle(
                    _autoBuildEnabled,
                    "AUTO-BUILD",
                    "Button",
                    GUILayout.Height(EditorGUIUtility.singleLineHeight * 2),
                    GUILayout.Width(100)
                );

                EditorGUILayout.EndHorizontal();

                GUI.backgroundColor = backgroundSwap;

                EditorGUILayout.Space();
                _chunkType = (EChunkType)EditorGUILayout.EnumFlagsField("Chunk Type", _chunkType);

                _shouldSpawnCollectables = EditorGUILayout.Toggle(
                    "Should Spawn Collectables",
                    _shouldSpawnCollectables
                );

                if ((_chunkType & EChunkType.Straight) != 0)
                {
                    SequenceEditorUI.DrawWallsSection(_wallsSettings, _wallFoldouts);
                    SequenceEditorUI.DrawObstaclesSection(_obstaclesSettings, _obstacleFoldouts);

                    if (_shouldSpawnCollectables)
                    {
                        backgroundSwap = GUI.backgroundColor;
                        GUI.backgroundColor = _backgroundColor;

                        if (GUILayout.Button("COLLECTABLES SETTINGS", GUILayout.Height(28)))
                            _collectablesFoldout = !_collectablesFoldout;

                        GUI.backgroundColor = backgroundSwap;

                        if (_collectablesFoldout) DrawCollectableGroups();
                    }
                }
                else
                {
                    SequenceEditorUI.DrawUniqueChunkSection(_sequenceSettings, _chunkGenSettings, _chunkType);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void InitializeFromSettings()
        {
            _chunkType = _sequenceSettings.ChunkType;
            _shouldSpawnCollectables = _sequenceSettings.ShouldSpawnCollectables;
            _wallsSettings = SequenceBuilder.CloneWallPairs(_sequenceSettings.WallsSettings);
            _obstaclesSettings = SequenceBuilder.CloneObstacles(_sequenceSettings.ObstaclesSettings);

            _collectableGroups = _sequenceSettings.CollectablesSpawnInfo
                .Select(kv => new CollectableGroup(kv.Key, new List<Vector3>(kv.Value)))
                .ToList();

            _wallFoldouts = Enumerable.Repeat(true, _wallsSettings.Count).ToList();
            _obstacleFoldouts = Enumerable.Repeat(true, _obstaclesSettings.Count).ToList();

            var flat = _collectableGroups
                .SelectMany(group => group.Offsets)
                .ToList();

            SequenceBuilder.BuildSequence(
                _sequenceSettings,
                _chunkGenSettings,
                _chunkType,
                _wallsSettings,
                _obstaclesSettings,
                _shouldSpawnCollectables,
                flat
            );
        }

        private void DrawCollectableGroups()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Collectables Groups", EditorStyles.boldLabel);

            for (var i = 0; i < _collectableGroups.Count; i++)
            {
                var grp = _collectableGroups[i];

                grp.Foldout = EditorGUILayout.Foldout(
                    grp.Foldout,
                    $"Group {i}: {grp.Type} ({grp.Offsets.Count})",
                    true
                );

                if (!grp.Foldout) 
                    continue;

                EditorGUI.indentLevel++;
                grp.Type = (ECollectableType)EditorGUILayout.EnumFlagsField("Collectable Type", grp.Type);

                for (var j = 0; j < grp.Offsets.Count; j++)
                {
                    grp.OffsetFoldouts[j] = EditorGUILayout.Foldout(grp.OffsetFoldouts[j], $"Offset {j}", true);
                    if (!grp.OffsetFoldouts[j]) 
                        continue;

                    EditorGUILayout.BeginHorizontal();
                    grp.Offsets[j] = EditorGUILayout.Vector3Field("", grp.Offsets[j]);

                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        grp.Offsets.RemoveAt(j);
                        grp.OffsetFoldouts.RemoveAt(j);
                        j--;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Collectable"))
                {
                    grp.Offsets.Add(Vector3.zero);
                    grp.OffsetFoldouts.Add(true);
                }

                EditorGUI.indentLevel--;

                if (GUILayout.Button("Remove Group"))
                {
                    _collectableGroups.RemoveAt(i);
                    i--;
                }

                EditorGUILayout.Space();
            }

            if (GUILayout.Button("Add Collectable Group"))
                _collectableGroups.Add(new CollectableGroup(ECollectableType.None, new List<Vector3>()));
        }

        private void BuildSave()
        {
            SequenceSettingsPersister.Save(
                _sequenceSettings,
                _chunkType,
                _shouldSpawnCollectables,
                _wallsSettings,
                _obstaclesSettings,
                out _wallsSettings,
                out _obstaclesSettings
            );

            using var _ = DictionaryPool<ECollectableType, List<Vector3>>.Get(out var dict);

            foreach (var grp in _collectableGroups)
                if (!dict.ContainsKey(grp.Type))
                    dict[grp.Type] = new List<Vector3>(grp.Offsets);

            var fieldInfo = typeof(SequenceParameters)
                .GetField("_collectablesSpawnInfo", BindingFlags.NonPublic | BindingFlags.Instance);

            fieldInfo?.SetValue(_sequenceSettings, dict);

            EditorUtility.SetDirty(_sequenceSettings);
            AssetDatabase.SaveAssets();

            _wallFoldouts = Enumerable.Repeat(true, _wallsSettings.Count).ToList();
            _obstacleFoldouts = Enumerable.Repeat(true, _obstaclesSettings.Count).ToList();
            _collectablesFoldout = true;

            foreach (var group in _collectableGroups)
                group.OffsetFoldouts = Enumerable.Repeat(true, group.Offsets.Count).ToList();
        }
    }
}