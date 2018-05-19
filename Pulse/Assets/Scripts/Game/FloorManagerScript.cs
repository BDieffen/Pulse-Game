using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManagerScript : MonoBehaviour {
    public GameObject[] levelTraps = new GameObject[5];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateFloor(int levelFloor)
    {
        levelTraps[levelFloor].SetActive(true);
    }

    public void DeactivateFloor(int levelFloor)
    {
        levelTraps[levelFloor].SetActive(false);
    }
}
