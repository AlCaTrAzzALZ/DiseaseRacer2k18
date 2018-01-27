﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDisease : MonoBehaviour
{

    public string diseaseName_ = "Base Disease";

    public DiseaseMotor motor;

    public int racePosition;

    bool isRacing_ = false;

    public bool IsRacing
    {
        get { return isRacing_; }
        set { isRacing_ = value; }
    }

    // Use this for initialization
    void Start ()
    {
        motor = GetComponent<DiseaseMotor>();
	}

}
