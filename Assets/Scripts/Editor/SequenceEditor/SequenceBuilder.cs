using System.Collections.Generic;
using System.Linq;
using Db.GameLevelsSettings.Impls;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Sequence;
using Game.Utils.Chunks;
using Game.Utils.Sequence;
using UnityEditor;
using UnityEngine;

namespace Editor.SequenceEditor
{
    public static class SequenceBuilder
    {
        private const string WALLS = "WALLS";
        private const string OBSTACLES = "OBSTACLES";
        private const string UNIQUE = "UNIQUE";
        private const string UNIQUE_OBSTACLES = "UNIQUE_OBSTACLES";
        private const string COLLECTABLES = "COLLECTABLES";
        private const string ALMOND_WATER_PREFAB_PATH = "Assets/Prefabs/Collectables";

        public static void BuildSequence(
            SequenceParameters sequenceSettings,
            ChunkGenerationSettings chunkGenSettings,
            EChunkType chunkType,
            List<WallPairTypes> wallsSettings,
            List<SequenceObstacle> obstaclesSettings,
            bool shouldSpawnCollectables,
            List<Vector3> collectableOffsets
        )
        {
            if (!sequenceSettings)
                return;

            var rootName = sequenceSettings.name;
            var root = GameObject.Find(rootName) ?? new GameObject(rootName);

            if ((chunkType & EChunkType.Straight) != 0)
            {
                var w = GetOrCreateChild(root.transform, WALLS);
                UpdateWalls(w, wallsSettings);
                var o = GetOrCreateChild(root.transform, OBSTACLES);
                UpdateObstacles(o, obstaclesSettings);
            }
            else
            {
                var u = GetOrCreateChild(root.transform, UNIQUE);
                UpdateUniquePrefab(u, chunkGenSettings, chunkType);

                var uoList = sequenceSettings.UniquechunkObstacleSettings
                    .Select
                    (
                        uq => new SequenceObstacle
                        {
                            ObstaclePrefab = uq.ObstaclePrefab,
                            PointsSettings = new SequencePoint
                            {
                                Offset = uq.Offset,
                                Rotation = uq.Rotation
                            },
                            RespawnParameters = new CustomRespawnParameters()
                        }
                    )
                    .ToList();

                var parent = GetOrCreateChild(root.transform, UNIQUE_OBSTACLES);
                UpdateObstacles(parent, uoList);
            }

            var child = GetOrCreateChild(root.transform, COLLECTABLES);

            if (shouldSpawnCollectables)
                UpdateCollectables(child, collectableOffsets);
            else
                ClearChildren(child);

            Selection.activeGameObject = root;
        }

        private static void UpdateCollectables(Transform parent, List<Vector3> offsets)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                $"{ALMOND_WATER_PREFAB_PATH}/AlmondWater.prefab"
            );

            if (!prefab)
            {
                Debug.LogError($"AlmondWater.prefab not found at {ALMOND_WATER_PREFAB_PATH}");
                return;
            }

            ClearChildren(parent);

            for (var i = 0; i < offsets.Count; i++)
            {
                var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent.gameObject.scene);
                go.transform.SetParent(parent, false);
                go.transform.localPosition = offsets[i];
                go.transform.localRotation = Quaternion.identity;
                go.name = $"{prefab.name}_{i}";
            }
        }

        private static void UpdateWalls(Transform parent, List<WallPairTypes> wallsSettings)
        {
            var needed = wallsSettings.Sum(p => p.IsReplaceWallsWithObstacles ? 1 : 2);

            var children = Enumerable
                .Range(0, parent.childCount)
                .Select(parent.GetChild)
                .ToList();

            for (var i = children.Count - 1; i >= needed; i--)
                Object.DestroyImmediate(children[i].gameObject);

            var childIndex = 0;

            for (var pairIdx = 0; pairIdx < wallsSettings.Count; pairIdx++)
            {
                var pair = wallsSettings[pairIdx];
                var basePos = new Vector3(0, 0, pairIdx * 9f);

                if (pair.IsReplaceWallsWithObstacles && pair.ReplaceObstacle?.ObstaclePrefab != null)
                {
                    var prefab = pair.ReplaceObstacle.ObstaclePrefab;
                    GameObject go;

                    if (childIndex < parent.childCount && children[childIndex].name.StartsWith(prefab.name))
                    {
                        go = children[childIndex].gameObject;
                    }
                    else
                    {
                        if (childIndex < parent.childCount)
                            Object.DestroyImmediate(children[childIndex].gameObject);

                        go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent.gameObject.scene);
                        go.transform.SetParent(parent, false);
                    }

                    go.transform.localPosition = basePos;
                    go.transform.localRotation = Quaternion.identity;
                    go.transform.localScale = Vector3.one;
                    go.name = prefab.name;
                    go.transform.SetSiblingIndex(childIndex);
                    childIndex++;
                }
                else
                {
                    for (var side = 0; side < 2; side++)
                    {
                        var wallInfo = side == 0 ? pair.LeftWall : pair.RightWall;
                        var prefab = wallInfo?.WallPrefab;

                        if (!prefab)
                            continue;

                        GameObject go;

                        if (childIndex < parent.childCount && children[childIndex].name.StartsWith(prefab.name))
                        {
                            go = children[childIndex].gameObject;
                        }
                        else
                        {
                            if (childIndex < parent.childCount)
                                Object.DestroyImmediate(children[childIndex].gameObject);

                            go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent.gameObject.scene);
                            go.transform.SetParent(parent, false);
                        }

                        go.transform.localPosition = basePos;
                        go.transform.localRotation = Quaternion.identity;

                        go.transform.localScale = side == 0 
                            ? Vector3.one 
                            : new Vector3(-1, 1, 1);
                        
                        go.name = prefab.name;
                        go.transform.SetSiblingIndex(childIndex);
                        childIndex++;
                    }
                }
            }
        }

        private static void UpdateObstacles(Transform parent, List<SequenceObstacle> list)
        {
            var children = Enumerable
                .Range(0, parent.childCount)
                .Select(parent.GetChild)
                .ToList();

            var needed = list.Count;

            for (var i = children.Count - 1; i >= needed; i--)
                Object.DestroyImmediate(children[i].gameObject);

            for (var i = 0; i < needed; i++)
            {
                var s = list[i];

                if (!s.ObstaclePrefab)
                    continue;

                GameObject gameObject;

                if (i < parent.childCount && children[i].name.StartsWith(s.ObstaclePrefab.name))
                {
                    gameObject = children[i].gameObject;
                }
                else
                {
                    if (i < parent.childCount)
                        Object.DestroyImmediate(children[i].gameObject);

                    gameObject = (GameObject)PrefabUtility.InstantiatePrefab(s.ObstaclePrefab, parent.gameObject.scene);
                    gameObject.transform.SetParent(parent, false);
                }

                gameObject.transform.localPosition = s.PointsSettings.Offset;
                gameObject.transform.localRotation = Quaternion.Euler(s.PointsSettings.Rotation);
                gameObject.name = s.ObstaclePrefab.name;
                gameObject.transform.SetSiblingIndex(i);
            }
        }

        private static void UpdateUniquePrefab(
            Transform parent,
            ChunkGenerationSettings chunkGenSettings,
            EChunkType chunkType
        )
        {
            ClearChildren(parent);

            if (!chunkGenSettings || !chunkGenSettings.Chunks.TryGetValue(chunkType, out var views) || views.Count <= 0)
                return;

            var prefab = views[0].gameObject;

            var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent.gameObject.scene);
            go.transform.SetParent(parent, false);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            go.name = prefab.name;
        }

        private static void ClearChildren(Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
        }

        private static Transform GetOrCreateChild(Transform parent, string name)
        {
            var child = parent.Find(name);

            if (child)
                return child;

            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            return go.transform;
        }

        public static List<WallPairTypes> CloneWallPairs(List<WallPairTypes> src)
        {
            return src
                .Select(
                    p => new WallPairTypes
                    {
                        LeftWall = new SequenceWall
                        {
                            WallPrefab = p.LeftWall?.WallPrefab,
                            WallType = p.LeftWall?.WallType ?? EChunkWallType.None
                        },

                        RightWall = new SequenceWall
                        {
                            WallPrefab = p.RightWall?.WallPrefab,
                            WallType = p.RightWall?.WallType ?? EChunkWallType.None
                        },

                        IsReplaceWallsWithObstacles = p.IsReplaceWallsWithObstacles,

                        ReplaceObstacle = p.ReplaceObstacle == null
                            ? null
                            : new SequenceObstacleForWalls
                            {
                                ObstaclePrefab = p.ReplaceObstacle.ObstaclePrefab,
                                ObstacleType = p.ReplaceObstacle.ObstacleType
                            }
                    }
                ).ToList();
        }

        public static List<SequenceObstacle> CloneObstacles(List<SequenceObstacle> src)
        {
            return src
                .Select(
                    obstacle => new SequenceObstacle
                    {
                        ObstaclePrefab = obstacle.ObstaclePrefab,
                        ObstacleType = obstacle.ObstacleType,

                        PointsSettings = new SequencePoint
                        {
                            Offset = obstacle.PointsSettings.Offset,
                            Rotation = obstacle.PointsSettings.Rotation,
                            IsNeedOffset = obstacle.PointsSettings.IsNeedOffset
                        },

                        RespawnParameters = new CustomRespawnParameters
                        {
                            IsSetUpRespawn = obstacle.RespawnParameters.IsSetUpRespawn,
                            Position = obstacle.RespawnParameters.Position
                        }
                    }
                )
                .ToList();
        }
    }
}