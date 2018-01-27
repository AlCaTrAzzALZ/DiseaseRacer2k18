using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squish : MonoBehaviour {

	public AudioClip squishSound;
	public float soundCooldown;

	private AudioSource source;
	private float lastTime;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		lastTime = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			source.PlayOneShot(squishSound, 1.0f);
			Debug.Log ("space");
		}	
	}
}
