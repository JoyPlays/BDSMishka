using System;
using UnityEngine;
using System.Collections;

public class PlayerControlerPC : MonoBehaviour
{

	public CharacterController MyController;
	public Animator mainAnimator;

	[Header("Setup")]
	public float Speed = 3f;
	public float AerialSpeed = 2f;
	public float GravityStrenght = 5f;
	public float JumpSpeed = 10f;
	public bool ZAxisControl;

	private bool canJump;
	private float verticalVelocity;

	private Vector3 velocity;
	private Vector3 groundedVelocity;
	private Vector3 normal;
	private bool onWall = false;

	private Directions direction = Directions.Right;

	void Update()
	{

		Vector3 myVector = Vector3.zero;
		Vector3 input = Vector3.zero;

		input.x = Input.GetAxis("Horizontal");
		input.z = ZAxisControl ? Input.GetAxis("Vertical") : 0;

		input = Vector3.ClampMagnitude(input, 1);

		if (MyController.isGrounded)
		{
			myVector = input;
			myVector *= Speed;
			CheckDirection(input.x);
			SetAnim("Run",myVector.magnitude);
		}
		else
		{
			myVector = groundedVelocity + input * AerialSpeed;
		}


		myVector = Vector3.ClampMagnitude(myVector, Speed);
		myVector *= Time.deltaTime;


		verticalVelocity -= GravityStrenght * Time.deltaTime;
		if (Input.GetButtonDown("Jump"))
		{
			if (onWall)
			{
				Vector3 reflection = Vector3.Reflect(velocity, normal);
				Vector3 projected = Vector3.ProjectOnPlane(reflection, Vector3.up);
				groundedVelocity = projected.normalized * Speed + normal * AerialSpeed;
			}
			if (canJump)
			{
				verticalVelocity += JumpSpeed;
				SetAnim("Jump");
			}
		}

		myVector.y = verticalVelocity * Time.deltaTime;

		CollisionFlags flags = MyController.Move(myVector);
		velocity = myVector / Time.deltaTime;

		canJump = (flags & (CollisionFlags.Sides | CollisionFlags.Below)) != 0;
		onWall = (flags & CollisionFlags.Sides) != 0;
		if ((flags & CollisionFlags.Below) != 0)
		{
			verticalVelocity = -3f;
			groundedVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
			onWall = false;
		}

	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		normal = hit.normal;
	}

	void CheckDirection(float hSpeed)
	{
		if (Math.Abs(hSpeed) < Mathf.Epsilon) return;
		if (hSpeed > 0 && direction == Directions.Right) return;
		if (hSpeed < 0 && direction == Directions.Left) return;
		
		direction = hSpeed > 0 ? Directions.Right : Directions.Left;
		transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (direction == Directions.Right ? 1 : -1), transform.localScale.y, transform.localScale.z);

	}

	void SetAnim(string anim, float? value = null)
	{
		if (!mainAnimator) return;

		if (value == null)
		{
			mainAnimator.SetTrigger(anim);
			return;
		}

		mainAnimator.SetFloat(anim, (float)value);
	}
}
