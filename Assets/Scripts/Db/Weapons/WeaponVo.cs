using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Db.Weapons
{
	[Serializable]
	public struct WeaponVo
	{
		[JsonProperty("objectId")]
		public EObjectType type;
		[JsonProperty("attackTypeId")]
		public EWeaponAttackType attackType;
		public PdUtils.Range damage;
		public float maxAttackDistance;
		public float reloadTimeSec;
		public float spread;
		public int count;
		public float criticalDamageMultiplier;
		public float criticalStrikeProbability;

		public int CritPercent => Mathf.RoundToInt(criticalStrikeProbability * 100f);
	}
}