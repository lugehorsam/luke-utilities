#if UNITY_EDITOR

namespace Utilities
{
	using Mesh;
	using NUnit.Framework;
	using UnityEngine;
	using Utilities;

	public class FlexibleLayoutTests
	{
		[Test]
		public void TestOverflow()
		{
			GameObject parent = new GameObject();

			CreateGameObjects(parent);

			var overflowChecker = new LayoutOverflowChecker(parent.transform, 5);
			Assert.That(overflowChecker.IsOverflowX());

			overflowChecker = new LayoutOverflowChecker(parent.transform, 3);

			Assert.That(overflowChecker.IsOverflowX());

			overflowChecker = new LayoutOverflowChecker(parent.transform, 15);
			Assert.That(!overflowChecker.IsOverflowX());
		}

		private void CreateGameObjects(GameObject parent)
		{
			for (int i = 0; i < 6; i++)
			{
				var child = new GameObject();
				child.transform.SetParent(parent.transform);
				child.transform.position = new Vector3(i, 0, 0);
				var renderer = child.AddComponent<MeshRenderer>();
				var filter = child.AddComponent<MeshFilter>();
				var meshData = new TriangleMesh(meshRadius: 3);
				filter.mesh = meshData.ToUnityMesh();
			}
		}
	}
}

#endif
