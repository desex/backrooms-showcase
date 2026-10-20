using Ecs.Views.Linkable.Impl;
using UnityEngine;

namespace Game.Utils.Characters
{
    public class HashProvider : MonoBehaviour
    {
        [SerializeField] ObjectView _objectView;
        
        public int Hash => _objectView.Hash;
    }
}