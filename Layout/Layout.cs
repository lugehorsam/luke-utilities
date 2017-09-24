namespace Utilities
{
    using UnityEngine;
    using Observable;
    
    public abstract class Layout<T, TList> : Controller<TList> where TList : Observables<T> where T : ILayoutMember
    {
        public T this[int i]
        {
            get { return Data[i]; }
            set { Data[i] = value; }
        }

        protected Layout(TList list)
        {
            Data = list;
        }
        
        public void DoLayout()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                T behavior = Data[i];

                if (behavior == null)
                    continue;
                                
                behavior.RectTransform.SetParent(behavior.RectTransform);
                behavior.RectTransform.SetSiblingIndex(i);
                behavior.OnLocalLayout(GetIdealLocalPosition(behavior));
            }
        }

        protected sealed override void HandleDatumChanged(TList oldData, TList newData)
        {           
            if (oldData != null)
            {
                oldData.OnAfterItemAdd -= HandleOnAdd;
                oldData.OnAfterItemRemove -= HandleOnRemove;
            }
            
            if (newData != null)
            {
                newData.OnAfterItemAdd += HandleOnAdd;
                newData.OnAfterItemRemove += HandleOnRemove;
            }
            
            DoLayout();
            HandleLayoutDatumChanged(oldData, newData);
        }

        protected virtual void HandleLayoutDatumChanged(TList oldData, TList newData)
        {
        }
        
        void HandleOnAdd(T datum)
        {
            datum.RectTransform.SetSiblingIndex(Data.IndexOf(datum));
            DoLayout();
        }
        
        void HandleOnRemove(T datum)
        {
            DoLayout();
        }

        protected abstract Vector2 GetIdealLocalPosition(T element);
    }      
}
