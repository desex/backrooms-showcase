using System;
using System.Collections.Generic;
using Game.Utils.Chunks;
using Game.Utils.Chunks.Pool;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.DistanceCalculation.Impl
{
    public class ChunkSegmentService : IChunkSegmentService
    {
        private readonly List<Transform> _anchors = new List<Transform>();
        private readonly IChunksSegmentPool _chunksSegmentPool;
        private readonly List<ChunkEndAnchor> _ends = new List<ChunkEndAnchor>();
        private readonly GameContext _game;
        private readonly List<ChunkSegment> _localSingleChunkSegments = new List<ChunkSegment>();

        private readonly List<ChunkSegment> _segments = new List<ChunkSegment>();

        private float _playerStartOffset;

        public ChunkSegmentService(
            GameContext game,
            IChunksSegmentPool chunksSegmentPool
        )
        {
            _game = game;
            _chunksSegmentPool = chunksSegmentPool;
        }

        public IReadOnlyList<ChunkSegment> Segments => _segments;
        
        public ChunkSegment LastChunkSegment => Segments[^1];

        public List<ChunkSegment> RegisterChunk(GameEntity chunkEntity)
        {
            var chunkAnchors = chunkEntity.ChunkAnchors.Value;
            var startPos = chunkAnchors.StartAnchor.transform.position;

            var chunkType = chunkEntity.ChunkNode.Value.ChunkType;

            _localSingleChunkSegments.Clear();

            if (ChunkUtility.IsTurnBothSides(chunkType))
                ProcessTChunk(chunkEntity, chunkAnchors, startPos);
            else if (chunkType is EChunkType.TransitionHole 
                               or EChunkType.TransitionTruck 
                               or EChunkType.TransitionTrain)
                ProcessTransitionHole(chunkEntity, chunkAnchors, startPos);
            else
                ProcessRegularChunk(chunkEntity, chunkAnchors, startPos);

            return _localSingleChunkSegments;
        }

        public void ApplyDistanceOffset(float offset)
        {
            foreach (var chunkSegment in _segments)
            {
                chunkSegment.StartDistance -= offset;
                chunkSegment.EndDistance -= offset;
            }

            for (var index = 0; index < _ends.Count; index++)
            {
                var oldEndAnchor = _ends[index];

                var newChunkEndAnchor = new ChunkEndAnchor(
                    oldEndAnchor.Position,
                    oldEndAnchor.Distance - offset,
                    oldEndAnchor.ChunkUid,
                    oldEndAnchor.ChunkType
                );

                _ends[index] = newChunkEndAnchor;
            }
        }

        public void UnregisterChunk(GameEntity chunkEntity)
        {
            for (var i = _segments.Count - 1; i >= 0; i--)
            {
                var segment = _segments[i];

                if (segment.ThisUId != chunkEntity.Uid.Value)
                    continue;

                _segments.RemoveAt(i);
                _chunksSegmentPool.Return(segment);
                _ends.RemoveAll(endAnchor => endAnchor.ChunkUid == segment.ThisUId);
            }
        }

        public void CorrectEndAnchor(ChunkSegment segment, Vector3 startPosition, Vector3 next1Position, float startDistance)
        {
            if (segment.ChunkType != EChunkType.Straight)
                throw new Exception(
                    nameof(ChunkSegmentService) + ".cs: Current logic cannot handle complex chunk modification"
                );

            segment.Start = startPosition;

            segment.Next1 = next1Position;

            var distanceToNext = Vector3.Distance(segment.Start, segment.Next1);
            segment.DistanceToNext1 = distanceToNext;

            if (segment.DistanceToNext2 != 0f)
                segment.DistanceToNext2 = distanceToNext;

            segment.StartDistance = startDistance;

            segment.EndDistance = segment.StartDistance + distanceToNext;

            for (var i = 0; i < _ends.Count; i++)
            {
                var chunk = _ends[i];

                if (chunk.ChunkUid != segment.ThisUId)
                    continue;

                _ends[i] = new(next1Position, segment.EndDistance, segment.ThisUId, chunk.ChunkType);
            }
        }

        public void SetStartOffset(float offset)
        {
            _playerStartOffset = offset;
        }

        private void ProcessRegularChunk(
            GameEntity chunkEntity,
            ChunkAnchors chunkAnchors,
            Vector3 startPos
        )
        {
            if (chunkAnchors.MiddleAnchors is not { Length: > 0, })
                ProcessRegularSingleChunk(chunkEntity, chunkAnchors, startPos);
            else
                ProcessRegularSegmentedChunk(chunkEntity, chunkAnchors);
        }

        private void ProcessRegularSingleChunk(
            GameEntity chunkEntity,
            ChunkAnchors chunkAnchors,
            Vector3 startPos
        )
        {
            var endOnlyPos = chunkAnchors.EndOnlyAnchor.transform.position;
            var distance = Vector3.Distance(startPos, endOnlyPos);

            var startDistance = 0f;
            var endDistance = 0f;

            if (_segments.Count == 0)
            {
                var chunk = _game.GetEntityWithUid(chunkEntity.Uid.Value);

                startDistance = chunk.ChunkNode.Value.PreviousNode == null
                    ? -_playerStartOffset
                    : 0;

                endDistance = Vector3.Distance(Vector3.zero, endOnlyPos);
            }
            else
            {
                startDistance = ChooseChunkEnd(startPos);
                endDistance = startDistance + distance;
            }

            var pointNode = GetSegment(
                chunkEntity,
                ESegment.None,
                startPos,
                endOnlyPos,
                default,
                null,
                distance,
                0f,
                startDistance,
                endDistance
            );

            Register(pointNode);
            _localSingleChunkSegments.Add(pointNode);
        }

        private void ProcessRegularSegmentedChunk(
            GameEntity chunkEntity,
            ChunkAnchors chunkAnchors
        )
        {
            _anchors.Clear();

            _anchors.Add(chunkAnchors.StartAnchor);
            _anchors.AddRange(chunkAnchors.MiddleAnchors);
            _anchors.Add(chunkAnchors.EndOnlyAnchor);

            for (var i = 0; i < _anchors.Count - 1; i++)
            {
                var currentAnchor = _anchors[i];
                var nextAnchor = _anchors[i + 1];
                var distanceToNext1 = Vector3.Distance(currentAnchor.transform.position, nextAnchor.transform.position);

                var startDistance = ChooseChunkEnd(_anchors[i].position);

                var endDistance = startDistance + distanceToNext1;

                var segment = GetSegment(
                    chunkEntity,
                    SegmentZone(i, _anchors),
                    currentAnchor.transform.position,
                    nextAnchor.transform.position,
                    default,
                    _anchors,
                    distanceToNext1,
                    0f,
                    startDistance,
                    endDistance
                );

                Register(segment);
                _localSingleChunkSegments.Add(segment);
            }
        }

        private static ESegment SegmentZone(int i, List<Transform> anchors)
        {
            var segmentZone = ESegment.None;

            if (i == 0)
                segmentZone = ESegment.Start;

            if (i == anchors.Count - 2)
                segmentZone = ESegment.End;

            if (segmentZone == ESegment.None)
                segmentZone = ESegment.Middle;

            return segmentZone;
        }

        private void ProcessTChunk(
            GameEntity chunkEntity,
            ChunkAnchors chunkAnchors,
            Vector3 startPos
        )
        {
            if (_segments.Count == 0)
                throw new("T Chunk cant be the first one in generation");

            var startDistance = 0f;

            startDistance = ChooseChunkEnd(startPos);

            var nextLeft = chunkAnchors.EndLeftAnchor.transform.position;
            var nextRight = chunkAnchors.EndRightAnchor.transform.position;
            var distanceToNext1 = Vector3.Distance(startPos, nextLeft);
            var distRight = Vector3.Distance(startPos, nextRight);

            if (Mathf.Abs(distanceToNext1 - distRight) > 0.1f)
                Debug.LogError("T chunk is not symmetrical. This logic should be improved");

            var segmentEndDistance = startDistance + distanceToNext1;

            var segment = GetSegment(
                chunkEntity,
                ESegment.None,
                startPos,
                nextLeft,
                nextRight,
                null,
                distanceToNext1,
                distRight,
                startDistance,
                segmentEndDistance
            );

            Register(segment);
            _localSingleChunkSegments.Add(segment);
        }

        private void ProcessTransitionHole(
            GameEntity chunkEntity,
            ChunkAnchors chunkAnchors,
            Vector3 startPos
        )
        {
            if (_segments.Count == 0)
                throw new("T Chunk cant be the first one in generation");

            var startDistance = 0f;

            startDistance = ChooseChunkEnd(startPos);

            var top = chunkAnchors.EndLeftAnchor.transform.position;
            var bottom = chunkAnchors.EndRightAnchor.transform.position;
            var startBottomPos = new Vector3(startPos.x, bottom.y, startPos.z);
            var distanceToTop = Vector3.Distance(startPos, top);
            var distanceToBottom = Vector3.Distance(startBottomPos, top);

            var segmentEndDistance = startDistance + distanceToTop;

            var segment = GetSegment(
                chunkEntity,
                ESegment.None,
                startPos,
                top,
                bottom,
                null,
                distanceToTop,
                distanceToBottom,
                startDistance,
                segmentEndDistance
            );

            Register(segment);
            _localSingleChunkSegments.Add(segment);
        }

        private void Register(ChunkSegment chunkSegment)
        {
            _segments.Add(chunkSegment);

            var chunkEndAnchor = new ChunkEndAnchor(
                chunkSegment.Next1,
                chunkSegment.EndDistance,
                chunkSegment.ThisUId,
                chunkSegment.ChunkType
            );

            _ends.Add(chunkEndAnchor);

            if (chunkSegment.Next2 == default)
                return;

            var endAnchor = new ChunkEndAnchor(
                chunkSegment.Next2,
                chunkSegment.EndDistance,
                chunkSegment.ThisUId,
                chunkSegment.ChunkType
            );

            _ends.Add(endAnchor);
        }

        private float ChooseChunkEnd(Vector3 startPosition)
        {
            if (_ends == null)
                throw new NullReferenceException("ChunkEnds is null");

            if (_ends.Count == 0)
                throw new NullReferenceException("ChunkEnds is empty");

            var startDistance = 0f;

            if (_ends.Count == 1)
            {
                startDistance = _ends[0].Distance;

                _ends.RemoveAt(0);

                return startDistance;
            }

            var closestDistance = float.MaxValue;
            var index = int.MaxValue;

            for (var i = _ends.Count; i > 0; i--)
            {
                var chunkEnd = _ends[i - 1];

                var distance = Vector3.Distance(startPosition, chunkEnd.Position);

                if (distance >= closestDistance)
                    continue;

                closestDistance = distance;
                index = i - 1;
            }

            startDistance = _ends[index].Distance;
            DeleteSegmentByIndex(index);

            return startDistance;
        }

        private ChunkSegment GetSegment(
            GameEntity chunkEntity,
            ESegment none,
            Vector3 startPos,
            Vector3 nextLeft,
            Vector3 nextRight,
            List<Transform> middlePointTransforms,
            float distanceToNext1,
            float distanceToNext2,
            float startDistance,
            float endDistance
        )
        {
            var segment = _chunksSegmentPool.Get();

            var segmentInitializeData = new ChunkSegmentInitializeData
            {
                ChunkEntity = chunkEntity,
                Segment = none,
                Start = startPos,
                Next1 = nextLeft,
                Next2 = nextRight,
                DistanceToNext1 = distanceToNext1,
                DistanceToNext2 = distanceToNext2,
                StartDistance = startDistance,
                EndDistance = endDistance,
            };
            
            if (segment != null)
                segment.Initialize(segmentInitializeData);
            else
                segment = new(segmentInitializeData);

            if (middlePointTransforms == null)
                return segment;

            for (var j = 1; j < middlePointTransforms.Count - 1; j++)
            {
                var middleAnchor = middlePointTransforms[j];
                segment.AddMiddlePoint(middleAnchor.position);
            }

            return segment;
        }

        private void DeleteSegmentByIndex(int index)
        {
            _ends.RemoveAt(index);
        }
    }
}