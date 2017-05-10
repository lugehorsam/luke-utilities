using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities
{
    public interface IObservableCollection<T> : ICollection<T>
    {
        ReadOnlyCollection<T> Items { get; }
        event Action<T> OnAfterItemAdd;
        event Action<T> OnAfterItemRemove;
    }
    
}
