using Db;
using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
	[Game]
	[Event(EventTarget.Self)]
	public class ObjectTypeComponent : IComponent
	{
		public EObjectType Value;

		public override string ToString() {
			// Более показать не могу
			return default;
		}
	}
}