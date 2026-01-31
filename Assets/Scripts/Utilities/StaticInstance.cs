using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// A static instance is similar to a singleton, but instead of destroying any new
    /// instances, it overrides the current instance. This is handy for resetting the state
    /// and saves you doing it manually
    /// </summary>
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        /// <summary>
        /// Whether another instance already exists. If this is set to true, it means that the associated
        /// gameObject is either already or about to be destroyed.
        /// </summary>
        protected bool _otherInstanceExists = false;
        
        protected override void Awake()
        {
            Debug.Log("hello");
            if (Instance != null)
            {
                Destroy(gameObject);
                _otherInstanceExists = true;
                return;
            }

            base.Awake();
        }
    }

    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}