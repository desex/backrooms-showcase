namespace Game.Utils
{
    public interface IResettableView
    {
        void SaveState();
        void ResetState();
    }
}