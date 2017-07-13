using UnityEngine;

namespace Utilities
{ 
    public static class ComponentExt {

        public static View GetView(this Component thisComponent)
        {
            return thisComponent.GetComponent<ViewBinding>().View;
        }
    }  
}
