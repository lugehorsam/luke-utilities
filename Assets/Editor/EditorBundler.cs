using System.Linq;
using Luke;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR

namespace Utilities.Assets
{
	using UnityEditor;

	/// <summary>
	/// Ensures all asset bundles are loaded at once in editor and that all instances of <see cref="Bundle"/> 
	/// in the project maintain their reference to their asset bundle across play and compilation phases.
	/// </summary>
	[InitializeOnLoad]
	public static class EditorBundler
	{
		private static Bundler<MainBundle> _bundler;
		
		static EditorBundler()
		{
			EditorApplication.update += OnEditorUpdate;
		}
		
		private static void OnEditorUpdate()
		{
			if (!EditorApplication.isPlaying)
			{
				_bundler = _bundler ?? new Bundler<MainBundle>();
				_bundler.AssignBundleToBehaviors();
				var currentScene = EditorSceneManager.GetActiveScene();
				var rootGameObjects = currentScene.GetRootGameObjects();
				var behaviors = rootGameObjects.Select(root => root.GetComponentInChildren<Behavior<MainBundle>>());
				behaviors.Concat(rootGameObjects.Select(root => root.GetComponent<Behavior<MainBundle>>()));
				behaviors = behaviors.Where(behavior => behavior != null);
				_bundler.AssignBundleToBehaviors(behaviors);
			}
		}
	}
}

#endif