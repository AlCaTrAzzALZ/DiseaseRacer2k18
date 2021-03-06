﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BaseDisease baseDiseasePrefab_;

    [SerializeField]
    BaseDisease blackDeathPrefab_;

    [SerializeField]
    BaseDisease SyphilisPrefab_;

    [SerializeField]
    BaseDisease ChlamydiaPrefab_;

    [SerializeField]
    BaseDisease EcoliPrefab_;

    [SerializeField]
    BaseDisease MadCowPrefab_;

    [SerializeField]
    SplitScreenCamera splitCameraPrefab_;

    [SerializeField]
    ScreenCanvas screenCanvas_;

    List<Camera> cameras_ = new List<Camera>();

    List<BaseDisease> diseases_ = new List<BaseDisease>();

    public int numberOfPlayers_ = 2;

    bool hasRaceStarted_ = false;
    bool isRaceOver_ = false;

    List<BaseDisease> finishingOrder_ = new List<BaseDisease>();

    float raceTime = 0f;

    [SerializeField]
    AudioClip readyClip;
    [SerializeField]
    AudioClip setClip;
    [SerializeField]
    AudioClip goClip;

    void Start ()
    {
        for (int i = 0; i < numberOfPlayers_; i++)
        {
            CreatePlayer(i);
        }

        StartCoroutine(DoPreRace());
	}

    IEnumerator DoPreRace()
    {
        float preRaceTimer = 0f;
        bool startRace = false;

        bool set = false;
        bool go = false;

        screenCanvas_.raceStartText_.text = "Ready...";
        AudioSource.PlayClipAtPoint(readyClip, this.transform.position);
        screenCanvas_.raceStartText_.GetComponent<Animation>().Play();

        do
        {
            if (preRaceTimer > 1.5f && !set)
            {
                screenCanvas_.raceStartText_.text = "Set...";
                AudioSource.PlayClipAtPoint(setClip, this.transform.position);
                screenCanvas_.raceStartText_.GetComponent<Animation>().Play();
                set = true;
            }

            if (preRaceTimer > 3.0f && !go)
            {
                screenCanvas_.raceStartText_.text = "Go!";
                AudioSource.PlayClipAtPoint(goClip, this.transform.position);
                screenCanvas_.raceStartText_.GetComponent<Animation>().Play();
                go = true;
                startRace = true;
            }

            preRaceTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (!startRace);

        StartRace();
    }

    void StartRace()
    {
        foreach (var disease in diseases_)
        {
            disease.IsRacing = true;
        }

        hasRaceStarted_ = true;
    }

    private void Update()
    {
        if (hasRaceStarted_ && !isRaceOver_)
        {
            raceTime += Time.deltaTime;
            screenCanvas_.raceTimeText_.text = "RACE TIME \n" + string.Format("{0:0}:{1:00}", (int)raceTime / 60, (int)raceTime % 60);
        }
    }

    public void CrossedFinishLine(BaseDisease disease)
    {
        finishingOrder_.Add(disease);

        disease.IsRacing = false;

        disease.GetComponentInChildren<SplitCanvas>().ShowEndRaceInfo(disease.racePosition, diseases_.Count, raceTime);

        if (!isRaceOver_)
        {
            EndRace();
            isRaceOver_ = true;
        }
    }

    void EndRace()
    {
        //show end race UI
        screenCanvas_.raceEndText_.gameObject.SetActive(true);
    }

    void CreatePlayer(int playerId)
    {
        BaseDisease diseasePrefab = baseDiseasePrefab_;

        int val = Random.Range(1, 6);

        switch (val)
        {
            case 1:
                diseasePrefab = blackDeathPrefab_;
                break;

            case 2:
                diseasePrefab = SyphilisPrefab_;
                break;

            case 3:
                diseasePrefab = ChlamydiaPrefab_;
                break;

            case 4:
                diseasePrefab = EcoliPrefab_;
                break;

            case 5:
                diseasePrefab = MadCowPrefab_;
                break;

            default:
                Debug.LogError("You fucked up");
                break;
        }


        BaseDisease disease = Instantiate(diseasePrefab, GetSpawnPoint().position, Quaternion.identity);
        var motor = disease.GetComponent<WheelDrive>();
        motor.playerId = playerId;

        var camera = Instantiate(splitCameraPrefab_, disease.transform);
        switch (playerId)
        {
            case 0:
                camera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
                break;
            case 1:
                camera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                break;
            default:
                Debug.LogError("unable to setup split screen camera");
                break;
        }

        camera.GetComponentInChildren<SplitCanvas>().diseaseRef_ = disease;

        camera.GetComponent<FollowCamera>().target = disease.gameObject;

        cameras_.Add(camera.GetComponent<Camera>());

        diseases_.Add(disease);
    }

    Transform GetSpawnPoint()
    {
        var spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (var spawnPoint in spawnPoints)
        {
            if (!spawnPoint.HasBeenUsed)
            {
                spawnPoint.HasBeenUsed = true;
                return spawnPoint.transform;
            }
        }

        Debug.LogError("Could not find valid spawn point, you need to add more!!!");
        return gameObject.transform;
    }
}
