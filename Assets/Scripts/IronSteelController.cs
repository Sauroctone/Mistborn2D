using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSteelController : MonoBehaviour {

	public LineRenderer line;
	private bool isClicking;

	private RaycastHit2D hit;
	private BoxCollider2D hitSizeCollider;
	private float hitSize;

	private Rigidbody2D rb;
	private PlayerController player;
	private BoxCollider2D playerSizeCollider;
	private float playerSize;

	public float ppForce; //push pull force

	//http://answers.unity3d.com/questions/860212/world-coordinates-of-boxcollider2d.html

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		player = GetComponent<PlayerController> ();
		playerSizeCollider = GetComponent<BoxCollider2D> ();
		playerSize = playerSizeCollider.size.x * playerSizeCollider.size.y;
		print (playerSize);
	}

	void FixedUpdate()
	{
		if (!isClicking) 
		{
			if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown(1)) 
			{
				//Getting the object under cursor
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				hit = Physics2D.Raycast (ray.origin, ray.direction);

				//Check if it's a metal
				if (hit.collider != null && hit.collider.tag == "Metal") 
				{
					isClicking = true;
					line.gameObject.SetActive (true);
					player.isJumping = false;

					hitSizeCollider = hit.transform.gameObject.GetComponent<BoxCollider2D>();
					hitSize = hitSizeCollider.size.x * hitSizeCollider.size.y;
					print (hitSize);

				}
			}
		}

		if (isClicking)
		{
			line.SetPosition (0, transform.position);
			line.SetPosition (1, hit.transform.position);

			if (playerSize < hitSize)
			{
				if (Input.GetMouseButton (0)) 
				{
					rb.AddForce ((transform.position - hit.transform.position).normalized * ppForce, ForceMode2D.Impulse);
				}

				if (Input.GetMouseButton (1)) 
				{
					rb.AddForce ((hit.transform.position - transform.position).normalized * ppForce, ForceMode2D.Impulse);
				}
			}
				

			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				isClicking = false;
				line.gameObject.SetActive (false);
			}
		}
	}
}
