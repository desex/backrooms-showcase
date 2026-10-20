using System;
using System.Collections;
using System.IO;
using Tests.Abstracts;
using Tests.Playable.UITest.Conditions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace Tests.Playable.UITest
{
	public abstract partial class UiTest : PdIntegrationTest
	{

		protected static IEnumerator LoadScene(string name)
		{
			return LoadSceneInternal(name);
		}

		private static IEnumerator LoadSceneInternal(string name)
		{
#if UNITY_EDITOR
			if (name.Contains(".unity"))
			{
				EditorSceneManager.LoadSceneInPlayMode(name, new LoadSceneParameters(LoadSceneMode.Single));
				yield return WaitFor(new SceneLoaded(Path.GetFileNameWithoutExtension(name)));
				yield break;
			}
#endif
			SceneManager.LoadScene(name);
			yield return WaitFor(new SceneLoaded(name));
		}


		protected static IEnumerator Press(string buttonName)
		{
			return PressInternal(buttonName);
		}

		protected static IEnumerator WaitWhile(Func<bool> condition, float timeout = 2f)
		{
			yield return WaitUntil(() => !condition(), timeout);
		}
		
		protected static IEnumerator WaitUntil(Func<bool> condition, float timeout = 2f)
		{
			yield return WaitFor(new BoolCondition(condition), timeout);
		}

		protected static IEnumerator Press(GameObject o)
		{
			return PressInternal(o);
		}

		private static IEnumerator ObjectActive(string id)
		{
			var appeared = new ObjectAppeared(id);
			yield return WaitFor(appeared);
		}

		private static IEnumerator ObjectInactive(string id)
		{
			var disappeared = new ObjectDisappeared(id);
			yield return WaitFor(disappeared);
		}

		private static IEnumerator PressInternal(string buttonName)
		{
			var buttonAppeared = new ObjectAppeared(buttonName);
			yield return WaitFor(buttonAppeared);
			yield return Press(buttonAppeared.Obj);
		}

		private static IEnumerator PressInternal(GameObject o)
		{
			yield return WaitFor(new ButtonAccessible(o));
			Debug.Log("Button pressed: " + o);
			ExecuteEvents.Execute(o, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			yield return null;
		}

		protected static IEnumerator WaitFor(ICondition condition, float timeout = 2)
		{
			return WaitForInternal(condition, timeout);
		}
		
		protected static IEnumerator CheckDuringTimeFor(ICondition condition, float duration = 2)
		{
			return CheckConditionContinuously(condition, duration);
		}

		private static IEnumerator WaitForInternal(ICondition condition, float timeout)
		{
			return new WaitCondition(condition, timeout).GetEnumerator();
		}
		
		private static IEnumerator CheckConditionContinuously(ICondition condition, float duration)
		{
			return new ContinuousConditionChecking(condition, duration).GetEnumerator();
		}
	}
}