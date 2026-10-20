namespace Game.Services.PoolCleaner
{
    public interface IClearablePool
    {
        void CreateSubPools();
        public void Clear();
    }
}