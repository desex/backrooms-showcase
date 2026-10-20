namespace Game.Services.CollectablesItem
{
    public interface ICollectablesItemPool<in TType, TObject>
    {
        TObject Spawn(TType type);
        void Despawn(TType type, TObject objectView);
    }
}