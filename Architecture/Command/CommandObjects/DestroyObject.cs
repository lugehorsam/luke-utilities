using UnityEngine;

namespace Enact
{
    using System.Collections;

    [CreateAssetMenu]
    public class DestroyObject : CommandObject 
    {
        public override IEnumerator Run(GameObject gameObject)
        {
            Destroy(gameObject);
            yield break;
        }
    }
}
