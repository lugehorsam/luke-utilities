using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities
{

    public abstract class Layout<T> : View where T : ILayoutMember
    {
        public ReadOnlyCollection<T> LayoutMembers
        {
            get { return new ReadOnlyCollection<T> (layoutMembers); }
        }

        private readonly List<T> layoutMembers;

        public Layout(List<T> members)
        {
            layoutMembers = members;
        }
                
        public void DoLayout()
        {
            for (int i = 0; i < layoutMembers.Count; i++)
            {
                T behavior = layoutMembers[i];

                if (behavior == null)
                    continue;
                
                behavior.GameObject.transform.SetParent(GameObject.transform);
                behavior.GameObject.transform.SetSiblingIndex(i);
                behavior.OnLocalLayout(GetIdealLocalPosition(behavior));
            }
        }

        public void AddLayoutMember(T member)
        {
            layoutMembers.Add(member);
            DoLayout();
        }

        protected abstract Vector2 GetIdealLocalPosition(T element);
    }   
}
