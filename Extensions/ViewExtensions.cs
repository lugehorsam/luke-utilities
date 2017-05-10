using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class ViewExtensions {

        public static K GetViewWithData<T, K>(this IEnumerable<K> views, T data) where K : View<T>
        {
            return views.FirstOrDefault(view => view.Data.Equals(data));
        }
        
        public static T GetDataWithView<T, K>(this IEnumerable<T> data, K view) where K : View<T>
        {
            return data.FirstOrDefault(datum => view.Data.Equals(datum));
        }

        public static void Bind<T, K>(IEnumerable<T> data, IEnumerable<T> view) where K : View<T>
        {
              
        }
    }
}