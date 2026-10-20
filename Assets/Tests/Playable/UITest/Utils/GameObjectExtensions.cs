using JCMG.EntitasRedux;
using UnityEngine;

namespace Tests.Playable.UITest.Utils
{
	public static class GameObjectExtensions
	{

		public static bool HasEntityLink(this GameObject gameObject)
		{
			var link = gameObject.GetEntityLink();
			return link != null && link.Entity != null;
		}
		
	}
}