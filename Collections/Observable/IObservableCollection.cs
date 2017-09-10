using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities.Observable
{
    public interface IObservableCollection<T> : ICollection<T>
    {
        event Action<T> OnAfterItemAdd;
        event Action<T> OnAfterItemRemove;
    }   
}
