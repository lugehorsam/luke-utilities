﻿using UnityEngine;
using Utilities;

namespace Utilities
{
	public class ViewContainer<T> : View<ObservableCollection<T>> {

		public ViewContainer()
		{
			Data = new ObservableCollection<T>();
		}

		public ViewContainer(Transform parent) : base(parent)
		{
			Data = new ObservableCollection<T>();
		}

		protected sealed override void HandleDatumChanged(ObservableCollection<T> oldData, ObservableCollection<T> newData)
		{
			if (oldData != null)
			{
				oldData.OnAfterItemAdd -= HandleItemAdd;
				oldData.OnAfterItemRemove -= HandleItemRemove;
				foreach (var item in oldData)
				{
					HandleItemRemove(item);
				}
			}
            
			if (newData != null)
			{
				newData.OnAfterItemAdd += HandleItemAdd;
				newData.OnAfterItemRemove += HandleItemRemove;
				foreach (var item in newData)
				{
					HandleItemAdd(item);
				}
			}            
		}
		
		protected virtual void HandleItemAdd(T item)
		{
		}

		protected virtual void HandleItemRemove(T item)
		{
		}
	}	
}