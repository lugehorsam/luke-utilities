using Datum;
using UnityEngine;

public abstract class Layout
{
    public ObservableList<ILayoutMember> LayoutMembers
    {
        get { return layoutMembers; }
    }
    
    private readonly ObservableList<ILayoutMember> layoutMembers = new ObservableList<ILayoutMember>();
    private readonly GameObject gameObject;
    
    public Layout()
    {
        gameObject = new GameObject();
        layoutMembers.OnAdd += HandleLayoutMemberAdd;
        layoutMembers.OnRemove += (member, index) => DoLayout();
    }

    void HandleLayoutMemberAdd(ILayoutMember layoutMember)
    {
        layoutMember.GameObject.transform.SetParent(gameObject.transform);
        DoLayout();
    }
    
    
    public void DoLayout (int startIndex = 0)
    {
        for (int i = startIndex; i < layoutMembers.Count; i++) 
        {
            ILayoutMember behavior = layoutMembers [i];
            behavior.GameObject.transform.SetSiblingIndex (i);
            behavior.OnLocalLayout(GetIdealLocalPosition(behavior));          
        }
    }
    
    protected abstract Vector2 GetIdealLocalPosition(ILayoutMember behavior);
}
