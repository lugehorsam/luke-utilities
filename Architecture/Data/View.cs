using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class View<T> : View {
        
        public event StateMachine<T>.StateChangedHandler OnDataChanged
        {
            add { stateMachine.OnStateChanged += value; }
            remove { stateMachine.OnStateChanged -= value; }
        }
       
         public T Data {
            set
            {
                stateMachine.State = value;
            }
            get {
                return stateMachine.State;
            }
        }

        private readonly StateMachine<T> stateMachine = new StateMachine<T>();
    
        protected virtual void HandleDatumChanged (T oldData, T newData) {}
        
        public View(Transform parent) : base(parent)
        {
            stateMachine.OnStateChanged += HandleDatumChanged;
        }

        public View()
        {
            stateMachine.OnStateChanged += HandleDatumChanged;
        }

        public override string ToString()
        {            
            return this.ToString("View", stateMachine.State != null ? stateMachine.State.ToString() : "");
        }

        public void Bind(StateMachine<T> stateMachine)
        {
            Data = stateMachine.State;
            stateMachine.OnStateChanged += (oldVal, newVal) => Data = newVal;
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

        protected GameObject _GameObject
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

        public View(Transform parent) : this()
        {
            Transform.SetParent(parent);
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
