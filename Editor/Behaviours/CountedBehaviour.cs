
using UnityEngine;

namespace ExpandedBehaviours.Behaviours
{
    /// <summary>
    /// MonoBehaviour that counts that keeps track of the amount of instances
    /// created of it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CountedBehaviour : MonoBehaviour
    {
        public static int InstanceCount
        {
            get; private set;
        }

        protected void Start ()
        {
            InstanceCount++;
        }

        protected void OnDestroy ()
        {
            InstanceCount--;
        }
    }
}
