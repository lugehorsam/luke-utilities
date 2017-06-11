using UnityEngine;

namespace Utilities
{
    public abstract class Layout<T> : View<ObservableList<T>> where T : ILayoutMember
    {
        public void DoLayout()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                T behavior = Data[i];

                if (behavior == null)
                    continue;

                behavior.GameObject.transform.SetParent(GameObject.transform);
                behavior.GameObject.transform.SetSiblingIndex(i);
                behavior.OnLocalLayout(GetIdealLocalPosition(behavior));
            }
        }

        protected override void HandleDatumChanged(ObservableList<T> oldData, ObservableList<T> newData)
        {           
            if (oldData != null)
            {
                oldData.OnAdd -= HandleOnAdd;
                oldData.OnRemove -= HandleOnRemove;
            }
            
            if (newData != null)
            {
                newData.OnAdd += HandleOnAdd;
                newData.OnRemove += HandleOnRemove;
            }
            
            DoLayout();
        }

        void HandleOnAdd(T datum)
        {
            DoLayout();
        }
        
        void HandleOnRemove(T datum, int index)
        {
            DoLayout();
        }

        protected abstract Vector2 GetIdealLocalPosition(T element);
    }      
}
