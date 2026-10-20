using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Ai.Impl
{
	[CreateAssetMenu(menuName = "Databases/Ai/" + nameof(AiBTreeSettingsBase), fileName = nameof(AiBTreeSettingsBase))]
	public class AiBTreeSettingsBase : SerializedScriptableObject, IAiBTreeSettingsBase
	{
		[OdinSerialize] private Dictionary<EAiType, ExternalBehaviorTree> _treesByType;
		
		public IReadOnlyDictionary<EAiType, ExternalBehaviorTree> Trees => _treesByType;
	}
}