using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private Vector2 velocity;

    public float moveSpeed = 5f;
    
    // new operator
    public float jumpForce = 10f;

    public Transform groundCheck;
	public LayerMask ground;

	private bool isGround, isJump;
	bool jumpPressed;
	int jumpCount;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


	void Update() // Update is called once per frame
	{
		if (Input.GetButtonDown("Jump") && jumpCount > 0)
		{
			jumpPressed = true;
		}
	}

	private void FixedUpdate()
	{
		isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
		GroundMovement();
		Jump();
		// SwitchAnim();
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
			isJump = false;
		}

		if (jumpPressed && isGround)
		{
			isJump = true;
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
			jumpCount--;
			jumpPressed = false;
		}
		else if (jumpPressed && jumpCount > 0 && isJump)
		{
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
			jumpCount--;
			jumpPressed = false;
		}
	}


}
