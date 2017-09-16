using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class View<T> : View {
        
        public event Reactive<T>.PropertyChangedHandler OnDataChanged
        {
            add { _reactive.OnPropertyChanged += value; }
            remove { _reactive.OnPropertyChanged -= value; }
        }
       
         public T Data {
            set
            {
                _reactive.Value = value;
            }
            get {
                return _reactive.Value;
            }
        }

        private readonly Reactive<T> _reactive = new Reactive<T>();
    
        protected virtual void HandleDatumChanged (T oldData, T newData) {}
        
        public View()
        {
            _reactive.OnPropertyChanged += HandleDatumChanged;
        }
        
        public override string ToString()
        {            
            return this.ToString("View", _reactive.Value != null ? _reactive.Value.ToString() : "");
        }

        public void Bind(Reactive<T> reactive)
        {
            Data = reactive.Value;
            reactive.OnPropertyChanged += (oldVal, newVal) => Data = newVal;
        }

        public static IEnumerable<View<T>> FromData(IEnumerable<T> data)
        {
            var views = new List<View<T>>();
            
            foreach (var datum in data)
            {
                var newView = new View<T>();
                newView.Data = datum;
                views.Add(newView);   
            }
            
            return views;
        }
    }

    public class View
    {
        public virtual string GameObjectName => ToString();

        private GameObject _GameObject
        {
            get;
        }

        public Transform Transform => _GameObject.GetComponent<Transform>();
        public RectTransform RectTransform => _GameObject.GetComponent<RectTransform>();

        public View()
        {
            _GameObject = GetPrefab() ?? new GameObject();
            var binding = _GameObject.AddComponent<ViewBinding>();
            binding.View = this;
        }

        protected virtual GameObject GetPrefab()
        {
            return null;
        }

        public void Destroy()
        {
            GameObject.Destroy(_GameObject);
        }

        public T AddComponent<T>() where T : Component
        {
            return _GameObject.AddComponent<T>();
        }
    }
}
