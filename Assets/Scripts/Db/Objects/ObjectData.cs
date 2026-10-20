using System;
using Db.Objects.Vo;
using UnityEngine;

namespace Db.Objects
{
	[Serializable]
	public class ObjectData : ObjectVo
	{
		public GameObject[] prefabs;
	}
}