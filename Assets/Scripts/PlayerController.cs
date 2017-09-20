using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float lastXInput;
	public float speed = 5f;
	public float jumpForce = 3f;
	public float maxVelocity;
	private float movLerp;
	public float airControl;

	private Rigidbody2D rb;
	private bool isGrounded = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		float xinput = Input.GetAxisRaw ("Horizontal");	

		if (isGrounded || xinput != 0)
			rb.velocity = Vector2.Lerp (rb.velocity, new Vector2 (xinput * speed, rb.velocity.y), movLerp);

		if (isGrounded) 
		{
			if (Input.GetButtonDown ("Jump")) 
			{
				rb.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
			}

			movLerp = 1f;
		}

		else
		{
			movLerp = airControl;
		}
			
		rb.velocity = Vector2.ClampMagnitude (rb.velocity, maxVelocity);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Floor") 
		{
			isGrounded = true;
		}
	}		// isGrounded = true;

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Floor") 
		{
			isGrounded = false;
		}
	} 	//isGrounded = false;
}
