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
    }

    public class View
    {
        public virtual string GameObjectName => ToString();

        public GameObject GameObject
        {
            get;
        }

        public Transform Transform => GameObject.GetComponent<Transform>();
        public RectTransform RectTransform => GameObject.GetComponent<RectTransform>();

        public View()
        {
            GameObject = GetPrefab() ?? new GameObject();
            GameObject.name = GameObjectName;
            var binding = GameObject.AddComponent<ViewBinding>();
            binding.View = this;
        }

        protected virtual GameObject GetPrefab()
        {
            return null;
        }

    }
}
