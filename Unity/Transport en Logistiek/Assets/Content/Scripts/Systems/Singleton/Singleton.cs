using UnityEngine;

// Singleton is like making a script accessible like a static class.
// Singleton can only be used if there's ONE object in the scene with that script. (useful for managers)
// Inherit the singleton script and acces by typing 'ManagerScript.Instance.*something*'.

namespace Systems.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance { get; set; }

        public static T Instance()
        {
            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name);
                instance = go.AddComponent<T>();

                SetParent(go);
            }

            return instance;
        }

        private static void SetParent(GameObject go)
        {
            //check current parent
            if (go.transform.parent != null)
                return;

            //find system
            var singletonGO = GameObject.Find("Singletons");
            if (singletonGO == null)
                singletonGO = new GameObject("Singletons");

            go.transform.SetParent(singletonGO.transform);
        }
    }
}