using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartYellowZone()
    {
        SceneManager.LoadScene("Zone1");
    }

    public void StartBlueZone()
    {
        SceneManager.LoadScene("Zone2");
    }

    public void StartRedZone()
    {
        SceneManager.LoadScene("Zone3");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
