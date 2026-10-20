using SimpleUi.Abstracts;

namespace Tests.Playable.UITest.Custom.Conditions
{
	public class UiViewActive<TView> : AUiViewCondition<TView> where TView : UiView
	{
		public UiViewActive(bool projectScope) : base(projectScope)
		{
		}
		
		public override bool Satisfied()
		{
			var canvas = GetCanvas();
			if (canvas == null)
				return false;
			
			var views = canvas.gameObject.GetComponentsInChildren<TView>(false);
			return views.Length == 1 && views[0].gameObject.activeSelf;
		}

		public override string ToString()
		{
			return $"Active view({typeof(TView).Name})";
		}
	}
}