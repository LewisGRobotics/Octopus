using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OctopusController : MonoBehaviour
{
	public LayerMask groundLayer;

	bool jump = false;
	bool canJump = false;
	bool hanging = false;
	bool dash = false;
	bool canDash = false;
	bool isFading = false;
	bool blueBellCanExecute = false;
	bool yellowBellCanExecute = false;
	bool greenBellCanExecute = false;
	bool redBellCanExecute = false;
	bool greenBellWasRang;

	float fade = 1f;

	List<GameObject> blueBellPlatformsInScene;
	List<GameObject> yellowBellPlatformsInScene;
	List<GameObject> redBellPlatformsInScene;

	Rigidbody2D rigidBody;
	Animator animator;
	SpriteRenderer spriteRenderer;
	BoxCollider2D boxCollider;
	Material material;

	// Start is called before the first frame update
	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		material = spriteRenderer.material;
		SetTimeScaleNormal();

		// Load bells according to scene
		string sceneName = SceneManager.GetActiveScene().name;

		if (sceneName.Contains("TowerArea") || sceneName.Contains("LightsOut") || sceneName == "LaserArea3") blueBellCanExecute = true;		
		if (sceneName.Contains("TowerArea") || sceneName.Contains("LaserArea") || sceneName == "LightsOut4") greenBellCanExecute = true;
		if (sceneName == "TowerArea3") greenBellCanExecute = true;
	}

	// Update is called once per frame
	void Update()
	{
		FindPlatforms();

		canJump = Physics2D.OverlapArea(new Vector2(transform.position.x - 1.8f, transform.position.y - 2.1f),
													new Vector2(transform.position.x + 1.8f, transform.position.y - 2.2f), groundLayer);

      // Exit
      if (Input.GetKey("escape"))
      {
			SceneManager.LoadScene("MainMenu");
		}

		// Hanging	
		if (boxCollider.IsTouchingLayers() && Input.GetButton("XboxLB"))
		{
			rigidBody.gravityScale = 0f;
			animator.SetBool("hanging", true);
			hanging = true;
			canJump = true;
		}

		if(Input.GetButtonUp("XboxLB") || !boxCollider.IsTouchingLayers())
		{
			animator.SetBool("hanging", false);
			hanging = false;
			rigidBody.gravityScale = 40f;
		}

		// Jump
		if ((Input.GetKeyDown("up") || (!Input.GetButton("XboxRB") && Input.GetButtonDown("XboxA"))) && canJump)
		{
			jump = true;
			if (hanging)
			{
				animator.SetBool("hanging", false);
				hanging = false;
				rigidBody.gravityScale = 40f;
			}
		}

		// Dash
		if (!Input.GetButton("XboxRB") && Input.GetButtonDown("XboxX") && canDash)
		{
			dash = true;
		}

		// Fading
		if (isFading)
		{
			rigidBody.gravityScale = 0f;
			rigidBody.mass = 10000f;
			fade -= Time.deltaTime;
			if(fade <= 0)
			{
				isFading = false;
				fade = 0f;
			}

			material.SetFloat("_Fade", fade);
		}

		#region Bells

		// Blue bell
		if ((Input.GetKeyDown("z") || (Input.GetButton("XboxRB") && Input.GetButtonDown("XboxX"))) && blueBellCanExecute)
		{
			var gate = GameObject.Find("Gate");
			gate?.BroadcastMessage("BellRang", "Blue");

			foreach (var platform in blueBellPlatformsInScene)
			{
				if (CalculateProximity(40f, 40f, platform)) platform.GetComponent<Animator>().SetBool("BlueBellRang", true);
			}

			Color blue = new Color(25f / 255, 122f / 255, 255f / 255);
			PlayBell(blue);
		}

		// Red bell
		if (Input.GetKeyDown("c") || (Input.GetButton("XboxRB") && Input.GetButtonDown("XboxB")) && redBellCanExecute)
		{
			var gate = GameObject.Find("Gate");
			gate?.BroadcastMessage("BellRang", "Red");

			foreach (var platform in redBellPlatformsInScene)
			{
				if (CalculateProximity(40f, 40f, platform)) platform.GetComponent<Animator>().SetBool("RedBellRang", true);
			}

			Color red = new Color(255f / 255, 50f / 255, 25f / 255);
			PlayBell(red);
			Invoke("RedBellRangOff", 0.5f);
		}

		// Yellow bell
		if (Input.GetKeyDown("x") || (Input.GetButton("XboxRB") && Input.GetButtonDown("XboxY")) && yellowBellCanExecute)
		{
			var gate = GameObject.Find("Gate");
			gate?.BroadcastMessage("BellRang", "Yellow");

			foreach (var platform in yellowBellPlatformsInScene)
			{
				if (CalculateProximity(40f, 40f, platform)) platform.GetComponent<Animator>().SetBool("YellowBellRang", true);
			}

			Color yellow = new Color(255f / 255, 185f / 255, 51f / 255);
			PlayBell(yellow);
			Invoke("YellowBellRangOff", 0.5f);
		}

		// Green bell
		if (Input.GetKeyDown("v") || (Input.GetButton("XboxRB") && Input.GetButtonDown("XboxA")) && greenBellCanExecute)
		{
			var gate = GameObject.Find("Gate");
			gate?.BroadcastMessage("BellRang", "Green");

			Time.timeScale = 0.5f;
			Color green = new Color(10f / 104, 185f / 255, 105f / 255);
			PlayBell(green);
			greenBellWasRang = true;
			Invoke("SetTimeScaleNormal", 2f);
			greenBellCanExecute = false;
		}

		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		float horizontalMove = Input.GetAxis("Horizontal");

      // Horizontal movement
      if (!hanging)
      {
			rigidBody.transform.position += new Vector3(0.6f * horizontalMove, 0, 0);
			rigidBody.AddForce(new Vector2(500 * horizontalMove, 0));
			if (horizontalMove > 0) spriteRenderer.flipX = false;
			if (horizontalMove < 0) spriteRenderer.flipX = true;
		}

		if (horizontalMove == 0) animator.SetBool("moving", false);
		else animator.SetBool("moving", true);

		// Jump 
		if (jump && canJump)
		{
			var ps = GameObject.Find("JumpParticleSystem").GetComponent<ParticleSystem>();
			ps.Play();
			rigidBody.AddForce(new Vector2(0, 18000));
			canJump = false;
			jump = false;
		}

		// Dash 
		if (dash && canDash)
		{
			var verticalAxis = Input.GetAxis("Vertical");
			var horizontalAxis = Input.GetAxis("Horizontal");

			var ps = GameObject.Find("DashParticleSystem").GetComponent<ParticleSystem>();
			ps.transform.eulerAngles = new Vector3(ps.transform.eulerAngles.x, ps.transform.eulerAngles.y, CalculateAngle(-horizontalAxis, verticalAxis));
			ps.Play();
			rigidBody.gravityScale = 0f;
			animator.SetBool("dashing", true);
			rigidBody.AddForce(new Vector2(24000 * horizontalAxis, 24000 * verticalAxis));
			Invoke("GravityReset", 0.2f);
			canDash = false;
			dash = false;
		}

		#endregion
	}

	private void FindPlatforms()
	{
		if (blueBellPlatformsInScene == null)
		{
			blueBellPlatformsInScene = new List<GameObject>(FindPlatformsInScene("BlueBellPlatform"));
		}
		if (yellowBellPlatformsInScene == null)
		{
			yellowBellPlatformsInScene = new List<GameObject>(FindPlatformsInScene("YellowBellPlatform"));
		}
		if (redBellPlatformsInScene == null)
		{
			redBellPlatformsInScene = new List<GameObject>(FindPlatformsInScene("RedBellPlatform"));
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0, 0.5f);
		Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 2.15f), new Vector2(3.6f, 0.1f));
	}

	#region Invoke

	// Plays the bell animation with the given color.
	private void PlayBell(Color bellColor)
	{
		var ps = GameObject.Find("BellParticleSystem").GetComponent<ParticleSystem>();
		var psColorModule = ps.colorOverLifetime;
		Gradient colorGradient = new Gradient();

		colorGradient.SetKeys(
				  new GradientColorKey[] { new GradientColorKey(bellColor, 0.0f), new GradientColorKey(bellColor, 0.5f), new GradientColorKey(bellColor, 1.0f) },
				  new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) }
			 );
		psColorModule.color = colorGradient;
		ps.Stop();
		ps.Play();
	}

	private void RedBellRangOff()
	{
		var bp = GameObject.Find("RedBellPlatform");
		bp.GetComponent<Animator>().SetBool("RedBellRang", false);
	}

	private void YellowBellRangOff()
	{
		//var bp = GameObject.Find("YellowBellPlatform");
		//bp.GetComponent<Animator>().SetBool("YellowBellRang", false);
	}

	private void SetTimeScaleNormal()
	{
		Time.timeScale = 1f;
		if (greenBellWasRang) Invoke("CoolDownReset", 5f);
	}

	private void CoolDownReset()
	{
		greenBellCanExecute = true;
		greenBellWasRang = false;
	}

	private void GravityReset()
	{
		animator.SetBool("dashing", false);
		rigidBody.gravityScale = 40f;
	}

	#endregion

	#region Eventhandlers

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name.Contains("JumpToken"))
		{
			rigidBody.AddForce(new Vector2(0, 20000));
		}

		if (collision.gameObject.name.Contains("DashToken"))
		{
			canDash = true;
		}

		if (collision.gameObject.name.Contains("Portal"))
		{
			isFading = true;
		}

		if (collision.gameObject.name == "BlueBell")
		{
			blueBellCanExecute = true;
		}

		if (collision.gameObject.name == "RedBell")
		{
			redBellCanExecute = true;
		}

		if (collision.gameObject.name == "YellowBell")
		{
			yellowBellCanExecute = true;
		}

		if (collision.gameObject.name == "GreenBell")
		{
			greenBellCanExecute = true;
		}

		if (collision.gameObject.name.Contains("RedBellPlatform"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	#endregion

	private List<GameObject> FindPlatformsInScene(string platformNames)
	{
		List<GameObject> foundPlatforms = new List<GameObject>();

		for (int i = 0; i < 10; i++)
		{
			var platform = GameObject.Find(platformNames + i);
			if (platform != null)
			{
				foundPlatforms.Add(platform);
			}
			
		}

		return foundPlatforms;
	}

	// Calculates if the given game object is in range of a certain x and y distance.
	private bool CalculateProximity(float xDist, float yDist, GameObject gameObject)
	{
		if (gameObject.transform.position.x < transform.position.x + xDist && gameObject.transform.position.x > transform.position.x - xDist)
		{
			if (gameObject.transform.position.y < transform.position.y + yDist && gameObject.transform.position.y > transform.position.y - yDist)
			{
				return true;
			}
		}
		return false;
	}

	private float CalculateAngle(float xAxis, float yAxis)
	{
		float angle = 0f;
		if (xAxis >= 0)
		{
			if (yAxis >= 0)
			{
				angle = 90f;
				angle += (Math.Abs(xAxis) * 90);
			}
			else
			{
				angle = 180f;
				angle += (Math.Abs(yAxis) * 90);
			}
		}
		else
		{
			if (yAxis >= 0)
			{
				angle = 0f;
				angle += (Math.Abs(yAxis) * 90);
			}
			else
			{
				angle = 270f;
				angle += (Math.Abs(xAxis) * 90);
			}
		}
		return angle;
	}
}
