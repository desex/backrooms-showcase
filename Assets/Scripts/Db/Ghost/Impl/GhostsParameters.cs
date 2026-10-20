using Game.Utils.Chunks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Ghost.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(GhostsParameters), fileName = nameof(GhostsParameters))]
    public partial class GhostsParameters : ScriptableObject, IGhostsParameters
    {
        [field: SerializeField] public int GhostCount { get; private set; } = 3;
        [field: SerializeField] public float KillGhostDistance { get; private set; } = 125f;
        [field: SerializeField] public float FailChancesDistance  { get; private set; } = 50f;
        

        [field: FoldoutGroup("Ghost Metrics")]
        [field: SerializeField] 
        public float GhostWidth { get; private set; } = 0.6f;
        
        [field: FoldoutGroup("Ghost Metrics")]
        [field: SerializeField] 
        public float GhostHeight { get; private set; } = 2f;

        [field: FoldoutGroup("Spawn")]
        [field: SerializeField] 
        public float StartForwardDeltaFromPlayer { get; private set; } = 4;

        [field: FoldoutGroup("Revive")]
        [field: SerializeField]
        public float TurnChunksForwardReviveOffset { get; private set; } = 2;

        [field: FoldoutGroup("Revive")]
        [field: MinMaxSlider(0f, 12f, true)]
        [field: SerializeField]
        public Vector2 SidesReviveDeltaRange { get; private set; } = new(0f, 4f);
        
        [field: FoldoutGroup("Revive")]
        [field: MinMaxSlider(0f, 60f, true)]
        [field: SerializeField]
        public Vector2 ForwardReviveDeltaRange { get; private set; } = new(5f, 15f);

        [field: FoldoutGroup("Revive")]
        [field: SerializeField] 
        public float RespawnDelay { get; private set; } = 1.5f;

        [field: FoldoutGroup("Revive")]
        [field: SerializeField]
        public float CooldownAfterFailRespawn { get; private set; } = 2f;

        [field: FoldoutGroup("Movement")]
        [field: SerializeField]
        public float StopTiltingThreshold { get; private set; } = 0.05f;

        [field: FoldoutGroup("Movement")]
        [field: SerializeField]
        public AnimationCurve AvoidObstacleVelocityRatioToPassedDistanceCurve { get; private set; }

        [field: FoldoutGroup("Movement")]
        [field: MinMaxSlider(0f, 12f, true)]
        [field: SerializeField]
        public Vector2 RandomTiltPointSidesDeltaRange { get; private set; } = new(0, 5f);

        [field: FoldoutGroup("Movement")]
        [field: MinMaxSlider(1f, 100f, true)]
        [field: SerializeField]
        public Vector2 RandomTiltPointForwardDeltaRange { get; private set; } = new(3f, 15f);
        
        [field: FoldoutGroup("Movement")]
        [field: MinMaxSlider(0f, 12f, true)]
        [field: SerializeField]
        public Vector2 RandomTiltMissPointSidesDeltaRange { get; private set; } = new(0, 2f);

        [field: FoldoutGroup("Movement")]
        [field: MinMaxSlider(0f, 100f, true)]
        [field: SerializeField]
        public Vector2 RandomTiltMissPointForwardDeltaRange { get; private set; } = new(3, 15f);

        [field: FoldoutGroup("Movement")]
        [field: SerializeField]
        public AnimationCurve RandomTiltVelocityRatioToPassedDistanceCurve { get; private set; }

        [field: FoldoutGroup("Movement")]
        [field: SerializeField]
        public EChunkType RandomTiltRestrictedChunks { get; private set; }
        

        [field: FoldoutGroup("Obstacle movement")]
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float SuccessMovementDecreaseChanceDelta { get; private set; } = 0.1f;

        [field: FoldoutGroup("Obstacle movement")]
        [field: SerializeField] 
        [field: Range(0f, 1f)]
        public float FailureMovementIncreaseChanceDelta { get; private set; } = 0.2f;

        [field: FoldoutGroup("Obstacle movement")]
        [field: MinMaxSlider(0f, 1, true)]
        [field: SerializeField]
        public Vector2 ObstacleFastMoveChance { get; private set; } = new (0.1f, 0.6f);
        
        [field: FoldoutGroup("Obstacle movement")]
        [field: MinMaxSlider(0f, 1f, true)]
        [field: SerializeField] 
        public Vector2 ObstacleFailChance { get; private set; }  = new (0.1f, 0.2f);

        [field: FoldoutGroup("Obstacle movement")]
        [field: MinMaxSlider(0f, 1f, true)]
        [field: SerializeField]
        public Vector2 JumpFailChance { get; private set; }  = new (0.2f, 0.4f);
        
        [field: FoldoutGroup("Obstacle movement")]
        [field: MinMaxSlider(0f, 1f, true)]
        [field: SerializeField]
        public Vector2 ComebackFailChance { get; private set; }  = new (0.4f, 0.6f);
        

        [field: FoldoutGroup("Turning")]
        [field: MinMaxSlider(0f, 1f, true)]
        [field: SerializeField] 
        public Vector2 TurningFailChance { get; private set; }  = new (0.05f, 0.1f);

        [field: FoldoutGroup("Turning")]
        [field: MinMaxSlider(0f, 15f, true)]
        [field: SerializeField]
        public Vector2 TurnDistanceRange { get; private set; } = new(0f, 6f);
    }
}