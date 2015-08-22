using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PathManager))]
public class PathEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		//base.OnInspectorGUI();
		if (GUILayout.Button ("Générer Path")) {
			var cl = target as PathManager;
			cl.CheckPoints.Clear ();
			foreach (Transform child in cl.gameObject.transform) {
				cl.CheckPoints.Add (child.gameObject);
			}
			foreach (var item in cl.CheckPoints) {
				Debug.Log (item);
			}
		}
		if (GUILayout.Button ("Toggle Path Visibility")) {
			var cl = target as PathManager;
			cl.ToggleGizmos ();
		}
	}

}
