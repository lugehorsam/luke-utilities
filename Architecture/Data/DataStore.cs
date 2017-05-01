using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class DataStore<T, K>
        where K : View<T>, new()
        
    {
        private readonly IList<T> _data;
        private readonly IList<K> _views;

        public DataStore(IList<T> data, IList<K> views)
        {
            _data = data;
            _views = views;

            foreach (T datum in data)
            {
                K view = new K();
                view.SetData(datum);
                views.Add(view);
            }
        }

        public void Add(T data, K view)
        {
            _data.Add(data);
            _views.Add(view);
            Debug.Log("adding view to list code " + _views.GetHashCode());
            HandleDataAdded(data, view);
        }

        public void Add(T data)
        {
            var newView = new K();
            newView.SetData(data);
            Add(data, newView);         
        }

        public void Remove(T data)
        {
            _data.Remove(data);
            var oldView = _views.First(view => view.HasData(data));
            GameObject.Destroy(oldView.GameObject);
            _views.Remove(oldView);
            HandleDataRemoved(data, oldView);
        }

        public T GetData(K view)
        {
            return _data.FirstOrDefault(view.HasData);
        }

        protected virtual void HandleDataAdded(T data, K view)
        {
            
        }

        protected virtual void HandleDataRemoved(T data, K view)
        {
            
        }
    }
}
