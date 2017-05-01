using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class DataStore<T, K, TCollection, KCollection>
        where K : View<T>, new()
        where TCollection : IList<T>
        where KCollection : IList<K>
        
    {
        private readonly TCollection _data;
        private readonly KCollection _views;

        public DataStore(TCollection data, KCollection views)
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

        public void Add(T data)
        {
            _data.Add(data);
            var newView = new K();

            newView.SetData(data);
            _views.Add(newView);
        }

        public void Remove(T data)
        {
            _data.Remove(data);
            var viewToRemove = _views.First(view => view.HasData(data));
            GameObject.Destroy(viewToRemove.GameObject);
            _views.Remove(viewToRemove);
        }

        public T GetData(K view)
        {
            return _data.FirstOrDefault(view.HasData);
        }
    }
}
