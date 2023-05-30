using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPool<T> where T : Component
    {
        private T prefab;
        private Transform parentTransform;
        private Queue<T> availableObjects = new Queue<T>();

        public ObjectPool(T prefab, int initialSize, Transform parentTransform)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            GrowPool(initialSize);
        }

        public T GetFromPool(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (availableObjects.Count == 0)
            {
                GrowPool(10);
            }

            T instance = availableObjects.Dequeue();
            Transform instanceTransform = instance.transform;

            instanceTransform.SetPositionAndRotation(position, rotation);
            instanceTransform.SetParent(parent);
            instance.gameObject.SetActive(true);

            return instance;
        }

        private void GrowPool(int n)
        {
            for (int i = 0; i < n; i++)
            {
                T instantiatedObj = Object.Instantiate(prefab, parentTransform);
                AddToPool(instantiatedObj);
            }
        }

        public void AddToPool(T instance)
        {
            instance.gameObject.SetActive(false);
            availableObjects.Enqueue(instance);
            instance.transform.SetParent(parentTransform);
        }
    }
}
