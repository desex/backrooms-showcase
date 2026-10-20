using UnityEngine;

namespace Db.Prefabs
{
    public interface IPrefabsBase
    {
        GameObject Get(string prefabName);
    }
}