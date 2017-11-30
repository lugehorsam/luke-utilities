namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    [Serializable] public class DirectedEdge<T> : IDirectedEdge<T>, ISerializationCallbackReceiver
    {
        private void UpdateNodeEdgeList(INode<T> element)
        {
            element.Edges.Add(this);
        }

        public bool Contains(T element)
        {
            return Start.Value.Equals(element) || End.Value.Equals(element);
        }

        public INode<T> OtherElement(T element)
        {
            return element.Equals(Start.Value) ? End : Start;
        }

        public INode<T> Start { get; private set; }

        public INode<T> End { get; private set; }

        [SerializeField] private T _start;
        [SerializeField] private T _end;

        public DirectedEdge(T start, T end)
        {
            Start = new Node<T>(start);
            End = new Node<T>(end);

            UpdateNodeEdgeList(Start);
            UpdateNodeEdgeList(End);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool DirectsTo(DirectedEdge<T> otherDirectedEdge)
        {
            return End.Equals(otherDirectedEdge.Start);
        }

        public IEnumerator<INode<T>> GetEnumerator()
        {
            yield return Start;
            yield return End;
        }

        public int CompareTo(DirectedEdge<T> other)
        {
            if (DirectsTo(other))
            {
                return -1;
            }

            if (other.DirectsTo(this))
            {
                return 1;
            }

            return 0;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return Start.Value;
            yield return End.Value;
        }

        public override string ToString()
        {
            return this.ToString("Start " + Start, "End " + End);
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            if (_start != null)
            {
                Start = new Node<T>(_start);
                UpdateNodeEdgeList(Start);
            }

            if (_end != null)
            {
                End = new Node<T>(_end);
                UpdateNodeEdgeList(End);
            }
        }
    }
}
