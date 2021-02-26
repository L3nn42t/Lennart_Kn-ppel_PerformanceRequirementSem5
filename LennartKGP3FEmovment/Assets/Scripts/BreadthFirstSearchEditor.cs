using UnityEditor;
using UnityEngine;

namespace UEGP3PR
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