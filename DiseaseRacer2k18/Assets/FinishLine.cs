using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FinishLine : MonoBehaviour
{
    GameManager gameManagerRef_;

	// Use this for initialization
	void Start ()
    {
        gameManagerRef_ = FindObjectOfType<GameManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BaseDisease>())
        {
            gameManagerRef_.CrossedFinishLine(other.GetComponentInParent<BaseDisease>());
        }
    }
}
