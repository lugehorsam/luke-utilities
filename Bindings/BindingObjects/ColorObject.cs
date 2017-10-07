using UnityEngine;

namespace Utilities.UI
{
    [CreateAssetMenu]
    public class ColorObject : ScriptableObject 
    {
        [SerializeField] private Color _color;
        public Color Color => _color;
    }
}
