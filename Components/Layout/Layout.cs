using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public abstract class Layout
{
    private readonly GameObject gameObject;

    public ReadOnlyCollection<ILayoutMember> LayoutMembers
    {
        get { return new ReadOnlyCollection<ILayoutMember>(layoutMembers); }
    }

    private readonly List<ILayoutMember> layoutMembers = new List<ILayoutMember>();
    
    public Layout()
    {
        gameObject = new GameObject();
        gameObject.name = "Layout";
    }

    public void AddLayoutMember(ILayoutMember layoutMember)
    {
        layoutMember.GameObject.transform.SetParent(gameObject.transform);
        DoLayout();
    }

    public void AddLayoutMembers(IList<ILayoutMember> layoutMembers)
    {
        foreach (var member in layoutMembers)
        {
            layoutMembers.Add(member);
        }
        DoLayout();
    }
    
    public void DoLayout ()
    {
        for (int i = 0; i < layoutMembers.Count; i++) 
        {
            ILayoutMember behavior = layoutMembers [i];
            behavior.GameObject.transform.SetSiblingIndex (i);
            behavior.OnLocalLayout(GetIdealLocalPosition(behavior));          
        }
    }
    
    protected abstract Vector2 GetIdealLocalPosition(ILayoutMember behavior);
}
