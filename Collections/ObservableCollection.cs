﻿using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities
{
    public class ObservableCollection<T> : Collection<T>, ISerializationCallbackReceiver
    {
        public event Action<T> OnAfterItemRemove = delegate { };
        public event Action<T> OnAfterItemAdd = delegate { };

        protected sealed override void ClearItems()
        {
            var oldItems = Items;
            base.ClearItems();
            foreach (var oldItem in oldItems)
            {
                OnAfterItemRemove(oldItem);
                HandleAfterItemRemove(oldItem);

            }
        }

        protected sealed override void SetItem(int index, T item)
        {
            var oldItem = Items[index];
            base.SetItem(index, item);
            HandleAfterItemRemove(oldItem);
            OnAfterItemRemove(oldItem);
            HandleAfterItemAdd(item);

            OnAfterItemAdd(item);

        }

        protected sealed override void RemoveItem(int index)
        {
            var oldItem = Items[index];
            base.RemoveItem(index);
            HandleAfterItemRemove(oldItem);
            OnAfterItemRemove(oldItem);
        }

        protected sealed override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            HandleAfterItemAdd(item);
            OnAfterItemAdd(item);
        }    

        protected virtual void HandleAfterItemAdd(T item)
        {
        
        }
    
        protected virtual void HandleAfterItemRemove(T item)
        {
        
        }

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {

        }
    }
   

}