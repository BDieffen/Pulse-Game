using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public TimeKeeper timeKeep;
    public static GameObject MenuObj;

    private void Awake()
    {
        if (MenuObj)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            MenuObj = gameObject;
        }
    }
    // Use this for initialization
    void Start () {
        timeKeep = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();

        if (!timeKeep)
        {
            //timeKeep.GrabScoreText();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PassZone(int z)
    {
        timeKeep = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();
        timeKeep.zone = z;
    }

    public void PassLevel(int l)
    {
        timeKeep = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();
        timeKeep.startingPositionFromMenu = l;
    }

    public void WriteScores()
    {
        timeKeep = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();
        timeKeep.WriteYellowScores();
        timeKeep.WriteBlueScores();
        timeKeep.WriteRedScores();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartYellowZone()
    {
        GameObject.Find("Zone1LevelSelect").SetActive(false);
        timeKeep.ToggleMenuObjs(0);
        SceneManager.LoadScene("Zone1");
    }

    public void StartBlueZone()
    {
        GameObject.Find("Zone2LevelSelect").SetActive(false);
        timeKeep.ToggleMenuObjs(0);
        SceneManager.LoadScene("Zone2");
    }

    public void StartRedZone()
    {
        GameObject.Find("Zone3LevelSelect").SetActive(false);
        timeKeep.ToggleMenuObjs(0);
        SceneManager.LoadScene("Zone3");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
