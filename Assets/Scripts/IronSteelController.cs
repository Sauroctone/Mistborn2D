using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSteelController : MonoBehaviour {

	public LineRenderer line;
	private bool isClicking;
	private RaycastHit2D hit;
	private Rigidbody2D rb;

	public float ppForce; //push pull force

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		if (!isClicking) 
		{
			if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown(1)) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				hit = Physics2D.Raycast (ray.origin, ray.direction);

				if (hit.collider != null && hit.collider.tag == "Metal") 
				{
					isClicking = true;
				}
			}
		}

		if (isClicking)
		{
			line.gameObject.SetActive (true);
			line.SetPosition (0, transform.position);
			line.SetPosition (1, hit.transform.position);

			if (Input.GetMouseButton (0))
				rb.AddForce ((transform.position - hit.transform.position).normalized * ppForce, ForceMode2D.Impulse);

			if (Input.GetMouseButton (1))
				rb.AddForce ((hit.transform.position - transform.position).normalized.normalized * ppForce, ForceMode2D.Impulse);
		}

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			isClicking = false;
			line.gameObject.SetActive (false);
		}
	}
}
