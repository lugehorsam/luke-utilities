using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class LineSquare : View
    {
        private readonly LineRenderer _lineRenderer;

        public float LineWidth
        {
            get { return _lineRenderer.GetWidth(); }
            set { _lineRenderer.SetWidth(value); }
        }

        protected override string Name { get { return "Line Square"; }}

        public LineSquare(Vector3 vector1, Vector3 vector2, Vector3 vector3, Vector3 vector4)
        {
            var initialVectors = new Vector3[]
            {
                vector1, vector2, vector3, vector4
            };
                        
            MathUtils.SortInCycle(initialVectors, CycleDirection.Clockwise);

            var sortedVectors = initialVectors.ToList();
            sortedVectors.Add(initialVectors[0]);
            
            Diagnostics.Log("sorted vectors " + sortedVectors.ToFormattedString());
            
            _lineRenderer = GameObject.AddComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.positionCount = 5;
            
            _lineRenderer.SetPositions
            (
                sortedVectors.ToArray()
            );
        }        
    }
}
