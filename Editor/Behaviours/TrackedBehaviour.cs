using System;
using System.Collections.Generic;

using UnityEngine;

namespace ExpandedBehaviours.Behaviours
{
    /// <summary>
    /// MonoBehaviour that keeps track of all it's created instances, a more
    /// complex variant of CountedBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TrackedBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public Guid InstanceId
        {
            get; private set;
        }

        private static Dictionary<Guid, T> _instanceTable = new Dictionary<Guid, T>();

        public static int InstanceCount => _instanceTable.Count;

        protected void Start ()
        {
            this.InstanceId = Guid.NewGuid();
            _instanceTable.Add(this.InstanceId, this.ExtractInstance());
        }

        protected void OnDestroy ()
        {
            _ = _instanceTable.Remove(this.InstanceId);
        }

        public T this[Guid identifier] => _instanceTable[identifier];

        public bool TryGetInstance (Guid identifier, out T instance)
        {
            return _instanceTable.TryGetValue(identifier, out instance);
        }

        /// <summary>
        /// Used to extract the generic type from it's wrapper.
        /// </summary>
        /// <returns>Extracted type.</returns>
        protected abstract T ExtractInstance ();
    }
}
