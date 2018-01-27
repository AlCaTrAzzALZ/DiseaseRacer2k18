using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostMeter : MonoBehaviour
{
    [SerializeField]
    Image backgroundRef_;
    
    [SerializeField]
    Image foregroundRef_;

    [SerializeField]
    TextMeshProUGUI boostTextRef_;

    // value from 0-1
    public void SetBoostLevel(float boostPercentage)
    {
        foregroundRef_.rectTransform.localScale = new Vector3(foregroundRef_.rectTransform.localScale.x, Mathf.Clamp01(boostPercentage), foregroundRef_.rectTransform.localScale.z);
        boostTextRef_.text = boostPercentage.ToString("F1");

        boostTextRef_.color = (boostPercentage >= 1f) ? Color.white : Color.black;
    }
}
