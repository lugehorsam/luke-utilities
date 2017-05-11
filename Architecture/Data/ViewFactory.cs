using System;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class ViewFactory<T, K> : ReadOnlyObservableCollection<K>
        where K : View<T> {
        
        private readonly Func<T, K> _viewConstructor;
        protected readonly ObservableCollection<T> _data;
        
        public ViewFactory(ObservableCollection<T> collection, Func<T, K> viewConstructor) : base(new ObservableCollection<K>())
        {
            _viewConstructor = viewConstructor;
            _data = collection;
            collection.OnAfterItemAdd += HandleAfterDataAdd;
            collection.OnAfterItemRemove += HandleAfterDataRemove;

            foreach (var item in collection)
            {
                HandleAfterDataAdd(item);
            }
        }
        
        void HandleAfterDataAdd(T data)
        {
            var newView = _viewConstructor(data);
            newView.Data = data;
            _observableCollection.Add(newView);
            HandleAfterViewAdd(data, newView);
        }
        
        void HandleAfterDataRemove(T data)
        {
            var oldView = Items.First(view => view.Data.Equals(data));
            GameObject.Destroy(oldView.GameObject);
            _observableCollection.Remove(oldView);
            HandleAfterViewRemove(data, oldView);
        }
        
        protected virtual void HandleAfterViewAdd(T data, K view)
        {            
        }

        protected virtual void HandleAfterViewRemove(T data, K view)
        {            
        }
        
    }
}

