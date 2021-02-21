using UnityEditor;
using UnityEngine;

public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
{
	private static T _instance;
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = AssetDatabase.LoadAssetAtPath<T>($"Assets/ScriptableObjects/{typeof(T).Name}.asset");
			}

			return _instance;
		}
	}
}
