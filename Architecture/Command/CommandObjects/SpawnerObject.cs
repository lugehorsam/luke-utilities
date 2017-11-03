namespace Enact
{
    using System.Collections;

    using UnityEngine;
    
    using Utilities;

    [CreateAssetMenu]
    public sealed class SpawnerObject : CommandObject
    {
        [SerializeField, Tooltip("Pause in between spawns, in seconds.")] 
        private float _spawnInterval;
        
        [SerializeField] private Prefab _prefab;
        
        public override IEnumerator Run(GameObject gameObject)
        {
            while (true)
            {                
                _prefab.Instantiate(gameObject.transform);
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}
