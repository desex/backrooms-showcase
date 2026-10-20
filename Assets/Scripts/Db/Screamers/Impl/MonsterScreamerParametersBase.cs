using UnityEngine;

namespace Db.Screamers.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(MonsterScreamerParametersBase), fileName = nameof(MonsterScreamerParametersBase))]
    public class MonsterScreamerParametersBase : ScriptableObject, IMonsterScreamerParameters
    {
        [field: Tooltip("In seconds")]
        [field: Range(0, 2f)]
        [field: SerializeField] 
        public float IdleDuration { get; private set; }  
        
        [field: Space]
        [field: Range(0, 10f)]
        [field: SerializeField] 
        public float MoveDelta { get; private set; }
    }
}