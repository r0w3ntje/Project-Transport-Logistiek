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
            return instance;
        }
    }
}