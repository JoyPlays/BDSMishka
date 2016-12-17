using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(raWaypointPath))]
public class raWaypointPathEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		raWaypointPath myScript = (raWaypointPath)target;

		EditorGUILayout.LabelField("Waypoints count", myScript.WaypointsCount.ToString());

		if (GUILayout.Button("Add waypoint"))
		{
			myScript.CreateWaypoint();
		}
	}

}

[CustomEditor(typeof(raWaypoint))]
public class raWaypointEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		raWaypoint myScript = (raWaypoint)target;
		EditorGUILayout.LabelField("Index", myScript.Index.ToString());

		if (GUILayout.Button("Add waypoint"))
		{
			myScript.CreateWaypoint();
		}
	}

}