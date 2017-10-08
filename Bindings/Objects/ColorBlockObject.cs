namespace Utilities
{
    using UnityEngine;
    using UnityEngine.UI;
    
    [CreateAssetMenu]
    public class ColorBlockObject : ScriptableObject
    {
        [SerializeField] private ColorBlock _colorBlock;
        public ColorBlock ColorBlock => _colorBlock;
    }   
}
