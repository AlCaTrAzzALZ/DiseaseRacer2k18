using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocity : MonoBehaviour {

	public float forceMultiplier = 1;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float force = forceMultiplier * Input.GetAxis("Vertical");
		rb.AddForce (transform.forward * force);
	}
}
