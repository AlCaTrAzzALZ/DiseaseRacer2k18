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

    [SerializeField]
    TextMeshProUGUI endRaceText_;

    [SerializeField]
    TextMeshProUGUI endRaceInfo_;

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

    public void ShowEndRaceInfo(int pos, int numberOfRacers, float time)
    {
        endRaceText_.text = (pos <= 1) ? "You Win!" : "You Lose!";
        endRaceInfo_.text = "Pos: " + pos.ToString() + "/" + numberOfRacers.ToString() + "\n Time: " + time.ToString("F2") + "s";

        endRaceText_.gameObject.SetActive(true);
        endRaceInfo_.gameObject.SetActive(true);
    }
}