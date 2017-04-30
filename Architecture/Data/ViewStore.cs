using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class ViewStore<T, K> where K : View<T>, new()
    {
        private readonly IList<T> _data;
        private readonly IList<K> _views;

        public ViewStore(IList<T> data, IList<K> views)
        {
            _data = data;
            _views = views;
        }

        public void Add(T data)
        {
            _data.Add(data);
            var newView = new K();

            newView.Datum = data;
            _views.Add(newView);
        }

        public void Remove(T data)
        {
            _data.Remove(data);
            var viewToRemove = _views.First(view => view.Datum.Equals(data));
            GameObject.Destroy(viewToRemove.GameObject);
            _views.Remove(viewToRemove);
        }
    }
}