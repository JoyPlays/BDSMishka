using UnityEngine;

public enum Directions
{
	Right,
	Left
}

public class PlayerControler : MonoBehaviour
{
	public static int Lives = 1;

	[Header("Main parameters")]
	[Range(10, 20)]
	public float speed = 15;

	[Header("Animator")]
	public Animator animator;
	public string animRun = "Run";

	[Header("Jump")]
	public LayerMask groundLayer;
	public Transform groundCheck;
	[Range(50, 500)]
	public float jumpSpeed = 200;

	private const float checkRadius = 0.2f;

	private Rigidbody rigidbodyPlayer;
	Directions direction = Directions.Right;

	void Start()
	{
		rigidbodyPlayer = GetComponent<Rigidbody>();
		if (!rigidbodyPlayer)
		{
			Debug.LogError("Player without rigidbody!");
		}
	}

	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		SetVelosity(speed * h);

		if (h < 0 && direction == Directions.Right)
		{
			Flip();
		}
		else if (h > 0 && direction == Directions.Left)
		{
			Flip();
		}

		if (IsGrounded() && Input.GetAxis("Jump") > 0.01f)
		{
			AnimSetTrigger("Jump");
			AddForce(jumpSpeed);
		}
	}

	void LateUpdate()
	{

	}

	void FixedUpdate()
	{
		//Debug.Log(rigidbodyPlayer.velocity.x);
		AnimSetFloat(animRun, Mathf.Abs(rigidbodyPlayer.velocity.x));

		//h = rigidbodyPlayer.velocity.y;
	}

	bool IsGrounded()
	{
		Collider[] coll = Physics.OverlapSphere(groundCheck.position, checkRadius, groundLayer);
		return coll.Length > 0;
	}

	void SetVelosity(float velocity)
	{
		if (!rigidbodyPlayer) return;
		rigidbodyPlayer.velocity = new Vector3(velocity, rigidbodyPlayer.velocity.y, 0);
	}

	void AddForce(float force)
	{
		if (!rigidbodyPlayer) return;	
		rigidbodyPlayer.AddForce(new Vector3(0,force, 0));
	}

	void AnimSetTrigger(string trigger)
	{
		if (!animator) return;

		animator.SetTrigger(trigger);
	}
	void AnimSetFloat(string triger, float value)
	{
		if (!animator) return;
		
		animator.SetFloat(triger, value);
	}

	void Flip()
	{
		direction = direction == Directions.Right ? Directions.Left : Directions.Right;
		transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (direction == Directions.Right ? 1 : -1), transform.localScale.y, transform.localScale.z);
	}

	public float Distance(Transform target)
	{
		return Distance(target.position);
	}

	public float Distance(Vector3 target)
	{
		return Vector3.Distance(transform.position, target);
	}
}
