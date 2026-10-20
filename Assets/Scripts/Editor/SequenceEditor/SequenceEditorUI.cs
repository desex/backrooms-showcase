using System.Collections.Generic;
using Db.GameLevelsSettings.Impls;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Sequence;
using Game.Utils.Chunks;
using Game.Utils.Obstacles;
using Game.Utils.Sequence;
using UnityEditor;
using UnityEngine;

namespace Editor.SequenceEditor
{
    public static class SequenceEditorUI
    {
        private static bool _wallSettingsFoldout = true;
        private static bool _obstacleSettingsFoldout = true;
        
        private static readonly Color _backgroundColor = new Color32(0x34, 0x67, 0x5C, 0xFF);
        private static readonly Color _color = new Color32(0xA3, 0xCC, 0xAB, 0xFF);

        public static void DrawWallsSection(
            List<WallPairTypes> wallsSettings,
            List<bool> wallFoldouts
        )
        {
            var prev = GUI.backgroundColor;
            GUI.backgroundColor = _backgroundColor;

            if (GUILayout.Button("WALL SETTINGS", GUILayout.Height(28)))
                ToggleFoldout(ref wallFoldouts, ref _wallSettingsFoldout);

            GUI.backgroundColor = prev;

            if (!_wallSettingsFoldout) 
                return;

            wallsSettings ??= new List<WallPairTypes>();
            var toRemove = -1;

            for (var i = 0; i < wallsSettings.Count; i++)
            {
                wallFoldouts[i] = EditorGUILayout.Foldout(wallFoldouts[i], $"Wall Pair {i}", true);
                if (!wallFoldouts[i]) 
                    continue;

                var s = wallsSettings[i];
                EditorGUILayout.BeginVertical("box");
                s.LeftWall ??= new SequenceWall();
                s.RightWall ??= new SequenceWall();

                if (!s.IsReplaceWallsWithObstacles)
                {
                    s.LeftWall.WallPrefab =
                        DrawPrefabPreviewField("Left Wall", s.LeftWall.WallPrefab);

                    s.LeftWall.WallType =
                        (EChunkWallType)EditorGUILayout.EnumPopup("Left Wall Type", s.LeftWall.WallType);

                    s.RightWall.WallPrefab =
                        DrawPrefabPreviewField("Right Wall", s.RightWall.WallPrefab);

                    s.RightWall.WallType =
                        (EChunkWallType)EditorGUILayout.EnumPopup("Right Wall Type", s.RightWall.WallType);
                }

                s.IsReplaceWallsWithObstacles =
                    EditorGUILayout.Toggle("Replace With Obstacle", s.IsReplaceWallsWithObstacles);

                if (s.IsReplaceWallsWithObstacles)
                {
                    s.ReplaceObstacle ??= new SequenceObstacleForWalls();

                    s.ReplaceObstacle.ObstaclePrefab =
                        DrawPrefabPreviewField("Replace Obstacle", s.ReplaceObstacle.ObstaclePrefab);

                    s.ReplaceObstacle.ObstacleType =
                        (EObstacleType)EditorGUILayout.EnumPopup("Obstacle Type", s.ReplaceObstacle.ObstacleType);
                }

                if (GUILayout.Button("Remove")) 
                    toRemove = i;
                EditorGUILayout.EndVertical();
                wallsSettings[i] = s;
            }

            if (toRemove >= 0)
            {
                wallsSettings.RemoveAt(toRemove);
                wallFoldouts.RemoveAt(toRemove);
            }

            if (GUILayout.Button("Add Wall Pair"))
            {
                wallsSettings.Add(new WallPairTypes());
                wallFoldouts.Add(true);
            }
        }

        public static void DrawObstaclesSection(
            List<SequenceObstacle> obstaclesSettings,
            List<bool> obstacleFoldouts
        )
        {
            var backgroundSwap = GUI.backgroundColor;
            GUI.backgroundColor = _color;

            if (GUILayout.Button("OBSTACLES SETTINGS", GUILayout.Height(28)))
                ToggleFoldout(ref obstacleFoldouts, ref _obstacleSettingsFoldout);

            GUI.backgroundColor = backgroundSwap;

            if (!_obstacleSettingsFoldout) 
                return;

            obstaclesSettings ??= new List<SequenceObstacle>();
            var toRemove = -1;

            for (var i = 0; i < obstaclesSettings.Count; i++)
            {
                obstacleFoldouts[i] = EditorGUILayout.Foldout(obstacleFoldouts[i], $"Obstacle {i}", true);
                if (!obstacleFoldouts[i]) 
                    continue;

                var obstacleSettings = obstaclesSettings[i];
                EditorGUILayout.BeginVertical("box");

                obstacleSettings.ObstaclePrefab =
                    DrawPrefabPreviewField("Obstacle Prefab", obstacleSettings.ObstaclePrefab);

                obstacleSettings.ObstacleType =
                    (EObstacleType)EditorGUILayout.EnumPopup("Type", obstacleSettings.ObstacleType);

                var pointsSettings = obstacleSettings.PointsSettings;
                pointsSettings.IsNeedOffset = EditorGUILayout.Toggle("Is Need Offset", pointsSettings.IsNeedOffset);

                if (pointsSettings.IsNeedOffset)
                {
                    pointsSettings.Offset = EditorGUILayout.Vector3Field("Offset", pointsSettings.Offset);
                    pointsSettings.Rotation = EditorGUILayout.Vector3Field("Rotation", pointsSettings.Rotation);
                }

                obstacleSettings.PointsSettings = pointsSettings;

                var respawnParams = obstacleSettings.RespawnParameters;
                respawnParams.IsSetUpRespawn = EditorGUILayout.Toggle("Custom Respawn", respawnParams.IsSetUpRespawn);

                if (respawnParams.IsSetUpRespawn)
                    respawnParams.Position = EditorGUILayout.Vector3Field("Respawn Offset", respawnParams.Position);

                obstacleSettings.RespawnParameters = respawnParams;

                if (GUILayout.Button("Remove")) 
                    toRemove = i;
                
                EditorGUILayout.EndVertical();
                obstaclesSettings[i] = obstacleSettings;
            }

            if (toRemove >= 0)
            {
                obstaclesSettings.RemoveAt(toRemove);
                obstacleFoldouts.RemoveAt(toRemove);
            }

            if (GUILayout.Button("Add Obstacle"))
            {
                obstaclesSettings.Add(new SequenceObstacle());
                obstacleFoldouts.Add(true);
            }
        }

        public static void DrawUniqueChunkSection(
            SequenceParameters sequenceParams,
            ChunkGenerationSettings config,
            EChunkType chunkType
        )
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unique Chunk Prefab", EditorStyles.boldLabel);

            if (config && config.Chunks.TryGetValue(chunkType, out var views) && views.Count > 0)
                EditorGUILayout.ObjectField("Prefab", views[0].gameObject, typeof(GameObject), false);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unique Chunk Obstacles", EditorStyles.boldLabel);

            foreach (var uc in sequenceParams.UniquechunkObstacleSettings)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.ObjectField("Obstacle Prefab", uc.ObstaclePrefab, typeof(GameObject), false);
                EditorGUILayout.Vector3Field("Offset", uc.Offset);
                EditorGUILayout.Vector3Field("Rotation", uc.Rotation);
                EditorGUILayout.EndVertical();
            }
        }

        private static GameObject DrawPrefabPreviewField(string label, GameObject prefab)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(100));
            var prev = AssetPreview.GetAssetPreview(prefab) ?? AssetPreview.GetMiniThumbnail(prefab);
            GUILayout.Label(prev, GUILayout.Width(90), GUILayout.Height(90));
            prefab = (GameObject)EditorGUILayout.ObjectField(prefab, typeof(GameObject), false, GUILayout.Height(60));
            EditorGUILayout.EndHorizontal();
            return prefab;
        }

        private static void ToggleFoldout(ref List<bool> list, ref bool state)
        {
            state = !state;

            for (var i = 0; i < list.Count; i++)
                list[i] = state;
        }
    }
}