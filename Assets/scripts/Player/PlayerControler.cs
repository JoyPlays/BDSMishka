using UnityEngine;

public enum Directions
{
	Right,
	Left
}

public class PlayerControler : MonoBehaviour
{
	[Header("Main parameters")]
	[Range(10, 20)]
	public float speed = 15;

	[Header("Animator")]
	public Animator animator;
	public string animRun = "Run";

	[Header("Jump")]
	public LayerMask groundLayer;
	public Transform groundCheck;
	[Range(50, 3000)]
	public float jumpSpeed = 200;

	private const float checkRadius = 0.2f;

	private Rigidbody rigidbodyPlayer;
	Directions direction = Directions.Right;

	// Use this for initialization
	void Start()
	{
		rigidbodyPlayer = GetComponent<Rigidbody>();
		if (!rigidbodyPlayer)
		{
			Debug.LogError("Plaey without rigidbody!");
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void FixedUpdate()
	{

		float h = Input.GetAxis("Horizontal");

		AnimSetFloat(animRun, Mathf.Abs(h));
		SetVelosity(speed  * h);

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
}
