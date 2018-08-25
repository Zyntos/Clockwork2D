// Kevin Hagen
// 19.08.2018

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utility
{
	public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
	{
		private static string EditorPath => "Assets/Resources/" + ScriptableName + ".asset";
		private static string BuildPath => ScriptableName;

		protected static string ScriptableName => typeof(T).Name;

		private static T _instance;
		public static T Instance
		{
			get
			{
				if (!_instance)
					_instance = Resources.Load(BuildPath) as T;
#if UNITY_EDITOR
				if (!_instance)
					_instance = AutoCreateOnPlaymodeStarted();
#endif

				return _instance;
			}
		}

		private static T AutoCreateOnPlaymodeStarted()
		{
			T newInstance = ScriptableObject.CreateInstance<T>();
			newInstance.OnCreate();

			if (EditorApplication.isPlayingOrWillChangePlaymode)
				EditorApplication.delayCall += () => Save(newInstance);
			else
				Save(newInstance);

			return newInstance;
		}

		private static void Save(T scriptableObjectToSave)
		{
			string directory = Path.GetDirectoryName(EditorPath);
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			AssetDatabase.CreateAsset(scriptableObjectToSave, EditorPath);
			AssetDatabase.SaveAssets();
		}

		protected abstract void OnCreate();
	}
}
