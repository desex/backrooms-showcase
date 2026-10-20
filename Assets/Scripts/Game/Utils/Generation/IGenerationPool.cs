namespace Game.Utils.Generation
{
    public interface IGenerationPool<in TType, TObject> : IViewPool<TObject>
    {
        (TObject objectView, int randomIndex) SpawnObject(TType type);
        void DespawnObject(TType type, TObject objectView, int randomIndex);
    }
}