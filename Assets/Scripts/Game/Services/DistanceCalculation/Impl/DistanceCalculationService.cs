using System;
using System.Collections.Generic;
using Ecs.Managers;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Services.DistanceCalculation.Impl
{
    public class DistanceCalculationService : IDistanceCalculationService
    {
        private readonly GameContext _game;

        private readonly Dictionary<Vector3, float> _distanceMap = new Dictionary<Vector3, float>();
        private readonly Queue<(Vector3, float)> _distanceQueue = new Queue<(Vector3, float)>();
        private readonly List<SpawnPoint> _spawnPositions = new List<SpawnPoint>();

        public DistanceCalculationService(GameContext game)
        {
            _game = game;
        }

        public float GetPlayerDistance(IEnumerable<ChunkSegment> segments)
        {
            var player = _game.PlayerEntity;

            var playerDistance = GetDistance(player.Position.Value, segments);
            return playerDistance;
        }

        public float GetCharacterDistance(Uid characterUid, IEnumerable<ChunkSegment> segments)
        {
            var character = _game.GetEntityWithUid(characterUid);

            return GetDistance(character.Position.Value, segments);
        }

        public float GetDistance(Vector3 target, IEnumerable<ChunkSegment> segments)
        {
            if (_distanceMap.Count == 0)
                return 0;

            var distance = CalculateDistance(target, out var projectedPoint, segments);

            return distance;
        }

        public bool TryGetPositions(float targetDistance, out List<SpawnPoint> spawnPositions, IEnumerable<ChunkSegment> segments)
        {
            _spawnPositions.Clear();

            foreach (var segment in segments)
                CalculateSpawnPointWithinRange(targetDistance, segment);

            if (_spawnPositions.Count == 0)
            {
                spawnPositions = null;
                return false;
            }

            spawnPositions = _spawnPositions;
            return true;
        }

        public EChunkSide GetChunkSide(Vector3 worldPosition, IEnumerable<ChunkSegment> segments)
        {
            using var minDistance = new NativeReference<float>(float.MaxValue, Allocator.TempJob);

            using var bestSegmentStart = new NativeReference<float3>(float3.zero, Allocator.TempJob);
            using var bestSegmentEnd = new NativeReference<float3>(float3.zero, Allocator.TempJob);

            foreach (var segment in segments)
            {
                if (segment.Start == default)
                    continue;

                if (!_distanceMap.ContainsKey(segment.Start))
                    continue;

                new CheckSegmentSideJob
                {
                    Start = segment.Start,
                    End = segment.Next1,
                    WorldPos = worldPosition,
                    MinDistance = minDistance,
                    BestSegmentStart = bestSegmentStart,
                    BestSegmentEnd = bestSegmentEnd,
                }.Run();

                new CheckSegmentSideJob
                {
                    Start = segment.Start,
                    End = segment.Next2,
                    WorldPos = worldPosition,
                    MinDistance = minDistance,
                    BestSegmentStart = bestSegmentStart,
                    BestSegmentEnd = bestSegmentEnd,
                }.Run();
            }

            if (Mathf.Approximately(minDistance.Value, float.MaxValue))
            {
                Debug.LogError("Can't find chunk side");
                return EChunkSide.Left;
            }

            var start2D = new Vector2(bestSegmentStart.Value.x, bestSegmentStart.Value.z);
            var end2D = new Vector2(bestSegmentEnd.Value.x, bestSegmentEnd.Value.z);
            var pos2D = new Vector2(worldPosition.x, worldPosition.z);

            var forward2D = (end2D - start2D).normalized;
            var toPosition2D = pos2D - start2D;

            var crossValue = forward2D.x * toPosition2D.y - forward2D.y * toPosition2D.x;

            return crossValue > 0 ? EChunkSide.Left : EChunkSide.Right;
        }

        private void CalculateSpawnPointWithinRange(float targetDistance, ChunkSegment segment)
        {
            if (segment.Start == default)
                return;

            if (!_distanceMap.ContainsKey(segment.Start))
                return;

            if (segment.ChunkType is EChunkType.TurnBothSides or EChunkType.TurnBothSidesBody)
            {
                CalculateSpawnPointWithinRange(targetDistance, segment, segment.Next1);
                CalculateSpawnPointWithinRange(targetDistance, segment, segment.Next2);
            }
            else
            {
                CalculateSpawnPointWithinRange(targetDistance, segment, segment.Next1);
            }
        }

        private void CalculateSpawnPointWithinRange(float targetDistance, ChunkSegment segment, Vector3 next)
        {
            if (!_distanceMap.TryGetValue(segment.Start, out var startDist))
                return;

            if (!_distanceMap.TryGetValue(next, out var endDist))
                return;

            if (targetDistance < startDist)
                return;

            if (targetDistance > endDist)
                return;

            var distance = (targetDistance - startDist) / (endDist - startDist);
            var markerPosition = Vector3.Lerp(segment.Start, next, distance);

            var spawnPoint = segment.MiddlePoints.Count == 0
                ? GetSpawnPointInRegularChunk(segment, markerPosition)
                : GetSpawnPointInSegmentedChunk(segment, markerPosition);

            _spawnPositions.Add(spawnPoint);
        }

        private static SpawnPoint GetSpawnPointInRegularChunk(ChunkSegment segment, Vector3 markerPosition)
        {
            return new SpawnPoint(
                markerPosition,
                segment.ThisUId,
                segment.ChunkType,
                GetDirection(segment.Start, segment.Next1)
            );
        }

        private static SpawnPoint GetSpawnPointInSegmentedChunk(ChunkSegment segment, Vector3 markerPosition)
        {
            if (segment.MiddlePoints.Count > 2)
                throw new Exception(
                    nameof(DistanceCalculationService)
                    + ".cs: Current implementation cannot handle more than 2 middle points. You should improve this method"
                );

            Vector3 startAnchor = default;
            Vector3 endAnchor = default;

            switch (segment.Segment)
            {
                case ESegment.Start:
                    startAnchor = segment.Start;
                    endAnchor = segment.MiddlePoints[0];
                    break;

                case ESegment.Middle:
                    startAnchor = segment.MiddlePoints[0];
                    endAnchor = segment.MiddlePoints[^1];
                    break;

                case ESegment.End:
                    startAnchor = segment.MiddlePoints[^1];
                    endAnchor = segment.Next1;
                    break;

                case ESegment.None:
                case ESegment.Everything:
                default:
                    break;
            }

            return new SpawnPoint(
                markerPosition,
                segment.ThisUId,
                segment.ChunkType,
                GetDirection(startAnchor, endAnchor)
            );
        }

        private static Quaternion GetDirection(Vector3 startAnchor, Vector3 endAnchor)
        {
            var direction = endAnchor - startAnchor;

            return direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
        }

        public void CalculateDistances(IReadOnlyList<ChunkSegment> segments)
        {
            _distanceQueue.Clear();
            _distanceMap.Clear();

            if (segments.Count > 0)
            {
                var segment = segments[0];

                if (segment.Start != default)
                {
                    _distanceQueue.Enqueue((segment.Start, segment.StartDistance));
                    _distanceMap[segment.Start] = segment.StartDistance;
                }
            }

            while (_distanceQueue.Count > 0)
            {
                var (current, currentDistance) = _distanceQueue.Dequeue();

                foreach (var segment in segments)
                {
                    if (segment.Start != current)
                        continue;

                    CalculateSingle(segment.Next1, segment.DistanceToNext1, currentDistance);
                    CalculateSingle(segment.Next2, segment.DistanceToNext2, currentDistance);
                }
            }
        }

        private void CalculateSingle(Vector3 next, float offset, float currentDistance)
        {
            if (next == default)
                return;

            if (_distanceMap.ContainsKey(next))
                return;

            var newDistance = currentDistance + offset;

            _distanceMap[next] = newDistance;
            _distanceQueue.Enqueue((next, newDistance));
        }

        private float CalculateDistance(Vector3 point, out Vector3 projectedPoint, IEnumerable<ChunkSegment> segments)
        {
            var minDistance = float.MaxValue;
            var projectedDistance = 0f;
            projectedPoint = Vector3.zero;

            foreach (var segment in segments)
            {
                if (segment.Start == default)
                    continue;

                if (!_distanceMap.ContainsKey(segment.Start))
                    continue;

                minDistance = GetMinDistance(
                    point,
                    ref projectedPoint,
                    segment,
                    minDistance,
                    ref projectedDistance,
                    segment.Next1
                );

                minDistance = GetMinDistance(
                    point,
                    ref projectedPoint,
                    segment,
                    minDistance,
                    ref projectedDistance,
                    segment.Next2
                );
            }

            return projectedDistance;
        }

        private float GetMinDistance(
            Vector3 point,
            ref Vector3 projectedPoint,
            ChunkSegment segment,
            float minDistance,
            ref float projectedDistance,
            Vector3 nextPosition
        )
        {
            if (nextPosition == default)
                return minDistance;

            if (!_distanceMap.ContainsKey(segment.Start))
                return minDistance;

            var projection = ProjectPointOnSegment(segment.Start, nextPosition, point);
            var distanceToProjection = Vector3.Distance(point, projection);

            if (distanceToProjection >= minDistance)
                return minDistance;

            minDistance = distanceToProjection;
            projectedPoint = projection;
            projectedDistance = _distanceMap[segment.Start] + Vector3.Distance(segment.Start, projection);

            return minDistance;
        }

        private static float3 ProjectPointOnSegment(float3 start, float3 end, float3 projection)
        {
            var startEndVector = end - start;
            var startProjectionVector = projection - start;

            var magnitudeStartEnd = math.lengthsq(startEndVector);
            var startEndStartProjectionProduct = math.dot(startProjectionVector, startEndVector);
            var distance = startEndStartProjectionProduct / magnitudeStartEnd;

            return distance switch
            {
                < 0 => start,
                > 1 => end,
                _ => start + distance * startEndVector,
            };
        }

        [BurstCompile]
        private struct CheckSegmentSideJob : IJob
        {
            [ReadOnly]
            public float3 Start;

            [ReadOnly]
            public float3 End;

            [ReadOnly]
            public float3 WorldPos;

            public NativeReference<float> MinDistance;

            [WriteOnly]
            public NativeReference<float3> BestSegmentStart;

            [WriteOnly]
            public NativeReference<float3> BestSegmentEnd;

            [BurstCompile]
            public void Execute()
            {
                if (math.all(End == float3.zero))
                    return;

                var projection = ProjectPointOnSegment(Start, End, WorldPos);
                var distanceToProjection = math.distance(WorldPos, projection);

                if (distanceToProjection >= MinDistance.Value)
                    return;

                MinDistance.Value = distanceToProjection;
                BestSegmentStart.Value = Start;
                BestSegmentEnd.Value = End;
            }
        }
    }
}