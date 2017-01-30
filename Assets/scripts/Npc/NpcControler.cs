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
    [Range(0, 30)]
    public float AttackDamage = 10;

	public string AttackAnim;

	internal NpcState State;



	private Player player;
	private float attacking;

	void Start()
	{
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		if (State == NpcState.Idle)
		{

			if (player.Distance(transform) <= RadarDistance && The.Player.Health.IsAlive)
			{
				State = NpcState.Attack;
				Waypoint.Stop();

				Debug.Log("Enemy: I find player");
			}
		}

		if (State == NpcState.Attack)
		{
			animator.SetFloat("speed", 0);
			if (player.Distance(transform) > RadarDistance)
			{
				State = NpcState.Idle;
				Waypoint.Reset();
				Debug.Log("Enemy: Lost enemy");
				return;
			}

			attacking -= Time.deltaTime;
			if (attacking <= 0)
			{
				if (player.Distance(transform) < AttackDistance)
				{
				    if (The.Player.Health.IsAlive)
				    {
				        Debug.Log("Enemy: Attacking");
				        animator.SetTrigger(AttackAnim);
				        The.Player.Health.TakeDamage(AttackDamage);
				        attacking = AttackCooldown;
				    }
				    else
				    {
				        State = NpcState.Idle;
				    }
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
