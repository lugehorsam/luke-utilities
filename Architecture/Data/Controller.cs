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

        private GameObject _gameObject;

        public Transform Transform => _gameObject.GetComponent<Transform>();
        public RectTransform RectTransform => _gameObject.GetComponent<RectTransform>();
        
        protected virtual Prefab _Prefab { get; }
        
        public void Instantiate(Transform parent)
        {
            _gameObject = _Prefab == null ? new GameObject() : _Prefab.Instantiate(); 
            var binding = _gameObject.AddComponent<ControllerBinding>();
            binding.Controller = this;
            _gameObject.transform.SetParent(parent);
            OnInstantiated();
        }

        public void Destroy()
        {
            GameObject.Destroy(_gameObject);
        }

        public T AddComponent<T>() where T : Component
        {
            return _gameObject.AddComponent<T>();
        }

        public T GetComponent<T>() where T : Component
        {
            return _gameObject.GetComponent<T>();
        }

        protected virtual void OnInstantiated()
        {
            
        }
    }
}
