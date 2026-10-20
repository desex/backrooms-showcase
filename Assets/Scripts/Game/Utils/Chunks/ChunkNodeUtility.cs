namespace Game.Utils.Chunks
{
    public static class ChunkNodeUtility
    {
        public static ChunkNode GetHead(ChunkNode chunkNode)
        {
            while (true)
            {
                if (chunkNode == null)
                    return null;

                if (chunkNode.PreviousNode == null)
                    return chunkNode;

                chunkNode = chunkNode.PreviousNode;
            }
        }
    }
}