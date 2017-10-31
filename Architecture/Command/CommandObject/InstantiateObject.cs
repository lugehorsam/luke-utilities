namespace Command
{
    using System.Collections;

    using UnityEngine;
    
    using Utilities;

    [CreateAssetMenu]
    public sealed class InstantiateObject : CommandObject
    {
        [SerializeField] private Prefab _prefab;
        
        public override IEnumerator Run(GameObject gameObject)
        {
            _prefab.Instantiate(gameObject.transform);
            yield break;
        }
    }
}
