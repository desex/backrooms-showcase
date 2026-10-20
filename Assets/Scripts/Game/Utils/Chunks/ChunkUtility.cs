namespace Game.Utils.Chunks
{
    public static class ChunkUtility
    {
        private static readonly EChunkType _turnMask =
            EChunkType.TurnLeft
          | EChunkType.TurnRight
          | EChunkType.TurnBothSides
          | EChunkType.TurnLeftBody
          | EChunkType.TurnRightBody
          | EChunkType.TurnBothSidesBody
          | EChunkType.TutorialTurnLeftV1
          | EChunkType.TutorialTurnRightV1
          | EChunkType.TutorialTurnRightV2
          | EChunkType.TutorialTurnLeftV2;

        private static readonly EChunkType _turnBothMask =
            EChunkType.TurnBothSides | EChunkType.TurnBothSidesBody;

        private static readonly EChunkType _twoAnchorsMask = EChunkType.TurnBothSides
                                                             | EChunkType.TurnBothSidesBody
                                                             | EChunkType.TransitionHole
                                                             | EChunkType.TransitionTruck
                                                             | EChunkType.TransitionTrain;

        public static bool IsTurnBothSides(EChunkType chunkType)
        {
            return (chunkType & _turnBothMask) != EChunkType.None;
        }

        public static bool HasTwoAnchors(EChunkType chunkType)
        {
            return (chunkType & _twoAnchorsMask) != EChunkType.None;
        }

        public static bool IsTurn(EChunkType chunkType)
        {
            return (chunkType & _turnMask) != EChunkType.None;
        }
    }
}