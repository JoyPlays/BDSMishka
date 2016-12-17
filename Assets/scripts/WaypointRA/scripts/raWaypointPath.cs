using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class raWaypointPath : MonoBehaviour
{
	public Color Color = Color.black;
	public Animator animator;

	public bool Active = true;

	[HideInInspector]
	public raWaypoint[] WaypointList;

	public int WaypointsCount {
		get {
			if (WaypointList == null)
			{
				return 0;
			}
			return WaypointList.Length;
		}
	}

	void Start()
	{
		UpdateWaypoints();
  }

	void UpdateWaypoints()
	{
		WaypointList = GetComponentsInChildren<raWaypoint>();

		for (var i = 0; i < WaypointList.Length; i++)
		{
			WaypointList[i].Index = i;
		}
	}

	public void CreateWaypoint()
	{
		GameObject waypoint = new GameObject("Waypoint");
#if UNITY_EDITOR
		Selection.activeGameObject = waypoint;
#endif
		waypoint.transform.SetParent(transform);
		waypoint.AddComponent<raWaypoint>();

		if (WaypointList.Length == 0)
		{
			waypoint.transform.localPosition = Vector3.zero;
		}
		else
		{
			waypoint.transform.localPosition = WaypointList[WaypointList.Length - 1].transform.localPosition;
			waypoint.GetComponent<raWaypoint>().SpeedModifier = WaypointList[WaypointList.Length - 1].SpeedModifier;
		}
		waypoint.GetComponent<raWaypoint>().Color = Color;
		UpdateWaypoints();
	}

	public bool isWaypoint(int waypoint)
	{
		return !(waypoint < 0 || waypoint >= WaypointList.Length);
	}

	public Vector3 localPosition(int waypoint)
	{
		if (!isWaypoint(waypoint))
		{
			return Vector3.zero;
		}
		return WaypointList[waypoint].transform.localPosition;
	}

	public Vector3 position(int waypoint)
	{
		if (!isWaypoint(waypoint))
		{
			return Vector3.zero;
		}
		return WaypointList[waypoint].transform.position;
	}

	public raWaypoint GetWaypoint(int waypoint)
	{
		if (!isWaypoint(waypoint))
		{
			return null;
		}

		return WaypointList[waypoint];

	}

	public raWaypoint GetFirst()
	{
		if (WaypointList.Length < 1 )
		{
			return null;
		}

		return WaypointList[0];
	}
	public raWaypoint GetLast()
	{
		if (WaypointList.Length < 1)
		{
			return null;
		}

		return WaypointList[WaypointList.Length - 1];
	}
	public raWaypoint GetNext(raWaypoint waypoint, int direction = 1)
	{
		if (direction == 1 && waypoint.Index >= WaypointList.Length - 1)
		{
			return null;
		}
		if (direction == -1 && waypoint.Index == 0)
		{
			return null;
		}

		return WaypointList[waypoint.Index + direction];
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color;

		WaypointList = GetComponentsInChildren<raWaypoint>();
		if (WaypointList.Length > 1)
		{
			for (var i = 0; i < (WaypointList.Length - 1); i++)
			{
				WaypointList[i].Color = Color;
				Gizmos.DrawLine(WaypointList[i].gameObject.transform.position, WaypointList[i + 1].gameObject.transform.position);
			}
		}
	}

	public void SetAnim(string trigger)
	{
		if (!animator) return;
		if (string.IsNullOrEmpty(trigger)) return;

		animator.SetTrigger(trigger);
	}
}
