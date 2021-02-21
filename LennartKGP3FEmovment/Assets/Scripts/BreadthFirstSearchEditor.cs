using UnityEditor;
using UnityEngine;

namespace GP3._04_SearchAlgorithms.BFS
{
	[CustomEditor(typeof(SearchBase), true)]
	public class SearchBaseEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Perform Search"))
			{
				(target as SearchBase)?.Search();
			}
		}
	}
}