using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public bool hasBeenUsed_ = false;

    public bool HasBeenUsed
    {
        get { return hasBeenUsed_; }
        set { hasBeenUsed_ = value; }
    }
}
