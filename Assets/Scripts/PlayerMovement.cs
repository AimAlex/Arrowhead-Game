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

	bool CollectSpring;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("spring"))
		{
			CollectSpring = true;
			Destroy(col.gameObject);
		}

		// if (col.CompareTag("Treasure"))
        // {
        //     // ++itemNumber;
        //     Destroy(col.gameObject);
        // }
	}

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


	void Update() // Update is called once per frame
	{
		isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
		GroundMovement();
		if (Input.GetButtonDown("Jump"))
		{
			Jump();
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
			jumpPressed = true;
		} else if (CollectSpring && jumpCount > 0)
		{
			jumpPressed = true;
		}

		if (jumpPressed)
		{
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
			jumpCount--;
			jumpPressed = false;
		}
	}


}
