using UnityEngine;
using Utils.Drawers.Key;

namespace Db.Fx.Impl
{
	[CreateAssetMenu(menuName = "Settings/" + nameof(FxObjectBase), fileName = nameof(FxObjectBase))]
	public class FxObjectBase : ScriptableObject, IFxObjectBase
	{
		[KeyValue(nameof(FxPrefab.type))] [SerializeField] private FxPrefab[] fxObjects;
		public FxPrefab[] FxObjects => fxObjects;
	}
}