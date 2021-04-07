using UnityEngine;

public class RobotController : MonoBehaviour
{
	GameObject platform;
	GameObject player;
	SpriteRenderer spriteRenderer;
	Rigidbody2D rigidbody2d;
	Animator animator;

	bool patrolRight;
	bool isResting;
	bool isJumping;

	void Start()
	{
		platform = transform.parent.gameObject;
		player = GameObject.Find("Octopus");
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (isResting) return;

		if (OnTop())
		{
			Jump();
			return;
		}

		if (Random.value > 0.995)
		{
			Rest();
			return;
		}

		if (PlayerInRange())
		{
			ChasePlayer();
		}
		else Patrol();
	}

	bool PlayerInRange()
	{
		if (player.transform.position.x < transform.position.x + 20f && player.transform.position.x > transform.position.x - 20f)
		{
			if (player.transform.position.y < transform.position.y + 10f && player.transform.position.y > transform.position.y - 10f)
			{
				return true;
			}
		}
		return false;
	}

	bool InsideBounds(bool isRight)
	{
		var platformBounds = platform.GetComponent<EdgeCollider2D>().bounds;
		if (isRight)
		{
			if (platformBounds.max.x - 1f > transform.position.x) return true;
		}
		else
		{
			if (platformBounds.min.x + 1f < transform.position.x) return true;
		}

		return false;
	}
	
	bool OnTop()
	{
		if (player.transform.position.x > transform.position.x - 4f &&
			 player.transform.position.x < transform.position.x + 4f &&
			 player.transform.position.y > transform.position.y)
		{
			return true;
		}

		return false;
	}

	void Rest()
	{
		isResting = true;
		animator.SetBool("resting", true);
		Invoke("RestCompleted", 1.5f);
	}

	void RestCompleted()
	{
		isResting = false;
		animator.SetBool("resting", false);
	}

	void Jump()
	{
		if (isJumping) return;
		isJumping = true;
		rigidbody2d.AddForce(new Vector2(0, 20000));
		//animator.SetBool("resting", true);
		Invoke("JumpCompleted", 1.5f);
	}

	void JumpCompleted()
	{
		isJumping = false;
		//animator.SetBool("resting", false);
	}

	void Patrol()
	{
		Debug.Log("Patrol");
		{
			if (patrolRight && InsideBounds(true))
			{
				transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
				spriteRenderer.flipX = true;
			}
			else if (!patrolRight && InsideBounds(false))
			{
				transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
				spriteRenderer.flipX = false;
			}
			else
			{
				patrolRight = !patrolRight;
			}
		}
	}

	void ChasePlayer()
	{
		if (player.transform.position.x > transform.position.x && InsideBounds(true))
		{
			transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
			spriteRenderer.flipX = true;
		}
		else if(InsideBounds(false))
		{
			transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
			spriteRenderer.flipX = false;
		}
	}

	void AttackPlayer()
	{

	}
}
