using System.Collections;
using SimpleUi.Abstracts;
using Tests.Playable.UITest.Conditions;
using Tests.Playable.UITest.Custom.Conditions;
using Tests.Playable.UITest.Utils;

namespace Tests.Playable.UITest
{
	public abstract partial class UiTest
	{
		protected static IEnumerator AssertLabel(string id, string text)
		{
			return AssertLabelInternal(id, text);
		}

		protected static IEnumerator AssertToggle(string id, bool val)
		{
			return AssertToggleInternal(id, val);
		}

		protected static IEnumerator AssertSlider(string id, float val)
		{
			return AssertSliderValueInternal(id, val);
		}

		protected static IEnumerator AssertObjectEulerRotationZ(string id, Interval val)
		{
			return AssertObjectEulerRotationZInternal(id, val);
		}

		protected static IEnumerator AssertObjectActive(string id)
		{
			return ObjectActive(id);
		}

		protected static IEnumerator AssertObjectInactive(string id)
		{
			return ObjectInactive(id);
		}

		protected static IEnumerator AssertObjectArrayActive(params string[] ids)
		{
			foreach (var id in ids)
			{
				yield return AssertObjectActive(id);
			}

			yield return null;
		}

		protected static IEnumerator AssertObjectArrayInactive(params string[] ids)
		{
			foreach (var id in ids)
			{
				yield return AssertObjectInactive(id);
			}

			yield return null;
		}

		protected static IEnumerator AssertUiViewInactive<TView>(bool projectScope = false, float timeout = 2) where TView : UiView
		{
			return WaitFor(new UiViewInactive<TView>(projectScope), timeout);
		}
		
		protected static IEnumerator AssertUiViewActive<TView>(bool projectScope = false, float timeout = 2) where TView : UiView
		{
			return WaitFor(new UiViewActive<TView>(projectScope), timeout);
		}
		
		private static IEnumerator AssertLabelInternal(string id, string text)
		{
			yield return WaitFor(new LabelTextAppeared(id, text));
		}

		private static IEnumerator AssertToggleInternal(string id, bool val)
		{
			yield return WaitFor(new ToggleValueAppeared(id, val));
		}

		private static IEnumerator AssertSliderValueInternal(string id, float val)
		{
			yield return WaitFor(new AssertSliderValue(id, val));
		}

		private static IEnumerator AssertObjectEulerRotationZInternal(string id, Interval val)
		{
			yield return WaitFor(new ObjectRotationZCondition(id, val));
		}
	}
}