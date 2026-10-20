using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Obstacles;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.GenerationSettings;
using Db.Lights;
using Db.Lights.Impl;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Core
{
    [CreateAssetMenu(menuName = "Settings/GameLevelsSettings/" + nameof(GameLevelResources), fileName = nameof(GameLevelResources))]
    public class GameLevelResources : ScriptableObject, IGameLevelResources
    {
        [SerializeField] private ChunkGenerationSettings _chunkGenerationSettings;
        [SerializeField] private ObstaclesBase _obstaclesBase;
        [SerializeField] private LightSettings _lightParameters;
        [SerializeField] private SequenceGenerationSettings _sequenceGenerationSettings;
        
        public IChunkGenerationSettings ChunkGenerationSettings => _chunkGenerationSettings;
        public ISequenceGenerationSettings SequenceGenerationSettings => _sequenceGenerationSettings;
        public IObstaclesBase ObstaclesBase => _obstaclesBase;
        public ILightSettings GlobalIlluminationSettings => _lightParameters;
    }
}