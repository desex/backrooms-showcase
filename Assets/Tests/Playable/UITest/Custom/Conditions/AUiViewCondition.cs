using SimpleUi.Abstracts;
using Tests.Playable.UITest.Conditions;
using UnityEngine;

namespace Tests.Playable.UITest.Custom.Conditions
{
	public abstract class AUiViewCondition<TView> : Condition<TView> where TView : UiView
	{
		private readonly bool _projectScope;

		protected AUiViewCondition(bool projectScope)
		{
			_projectScope = projectScope;
		}
		
		protected Canvas GetCanvas()
		{
			var canvases = Object.FindObjectsOfType<Canvas>();
			foreach (var canvas in canvases)
			{
				if (canvas.name.Contains("Tutorial") || canvas.name.Contains("DebugCanvas"))
					continue;
				
				var isProjectCanvas = canvas.name.Contains("Project");
				if (isProjectCanvas == _projectScope)
					return canvas;
			}
			return null;
		}
	}
}