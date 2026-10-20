using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Game.Utils.Chunks;
using Game.Utils.Sequence;
using UnityEditor;

namespace Editor.SequenceEditor
{
    public static class SequenceSettingsPersister
    {
        public static void Save(
            SequenceParameters sequenceSettings,
            EChunkType chunkType,
            bool shouldSpawnCollectables,
            List<WallPairTypes> wallsSettings,
            List<SequenceObstacle> obstaclesSettings,
            out List<WallPairTypes> newWallsSettings,
            out List<SequenceObstacle> newObstaclesSettings
        )
        {
            if (!sequenceSettings)
            {
                newWallsSettings = wallsSettings;
                newObstaclesSettings = obstaclesSettings;
                return;
            }

            Undo.RecordObject(sequenceSettings, "Save SequenceParameters");
            sequenceSettings.ChunkType = chunkType;

            var so = new SerializedObject(sequenceSettings);
            so.FindProperty("<ShouldSpawnCollectables>k__BackingField").boolValue = shouldSpawnCollectables;
            so.ApplyModifiedProperties();

            var bindingFlags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;

            typeof(SequenceParameters)
                .GetField("<WallsSettings>k__BackingField", bindingFlags)
                ?.SetValue(sequenceSettings, wallsSettings);

            typeof(SequenceParameters)
                .GetField("<ObstaclesSettings>k__BackingField", bindingFlags)
                ?.SetValue(sequenceSettings, obstaclesSettings);

            EditorUtility.SetDirty(sequenceSettings);
            AssetDatabase.SaveAssets();

            newWallsSettings = SequenceBuilder.CloneWallPairs(sequenceSettings.WallsSettings);
            newObstaclesSettings = SequenceBuilder.CloneObstacles(sequenceSettings.ObstaclesSettings);
        }
    }
}