using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities.Meshes;

namespace Utilities
{ 
    public sealed class UIResizer : UIBehaviour
    {
        public SquareMesh SquareMesh { get; set; }
        
        private RectTransform _RectTransform => _rectTransform ?? (_rectTransform = gameObject.GetComponent<RectTransform>());
        private BoxCollider _BoxCollider => _boxCollider ?? (_boxCollider = gameObject.GetComponent<BoxCollider>());
        private MeshFilter _MeshFilter => _meshFilter ?? (_meshFilter = gameObject.GetComponent<MeshFilter>());
    
        private BoxCollider _boxCollider;
        private RectTransform _rectTransform;
        private LayoutElement _layoutElement;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private Vector3 _padding;

        void Awake()
        {
            Resize();
        }
        
        protected override void OnRectTransformDimensionsChange()
        {
            Resize();
        }

        void Resize ()
        {
            float width = _RectTransform.rect.width;
            float height = _RectTransform.rect.height;

            if (_BoxCollider != null)
            {
                _BoxCollider.size = new Vector2(width, height);
            }

            if (SquareMesh != null)
            {
                if (_MeshFilter == null)
                {
                    Diagnostics.LogWarning("Square mesh assigned to UI Resizer without a mesh filter.");
                    return;
                }

                SquareMesh.Width = width;
                SquareMesh.Height = height;
                _MeshFilter.mesh = SquareMesh.ToUnityMesh();
            }
        }
    }
}
