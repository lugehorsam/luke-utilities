using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Controller<T> : Controller 
    {    
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
                
        public Controller()
        {
            _reactive.OnPropertyChanged += HandleDatumChanged;
        }
        
        public override string ToString()
        {            
            return this.ToString(typeof(Controller), _reactive.Value != null ? _reactive.Value.ToString() : "");
        }

        public void Bind(Reactive<T> reactive)
        {
            Data = reactive.Value;
            reactive.OnPropertyChanged += (oldVal, newVal) => Data = newVal;
        }

        public static IEnumerable<Controller<T>> FromData(IEnumerable<T> data)
        {
            var views = new List<Controller<T>>();
            
            foreach (var datum in data)
            {
                var newView = new Controller<T>();
                newView.Data = datum;
                views.Add(newView);   
            }
            
            return views;
        }

        protected virtual void HandleDatumChanged (T oldData, T newData) {}
    }

    public class Controller
    {
        public virtual string GameObjectName => ToString();

        private GameObject _GameObject
        {
            get;
        }

        public Transform Transform => _GameObject.GetComponent<Transform>();
        public RectTransform RectTransform => _GameObject.GetComponent<RectTransform>();

        public Controller(Prefab prefab = null)
        {
            _GameObject = prefab == null ? new GameObject() : prefab.Instantiate();
            var binding = _GameObject.AddComponent<ViewBinding>();
            binding.Controller = this;
        }

        public void Destroy()
        {
            GameObject.Destroy(_GameObject);
        }

        public T AddComponent<T>() where T : Component
        {
            return _GameObject.AddComponent<T>();
        }

        public T GetComponent<T>() where T : Component
        {
            return _GameObject.GetComponent<T>();
        }
    }
}
