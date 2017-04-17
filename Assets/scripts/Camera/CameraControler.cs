using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour
{

	public Transform target;

	[Range(0, 10)]
	public float smooth = 5;

	private Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void Update()
	{
		Vector3 targetPosition = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
	}
}
