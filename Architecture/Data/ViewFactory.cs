namespace Utilities
{
    using System;
    using System.Linq;
    using Observable;
    
    public sealed class ViewFactory<T, K> : ReadOnlyObservableCollection<K>
        where K : View<T> {
        
        private readonly Func<T, K> _viewConstructor;
        private readonly Action<K> _viewDestructor;

        public Observables<T> Data
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
        
        protected Observables<T> _data;
        
        public ViewFactory(Observables<T> observables, Func<T, K> viewConstructor,  Action<K> viewDestructor) : base(new Observables<K>())
        {
            _viewConstructor = viewConstructor;
            _viewDestructor = viewDestructor;
            _data = observables;

            _data.OnAfterItemAdd += HandleAfterDataAdd;
            _data.OnAfterItemRemove += HandleAfterDataRemove;

            foreach (var item in observables)
            {
                HandleAfterDataAdd(item);
            }
        }
        
        void HandleAfterDataAdd(T data)
        {
            var newView = _viewConstructor(data);
            newView.Data = data;
            Observables.Add(newView);
        }
        
        void HandleAfterDataRemove(T data)
        {
            var oldView = this.First(view => view.Data.Equals(data));
            Observables.Remove(oldView);
            _viewDestructor(oldView);
        }        
    }
}

