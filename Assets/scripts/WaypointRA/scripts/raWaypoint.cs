using UnityEngine;


public class raWaypoint : MonoBehaviour {

	public float SpeedModifier = 1;

	[HideInInspector]
	public int Index;

	[HideInInspector]
	public Color Color = Color.black;

	void  OnDrawGizmos()
	{
		Gizmos.color = Color;
		Gizmos.DrawSphere(transform.position + new Vector3(0,0.3f,0), 0.3f);
	}

	public void CreateWaypoint()
	{

		if (!transform.parent)
		{
			return;
		}
		raWaypointPath path = transform.parent.gameObject.GetComponent<raWaypointPath>();
		if (path)
		{
			path.CreateWaypoint();
		}
	}
}

