using UnityEngine;

namespace Utilities
{ 
    public static class ComponentExt {

        public static Controller GetView(this Component thisComponent)
        {
            return thisComponent.GetComponent<ControllerBinding>().Controller;
        }
    }  
}
