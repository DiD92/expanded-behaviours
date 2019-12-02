
using UnityEngine;

namespace ExpandedBehaviours.Behaviours
{
    public sealed class NoValidCandidateException : System.Exception
    {
        public NoValidCandidateException (string message) : base(message) { }

        public NoValidCandidateException (string message, System.Exception innerException) : 
            base(message, innerException) { }
    }

    /// <summary>
    /// Base class used to build singleton MonoBehaviour components.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    SingletonBehaviour<T> candidate = FindObjectOfType<SingletonBehaviour<T>>();

                    if (candidate == null)
                    {
                        throw new NoValidCandidateException("No valid singletion candidate found!");
                    }

                    _instance = candidate.ExtractInstance();

                    #if UNITY_EDITOR
                    Debug.Log($"Created singleton for {_instance.ToString()}");
                    #endif

                    candidate.InitSingleton();
                }

                return _instance;
            }
        }

        protected void Start ()
        {
            if (Instance != this.ExtractInstance())
            {
                #if UNITY_EDITOR
                Debug.Log($"Destroyed gameobject {this} since singleton is already assigned!");
                #endif

                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Place the code you would place on the Start method here.
        /// </summary>
        protected abstract void InitSingleton ();

        /// <summary>
        /// Used to extract the generic type from it's wrapper.
        /// </summary>
        /// <returns>Extracted type.</returns>
        protected abstract T ExtractInstance ();
    }
}
