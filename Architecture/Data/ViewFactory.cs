﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities
{
    public class ViewFactory<T, K>
        where K : View<T>

    {
        public event Action<T, K> OnAfterViewAdd = delegate { };
        public event Action<T, K> OnAfterViewRemove = delegate { };
           
        public ReadOnlyCollection<K> Views
        {
            get{ return new ReadOnlyCollection<K>(_views); }
        }
        
        private readonly IList<K> _views = new List<K>();

        private readonly Func<T, K> _viewConstructor;

        void Add(T data)
        {
            var newView = _viewConstructor(data);
            newView.SetData(data);
            _views.Add(newView);
            HandleAfterDataAdd(data, newView);
            OnAfterViewAdd(data, newView);
        }
        
        void Remove(T data)
        {
            var oldView = _views.First(view => view.HasData(data));
            GameObject.Destroy(oldView.GameObject);
            _views.Remove(oldView);
            HandleDataRemoved(data, oldView);
            OnAfterViewRemove(data, oldView);
        }
        
        protected virtual void HandleAfterDataAdd(T data, K view)
        {
            
        }

        protected virtual void HandleDataRemoved(T data, K view)
        {
            
        }

        public ViewFactory(ObservableCollection<T> collection, Func<T, K> viewConstructor)
        {
            _viewConstructor = viewConstructor;
            collection.OnAfterItemAdd += Add;
            collection.OnAfterItemRemove += Remove;

            foreach (var item in collection)
            {
                Add(item);
            }
        }
    }
}

