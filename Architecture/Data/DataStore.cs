using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities
{
    public class DataStore<T, K>
        where K : View<T>, new()
        
    {
        public ReadOnlyCollection<T> Data
        {
            get{return new ReadOnlyCollection<T>(_data);}
        }
        
        private readonly IList<T> _data;
        
        public ReadOnlyCollection<K> Views
        {
            get{ return new ReadOnlyCollection<K>(_views);}
        }
        private readonly IList<K> _views;

        public DataStore(IList<T> data, IList<K> views = null)
        {
            _views = views ?? new List<K>();

            foreach (T datum in data)
            {
                if (_views.Any(view => view.HasData(datum)))
                {
                    continue;
                }
                
                K newView = new K();
                newView.SetData(datum);
                _views.Add(newView);
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

        public K GetView(T data)
        {
            return _views.FirstOrDefault(view => view.HasData(data));   
        }

        protected virtual void HandleDataAdded(T data, K view)
        {
            
        }

        protected virtual void HandleDataRemoved(T data, K view)
        {
            
        }
    }
}
