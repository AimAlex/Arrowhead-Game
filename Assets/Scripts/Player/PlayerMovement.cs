using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody;

    private Vector2 velocity;

    public float moveSpeed = 5f;
    
    // public static float jumpForce = 10f;
    public float jumpForce = 10f;

    public float dashDistance = 10f; 
    public Transform groundCheck;
	public LayerMask ground;

	public bool isGround, isDashing, isPreview, isMoved, isRunning;
	bool jumpPressed;
	int jumpCount, dashCount;

	public static bool collectDoubleJump, collectDash;
	public AudioSource audioSource, audioSource2;
	AudioClip jumpAudio, dashAudio, pickPowerAudio;
	// public Image power1;
    // public Image power2;
    // public Image power3;
	// public Image power4;
	private Image power1;
    private Image power2;
    private Image power3;
	private Image power4;
	private SideScrolling cameraScroll;
	[SerializeField] public Camera camera;

	public bool facingRight;
	public void PlayAudio(AudioClip audioclip)
	{
		audioSource.clip = audioclip;
		audioSource.Play();
	}
	public void PlayAudio2(AudioClip audioclip)
	{
		audioSource2.clip = audioclip;
		audioSource2.Play();
	}

	private void UpdatePowerShow(Collider2D col)
	{
		if (isMoved){
		PlayAudio2(pickPowerAudio);
		}
		if (power1.sprite == null)
		{
			power1.sprite = col.gameObject.GetComponent<SpriteRenderer>().sprite;
			power1.color = col.gameObject.GetComponent<SpriteRenderer>().color;
		}
		else if (power2.sprite == null)
		{
			power2.sprite = col.gameObject.GetComponent<SpriteRenderer>().sprite;
			power2.color = col.gameObject.GetComponent<SpriteRenderer>().color;

		}
		else if (power3.sprite == null)
		{
			power3.sprite = col.gameObject.GetComponent<SpriteRenderer>().sprite;
			power3.color = col.gameObject.GetComponent<SpriteRenderer>().color;
		}
		else if (power4.sprite == null)
		{
			power4.sprite = col.gameObject.GetComponent<SpriteRenderer>().sprite;
			power4.color = col.gameObject.GetComponent<SpriteRenderer>().color;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.CompareTag("Booster"))
		{
			if (col.name == "doubleJumpItem")
			{
				collectDoubleJump = true;
				// GameObject.Find("info").GetComponent<InfoShow>().showInfo("You got double jump!");
			}

			if (col.name == "dashItem")
			{
				collectDash = true;
			}
			
			UpdatePowerShow(col);
			Destroy(col.gameObject);
		}
	}

	private void Awake()
    {
	    collectDoubleJump = false;
	    collectDash = false;
		isMoved = false;
	    rigidbody = GetComponent<Rigidbody2D>();
		jumpForce = 10f;
		power1 = GameObject.Find("power1").GetComponent<Image>();
        power2 = GameObject.Find("power2").GetComponent<Image>();
        power3 = GameObject.Find("power3").GetComponent<Image>();
		power4 = GameObject.Find("power4").GetComponent<Image>();
		isPreview = false;
		// add audio
		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource2 = GameObject.Find("Grid").GetComponent<AudioSource>();
		audioSource.volume = 0.25f;
		audioSource2.volume = 0.25f;
		jumpAudio = Resources.Load<AudioClip>("music/jump");
		dashAudio = Resources.Load<AudioClip>("music/dash");
		pickPowerAudio = Resources.Load<AudioClip>("music/DM-CGS-15");
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraScroll = camera.GetComponent<SideScrolling>();
    }


	void Update() // Update is called once per frame
	{
		if (Input.GetKeyDown(KeyCode.O))
        {
            isPreview = true;
        }
		if (Input.GetKeyUp(KeyCode.O))
		{
			isPreview = false;
		} 
		if (!isPreview && Time.timeScale != 0 && !cameraScroll.camaraMove){
			isGround = Physics2D.OverlapCircle(groundCheck.position, 0.7f, ground);
			if (!isMoved){
				isMoved = true;
			}
			if (!isDashing)
			{
				GroundMovement();	
			}
			if (Input.GetButtonDown("Jump"))
			{
				StartCoroutine(Jump());
			}
			if (Input.GetKeyDown("l"))
			{
				StartCoroutine(Dash());
			}
		}
	}

	void GroundMovement()
	{
		float horizontalMove = Input.GetAxisRaw("Horizontal");

		rigidbody.velocity = new Vector2(horizontalMove * moveSpeed, rigidbody.velocity.y);

		if (horizontalMove != 0)
		{
			if (horizontalMove > 0)
			{
				transform.localScale = new Vector3(horizontalMove, 1, 1);
			}
			else
			{
				transform.localScale = new Vector3(-horizontalMove, 1, 1);
			}
			Animation.anim.SetBool("running",true);
			Animation.anim.SetBool("idle",false);
			isRunning = true;
		}else{
			Animation.anim.SetBool("running",false);
			Animation.anim.SetBool("idle",true);
			isRunning = false;
		}

		if (horizontalMove > 0 && facingRight)
		{
			Flip();
		}
		else if (horizontalMove < 0 && !facingRight)
		{
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		transform.Rotate(0f, 180f, 0f);
	}

	IEnumerator Jump()
	{   
		if (isGround)
		{
			jumpCount = 2;
			dashCount = 1;
			jumpPressed = true;
		} else if (collectDoubleJump && jumpCount > 0)
		{
			jumpPressed = true;
		}

		if (jumpPressed)
		{
			Animation.anim.SetBool("jump", true);
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
			jumpCount--;
			jumpPressed = false;
			// play audio
			audioSource.clip = jumpAudio;
			audioSource.Play();
			yield return new WaitForSeconds(0.375f);
			if (isRunning)
			{
				Animation.anim.SetBool("running", true);
				Animation.anim.SetBool("jump", false);
			}
			else
			{
				Animation.anim.SetBool("idle", true);
				Animation.anim.SetBool("jump", false);				
			}
		}

	}

	IEnumerator Dash()
	{
		if (collectDash && (dashCount > 0 || isGround) && !isDashing)
		{
			isDashing = true;
			Animation.anim.SetBool("dash", true);
			float d = dashDistance;
			audioSource.clip = dashAudio;
			audioSource.Play();
			if (rigidbody.velocity.x < 0)
			{
				d = -d;
			} else if (rigidbody.velocity.x == 0)
			{
				d = 0;
			}
			rigidbody.AddForce(new Vector2(d, 0f), ForceMode2D.Impulse);
			float gravity = rigidbody.gravityScale;
			rigidbody.gravityScale = 0;
			yield return new WaitForSeconds(0.3f);
			rigidbody.gravityScale = gravity;
			--dashCount;
			isDashing = false;
			Animation.anim.SetBool("running", true);
			Animation.anim.SetBool("dash", false);
		}
	}

}
