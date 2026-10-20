namespace Game.Utils.Tutorial
{
    public readonly struct TutorialChunkData
    {
        public readonly ETutorialState State;
        public readonly int Index;

        public TutorialChunkData(ETutorialState state, int index)
        {
            State = state;
            Index = index;
        }
    }
}