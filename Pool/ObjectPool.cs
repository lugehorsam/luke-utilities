namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ObjectPool<T>
    {
        public event Action<T> OnRelease = obj => { };
        public event Action<T> OnPool = obj => { };

        private readonly Queue<T> pooledObjects = new Queue<T>();

        public ReadOnlyCollection<T> ReleasedObjects
        {
            get { return new ReadOnlyCollection<T>(releasedObjects); }
        }

        private readonly List<T> releasedObjects = new List<T>();

        private readonly bool allowResize;
        private readonly Func<T> objectFactory;
        private readonly int initialSize;

        public ObjectPool(Func<T> objectFactory, int initialSize, bool allowResize = true)
        {
            this.allowResize = allowResize;
            this.objectFactory = objectFactory;
            this.initialSize = initialSize;
            Init();
        }

        private void Init()
        {
            for (var i = 0; i < initialSize; i++)
            {
                T objectToPool = objectFactory();
                pooledObjects.Enqueue(objectToPool);
            }
        }

        public T Release()
        {
            if (allowResize && (pooledObjects.Count == 0))
            {
                pooledObjects.Enqueue(objectFactory());
            }
            try
            {
                T objectToRelease = pooledObjects.Dequeue();
                releasedObjects.Add(objectToRelease);
                HandleOnRelease(objectToRelease);
                OnRelease(objectToRelease);
                return objectToRelease;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Object pool of type " + typeof(T) + " is empty.");
            }
        }

        public void Pool(T objectToPool)
        {
            releasedObjects.Remove(objectToPool);
            pooledObjects.Enqueue(objectToPool);
            HandleOnPool(objectToPool);
            OnPool(objectToPool);
        }

        public void Pool(int numToPool = 1)
        {
            for (var i = 0; i < numToPool; i++)
            {
                Pool(releasedObjects.GetLast());
            }
        }

        protected virtual void HandleOnPool(T objectToPool) { }

        protected virtual void HandleOnRelease(T objectToRelease) { }

        public void SetNumReleased(int numReleased)
        {
            while (numReleased != releasedObjects.Count)
            {
                if (releasedObjects.Count > numReleased)
                {
                    Pool(releasedObjects.FirstOrDefault());
                }
                else
                {
                    Release();
                }
            }
        }

        public void TransferTo(ObjectPool<T> otherPool, T obj, int insertionIndex)
        {
            releasedObjects.Remove(obj);
            otherPool.releasedObjects.Insert(insertionIndex, obj);
        }
    }
}
