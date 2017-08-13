using System;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public sealed class ViewFactory<T, K> : ReadOnlyObservableCollection<K>
        where K : View<T> {
        
        private readonly Func<T, K> _viewConstructor;
        private readonly Action<K> _viewDestructor;

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
        
        public ViewFactory(ObservableCollection<T> collection, Func<T, K> viewConstructor,  Action<K> viewDestructor) : base(new ObservableCollection<K>())
        {
            _viewConstructor = viewConstructor;
            _viewDestructor = viewDestructor;
            _data = collection;

            _data.OnAfterItemAdd += HandleAfterDataAdd;
            _data.OnAfterItemRemove += HandleAfterDataRemove;

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
        }
        
        void HandleAfterDataRemove(T data)
        {
            var oldView = this.First(view => view.Data.Equals(data));
            _observableCollection.Remove(oldView);
            _viewDestructor(oldView);
        }        
    }
}

