namespace Db.HoleGenerator
{
    public interface IHoleGeneratorPrefabsParameters
    {
        HoleGeneratorPrefabVo[] GetPrefab(EGeneratorPrefab prefabType);
    }
}