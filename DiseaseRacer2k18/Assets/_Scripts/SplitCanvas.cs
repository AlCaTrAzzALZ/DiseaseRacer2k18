using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplitCanvas : MonoBehaviour
{
    public BaseDisease diseaseRef_;
    Camera cameraRef_;

    [SerializeField]
    TextMeshProUGUI diseaseNameText_;

    [SerializeField]
    BoostMeter boostMeterRef_;

    private void Awake()
    {
        cameraRef_ = GetComponentInParent<Camera>();
    }

    bool canvasInitialised_ = false;

	void Update ()
    {
		if (!canvasInitialised_)
        {
            if (diseaseRef_)
            {
                diseaseNameText_.text = diseaseRef_.diseaseName_ + " - "+ diseaseRef_.GetComponent<DiseaseMotor>().playerId;

                canvasInitialised_ = true;
            }
        }
	}

    private void LateUpdate()
    {
        if (canvasInitialised_ && boostMeterRef_ && diseaseRef_)
        {
            boostMeterRef_.SetBoostLevel(diseaseRef_.motor.BoostFuel);
        }
    }
}
