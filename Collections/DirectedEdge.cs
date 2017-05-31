using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
	[Serializable]
	public class DirectedEdge<T>
	{
		public T Start 
		{
			get { return _start; }
		}
		
		[SerializeField]
		private T _start;

		public T End
		{
			get { return _end; }
		}

		[SerializeField]
		private T _end;
			
		public DirectedEdge(T start, T end)
		{
			_start = start;
			_end = end;
		}

		public bool Contains(T element)
		{
			return Start.Equals(element) || End.Equals(element);
		}

		public T OtherElement(T element)
		{
			return element.Equals(Start) ? End: Start; 
		}

		public static List<DirectedEdge<T>> GetEdgesWithElement(IEnumerable<DirectedEdge<T>> connections, T element)
		{
			var connectionsWithNode = new List<DirectedEdge<T>>();
		
			foreach (var connection in connections)
			{
				if (connection.Contains(element))
					connectionsWithNode.Add(connection);
			}
			return connectionsWithNode;
		}
	
		public static List<DirectedEdge<T>> GetIncompleteEdges(IEnumerable<DirectedEdge<T>> connections)
		{
			var connectionsWithOutNode = new List<DirectedEdge<T>>();
		
			foreach (var connection in connections)
			{
				if (connection.Start == null || connection.End == null)
					connectionsWithOutNode.Add(connection);
			}

			return connectionsWithOutNode;
		}

		public override string ToString()
		{
			return this.ToString("Start " + Start, "end " + End);
		}
	}  
}
