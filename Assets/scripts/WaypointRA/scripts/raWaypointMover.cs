using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum raWaypointPathKind
{
	SIMPLE,
	LOOP,
	PINGPONG
}

public class raWaypointMover : MonoBehaviour
{

	public raWaypointPath Path;
	public raWaypointPathKind PathKind;

	[Header("Main propertys")]
	[Range(0, 100)]
	public float Speed = 20;
	public string WalkAnim;


	[Header("Rotate to target")]
	public bool LookAt = true;
	public Transform LookAtTransform;
	[Range(0, 3)]
	public float LookAtTime = 1;
	public string LookAtAnim;
	

	public UnityEvent OnWaypointEnd;
	public UnityEvent OnWaypointReach;

	raWaypoint destinationLast;
	int direction = 1;

	void Start()
	{
		raWaypoint wayPoint = Path.GetFirst();
		if (!wayPoint) return;
		transform.position = wayPoint.transform.position;
		wayPoint = Path.GetNext(wayPoint);
		if (!wayPoint) return;
		StartCoroutine(MoveToWaypoint(wayPoint));
	}

	public void Stop()
	{
		Path.Active = false;
	}

	public void StepTo(Transform target)
	{
		
		if (LookAt)
		{
			Transform look = LookAtTransform ? LookAtTransform : transform;
			Vector3 lookPos = target.position;
			lookPos.y = 0;
			look.LookAt(lookPos);


			//Quaternion rotation = Quaternion.LookRotation(lookPos - look.position);
			//look.rotation = rotation;

			//look.LookAt(target);
		}
		Path.animator.SetFloat("speed", 1);
		Vector3 t = target.position;
		t.y = 0;
		transform.position = Vector3.MoveTowards(transform.position, t, Time.deltaTime * Speed);
	}

	public void Reset()
	{
		Path.Active = true;
		if (destinationLast)
		{
			StartCoroutine(MoveToWaypoint(destinationLast));
		}

	}

	IEnumerator MoveToWaypoint(raWaypoint wayPoint)
	{
		destinationLast = wayPoint;

		Vector3 destination = wayPoint.transform.position;

		if (LookAt)
		{
			Transform look = LookAtTransform ? LookAtTransform : transform;
			//look.LookAt(destination);

			Quaternion rotation = Quaternion.LookRotation(destination - look.position);
			Quaternion startRot = look.rotation;

			Path.animator.SetFloat("speed", 0);
			Path.SetAnim(LookAtAnim);
			float time = LookAtTime;
			while (time > 0)
			{
				time -= Time.deltaTime;
				look.rotation = Quaternion.Lerp(startRot, rotation, 1-time/ LookAtTime);
				yield return null;
			}

		}

		Path.SetAnim(WalkAnim);
		Path.animator.SetFloat("speed",1);
		while (Vector3.Distance(transform.position, destination) > 0.01f)
		{
			transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime*Speed * wayPoint.SpeedModifier);

			if (!Path.Active) yield break;

			yield return null;
		}

		transform.position = destination;
		OnWaypointReach.Invoke();

		raWaypoint next = Path.GetNext(wayPoint, direction);
		if (!next)
		{
			//End of waypoints
			if (PathKind == raWaypointPathKind.SIMPLE)
			{
				yield break;
			}

			if (PathKind == raWaypointPathKind.LOOP)
			{
				next = Path.GetFirst();
			}

			if (PathKind == raWaypointPathKind.PINGPONG)
			{
				next = wayPoint;
				direction *= -1;
			}
		}

		if (!next)
		{
			yield break;
		}

		StartCoroutine(MoveToWaypoint(next));
	}

}
