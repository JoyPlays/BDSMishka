using UnityEngine;
using System.Collections;

public enum NpcState
{
	Idle, 
	Attack,
	Ded
}

public class NpcControler : MonoBehaviour
{
	public Animator animator;

	public raWaypointMover Waypoint;

	[Range(0,5)]
	public float MoveSpeed = 3;

	[Header("Atack parameters")]
	[Range(0,30)]
	public float RadarDistance = 10;
	[Range(0, 30)]
	public float AttackDistance = 3;
	[Range(0, 5)]
	public float AttackCooldown = 2;

	public string AttackAnim;

	internal NpcState State;



	private PlayerControler player;
	private float attacking;

	void Start()
	{
		player = FindObjectOfType<PlayerControler>();
	}

	void Update()
	{
		if (State == NpcState.Idle)
		{

			if (player.Distance(transform) <= RadarDistance)
			{
				State = NpcState.Attack;
				Waypoint.Stop();

				Debug.Log("I find player");
			}
		}

		if (State == NpcState.Attack)
		{
			animator.SetFloat("speed", 0);
			if (player.Distance(transform) > RadarDistance)
			{
				State = NpcState.Idle;
				Waypoint.Reset();
				Debug.Log("Lost enemy");
				return;
			}

			attacking -= Time.deltaTime;
			if (attacking <= 0)
			{
				if (player.Distance(transform) < AttackDistance)
				{
					animator.SetTrigger(AttackAnim);
					attacking = AttackCooldown;
					return;
				}
			}
			else
			{
				return;
			}

			if (player.Distance(transform) > 2)
			{
				Waypoint.StepTo(player.transform);
			}
		}
	}
}
