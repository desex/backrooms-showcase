using UnityEngine;

namespace Tools
{
    public class RemoveThisGameObject : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_EDITOR
            Destroy(gameObject);
#else
            Debug.LogWarning("Remove this GameObject before task finish " + gameObject.name);
#endif
        }
    }
}