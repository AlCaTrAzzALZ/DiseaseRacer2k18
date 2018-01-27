using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [SerializeField]
    Vector3 posOffset;

    [SerializeField]
    Vector3 rotOffset;
	
	// Update is called once per frame
	void LateUpdate ()
    {
		if (transform.parent)
        {
            this.gameObject.transform.position = transform.parent.position + posOffset;
            //this.gameObject.transform.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles + rotOffset);
        }
	}
}
