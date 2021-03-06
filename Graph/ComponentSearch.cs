﻿namespace Utilities
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	public class ComponentSearch<T> : IEnumerator<T> where T : class
	{
		private readonly Graph<T> _graph;
		private readonly HashSet<T> _visitedNodes = new HashSet<T>();
		private DepthFirstSearch<T> _currentDFS = default(DepthFirstSearch<T>);

		public ComponentSearch(Graph<T> graph)
		{
			_graph = graph;

			CreateCurrentComponent();
			AssignNewCurrent();
			AddCurrentToComponent();
		}

		public List<List<T>> AllComponents { get; } = new List<List<T>>();

		public T Current { get; private set; }

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public bool MoveNext()
		{
			AssignNewCurrent();
			return Current != null;
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		private void CreateCurrentComponent()
		{
			if (Current == null)
			{
				return;
			}

			AllComponents.Add(new List<T>());
		}

		private void AddCurrentToComponent()
		{
			if (Current == null)
			{
				return;
			}

			AllComponents[AllComponents.Count - 1].Add(Current);
		}

		private void AssignNewCurrent()
		{
			if (CanAssignCurrentFromDFS())
			{
				Current = _currentDFS.Current;
			}
			else if (CanAssignCurrentFromNewStartNode())
			{
				Current = GetUnsearchedStartNodes().First();
			}
			else
			{
				Current = null;
			}
		}

		private bool CanAssignCurrentFromDFS()
		{
			return _currentDFS.MoveNext();
		}

		private bool CanAssignCurrentFromNewStartNode()
		{
			return GetUnsearchedStartNodes().Any();
		}

		private IEnumerable<T> GetUnsearchedStartNodes()
		{
			return _graph.Nodes.Except(_visitedNodes);
		}
	}
}
