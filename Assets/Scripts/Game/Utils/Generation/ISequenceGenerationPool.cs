namespace Game.Utils.Generation
{
    public interface ISequenceGenerationPool<TObject> : IViewPool<TObject>
    {
        TObject SpawnObject(int instanceID);
        void DespawnObject(int instanceID, TObject objectView);
    }
}