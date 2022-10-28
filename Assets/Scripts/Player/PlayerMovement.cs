using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private Vector2 velocity;

    public float moveSpeed = 5f;
    
    // public static float jumpForce = 10f;
    public float jumpForce = 10f;

    public float dashDistance = 10f; 
    public Transform groundCheck;
	public LayerMask ground;

	private bool isGround, isDashing, isPreview;
	bool jumpPressed;
	int jumpCount, dashCount;

	public static bool collectDoubleJump, collectDash;
	AudioSource audioSource;
	AudioClip jumpAudio;
	public Image power1;
    public Image power2;
    public Image power3;
	public Image power4;

	private void UpdatePowerShow(Collider2D col)
	{
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
	    rigidbody = GetComponent<Rigidbody2D>();
		jumpForce = 10f;
		power1 = GameObject.Find("power1").GetComponent<Image>();
        power2 = GameObject.Find("power2").GetComponent<Image>();
        power3 = GameObject.Find("power3").GetComponent<Image>();
		power4 = GameObject.Find("power4").GetComponent<Image>();
		isPreview = false;
		// add audio
		audioSource = gameObject.GetComponent<AudioSource>();
		jumpAudio = Resources.Load<AudioClip>("music/jump");
    }


	void Update() // Update is called once per frame
	{
		if (Input.GetKeyDown(KeyCode.P))
        {
            isPreview = true;
        }
		if (Input.GetKeyUp(KeyCode.P))
		{
			isPreview = false;
		} 
		if (!isPreview){
			isGround = Physics2D.OverlapCircle(groundCheck.position, 0.7f, ground);
			if (!isDashing)
			{
				GroundMovement();	
			}
			if (Input.GetButtonDown("Jump"))
			{
				Jump();
			}
			if (Input.GetKeyDown("w") || Input.GetKeyDown("w"))
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
			transform.localScale = new Vector3(horizontalMove, 1, 1);
		}
	}

	void Jump()
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
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
			jumpCount--;
			jumpPressed = false;
			// play audio
			audioSource.clip = jumpAudio;
			audioSource.Play();
		}

	}

	IEnumerator Dash()
	{
		if (collectDash && (dashCount > 0 || isGround) && !isDashing)
		{
			isDashing = true;
			float d = dashDistance;
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
			yield return new WaitForSeconds(0.2f);
			rigidbody.gravityScale = gravity;
			--dashCount;
			isDashing = false;
		}
	}

	


}
