using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    

    public static class ICollectionExt {

        public static void Observe<T>(this ICollection<T> thisCollection, IObservableCollection<T> observableCollection)
        {
            observableCollection.OnAfterItemAdd += thisCollection.Add;
            observableCollection.OnAfterItemRemove += (item) =>
            {
                thisCollection.Remove(item);
            };
        }
    
        public static void Bind<T>(this ICollection<T> thisCollection, IObservableCollection<T> observableCollection)
        {
            foreach (T item in observableCollection.Items)
            {
                thisCollection.Add(item);                
            }
            
            thisCollection.Observe(observableCollection);
        }}

}