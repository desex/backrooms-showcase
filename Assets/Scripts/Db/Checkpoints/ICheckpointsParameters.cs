using System.Collections.Generic;
using Game.Utils.Checkpoint;
using Game.Utils.Chunks;
using UnityEngine;

namespace Db.Checkpoints
{
    public interface ICheckpointsParameters
    {
        int RadiusObstacleSpawnCheckInDefault{ get; }
        int RadiusObstacleSpawnCheckInSequential { get; }
        int MeterToKilometerThreshold { get; }
        float ForwardAngleThreshold { get; }
        int FrequencyCheckpointsInMeters { get; }
        List<EChunkType> SpawnChunks { get; }
        int ParentDelayAfterCreation { get; }
        Texture2D BlackScreenTexture { get; }
        Vector2Int BlackScreenTextureSize { get; }  
        CheckpointTexturesData NoiseTexturesData { get; }
        Vector2Int AppearNoiseTextureSize { get; }        
        Vector2Int LoopNoiseTextureSize { get; }
        Vector2Int AppearDefaultTextureSize { get; }        
        Vector2Int LoopDefaultTextureSize { get; } 
        Texture2D NewRecordTexture { get; }
        Vector2Int AppearNewRecordTextureSize { get; }
        Vector2Int LoopNewRecordTextureSize { get; }
        CheckPointStateDistanceData[] StateDistances { get; }
        IReadOnlyDictionary<int, CheckpointTexturesData> TexturesData { get; }
    }
}