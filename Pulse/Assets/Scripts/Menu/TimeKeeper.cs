﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeKeeper : MonoBehaviour {

    public static GameObject Instance;

    string filePath;
    public int zone = 0;
    public int level = 0;

    public int startingPositionFromMenu = 0;

    public float currentTime = 0;
    public float newHighScore = 0;

    public TextMeshProUGUI[] objectsToWriteToZone1 = new TextMeshProUGUI[6];
    public TextMeshProUGUI[] objectsToWriteToZone2 = new TextMeshProUGUI[6];
    public TextMeshProUGUI[] objectsToWriteToZone3 = new TextMeshProUGUI[6];

    public float[] allZone1Levels = new float[6];
    public float[] allZone2Levels = new float[6];
    public float[] allZone3Levels = new float[6];

    public GameObject theCanvas;
    public GameObject[] mainMenuToggles;

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = gameObject;
        }
    }
    // Use this for initialization
    void Start () {
        SetArrays();

        if (File.Exists(filePath + "/PlayerTimes.dat"))
        {
            LoadTimes();
        }
        else Save();

        theCanvas = GameObject.Find("Canvas");
        mainMenuToggles = GameObject.FindGameObjectsWithTag("MainMenuToggle");
    }
	
	// Update is called once per frame
	void Update () {
        	
	}

    void SetArrays()
    {
        allZone1Levels[0] = 0;
        allZone1Levels[1] = 0;
        allZone1Levels[2] = 0;
        allZone1Levels[3] = 0;
        allZone1Levels[4] = 0;
        allZone1Levels[5] = 0;

        allZone2Levels[0] = 0;
        allZone2Levels[1] = 0;
        allZone2Levels[2] = 0;
        allZone2Levels[3] = 0;
        allZone2Levels[4] = 0;
        allZone2Levels[5] = 0;

        allZone3Levels[0] = 0;
        allZone3Levels[1] = 0;
        allZone3Levels[2] = 0;
        allZone3Levels[3] = 0;
        allZone3Levels[4] = 0;
        allZone3Levels[5] = 0;
    }

    public void WriteYellowScores()
    {
        for (int i = 0; i < 6; i++)
        {
            float minuteTime = 0;
            float seconds = allZone1Levels[i];

            while (seconds > 60f)
            {
                minuteTime++;
                seconds -= 60f;
            }
            objectsToWriteToZone1[i].text = minuteTime + ":" + seconds.ToString("F2");
        }
    }

    public void WriteBlueScores()
    {
        for (int i = 0; i < 6; i++)
        {
            float minuteTime2 = 0;
            float seconds2 = allZone2Levels[i];

            while (seconds2 > 60f)
            {
                minuteTime2++;
                seconds2 -= 60f;
            }
            objectsToWriteToZone2[i].text = minuteTime2 + ":" + seconds2.ToString("F2");
        }
    }

    public void WriteRedScores()
    {
        for (int i = 0; i < 6; i++)
        {
            float minuteTime3 = 0;
            float seconds3 = allZone3Levels[i];

            while (seconds3 > 60f)
            {
                minuteTime3++;
                seconds3 -= 60f;
            }
            objectsToWriteToZone3[i].text = minuteTime3 + ":" + seconds3.ToString("F2");
        }
    }

    //Saves the player's high scores
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerTimes.dat");

        PlayerTimes data = new PlayerTimes();

        data.zone1Levels = allZone1Levels;
        data.zone2Levels = allZone2Levels;
        data.zone3Levels = allZone3Levels;

        bf.Serialize(file, data);
        file.Close();
    }

    //Loads the player's high scores
    public void LoadTimes()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerTimes.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerTimes.dat", FileMode.Open);

            PlayerTimes data = (PlayerTimes)bf.Deserialize(file);
            file.Close();

            data.zone1Levels = allZone1Levels;
            data.zone2Levels = allZone2Levels;
            data.zone3Levels = allZone3Levels;
        }
    }

    //The class that is created and retreived when saving and loading the high score file
    [System.Serializable]
    public class PlayerTimes
    {
        public float[] zone1Levels;
        public float[] zone2Levels;
        public float[] zone3Levels;
    }

    public void ToggleMenuObjs(int i)
    {
        if (i == 0)
        {
            foreach (GameObject toggleObj in mainMenuToggles)
            {
                if (toggleObj.activeInHierarchy)
                    toggleObj.SetActive(false);
            }
        }
        else if (i == 1)
        {
            foreach (GameObject toggleObj in mainMenuToggles)
            {
                if (!toggleObj.activeInHierarchy)
                    toggleObj.SetActive(true);
            }
        }
        else Debug.Log("Please pass in either a 1(on) or 0(off)");

    }
}
