using UnityEngine.SceneManagement;
using Zenject;

namespace Tests.Playable.UITest.Utils
{
	public static class DiUtils
	{
		public static DiContainer Container
		{
			get
			{
				var scene = SceneManager.GetActiveScene();
				foreach (var gameObject in scene.GetRootGameObjects())
				{
					var sceneContext = gameObject.GetComponent<Context>();
					if (sceneContext != null)
						return sceneContext.Container;
				}

				return null;
			}
		}
	}
}