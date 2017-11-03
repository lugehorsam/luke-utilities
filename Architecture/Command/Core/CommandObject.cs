namespace Enact
{
    using System.Collections;
    using UnityEngine;

    public class CommandObject : ScriptableObject
    {   
        public virtual IEnumerator Run(GameObject gameObject)
        {
            yield break;
        }
    }
}