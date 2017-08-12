using System;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class ViewFactory<T, K> : ReadOnlyObservableCollection<K>
        where K : View<T> {
        
        private readonly Func<T, K> _viewConstructor;

        public ObservableCollection<T> Data
        {
            get { return _data; }
            set
            {
                if (_data != null)
                {
                    foreach (T datum in _data)
                        HandleAfterDataRemove(datum);
                }
                
                _data = value;
                
                foreach (var datum in _data) 
                    HandleAfterDataAdd(datum);
            }
        }
        
        protected ObservableCollection<T> _data;

        public ViewFactory(Func<T, K> viewConstructor) : base(new ObservableCollection<K>())
        {
            _viewConstructor = viewConstructor;
        }
        
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
            var oldView = this.First(view => view.Data.Equals(data));
            oldView.Destroy();
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

