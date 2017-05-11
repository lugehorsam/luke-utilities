using System;
using System.Collections.Generic;

namespace Utilities
{
    

    public static class ICollectionExt {
        
                
        public static void Observe<T, K>(this ICollection<T> thisCollection, IObservableCollection<K> observableCollection, Func<K, T> transform)
        {
            observableCollection.OnAfterItemAdd += k => thisCollection.Add(transform(k));
            observableCollection.OnAfterItemRemove += k => thisCollection.Remove(transform(k));
        }

        public static void Observe<T>(this ICollection<T> thisCollection, IObservableCollection<T> observableCollection)
        {
            observableCollection.OnAfterItemAdd += thisCollection.Add;
            observableCollection.OnAfterItemRemove += item =>
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
        }
    }
    
    

}