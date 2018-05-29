using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject objToFollow;
    Transform locToFollow;

    public int currentCamIndex = 0;
    public Vector3[] listOfPositions = new Vector3[5];

    private void Awake()
    {
        listOfPositions[0] = GameObject.Find("Cam1").transform.position;
        listOfPositions[1] = GameObject.Find("Cam2").transform.position;
        listOfPositions[2] = GameObject.Find("Cam3").transform.position;
        listOfPositions[3] = GameObject.Find("Cam4").transform.position;
        listOfPositions[4] = GameObject.Find("Cam5").transform.position;
    }

    // Use this for initialization
    void Start () {


        //transform.position = listOfPositions[currentCamIndex];
    }
	
	// Update is called once per frame
	void Update () {

        locToFollow = objToFollow.transform;
		
	}

    private void FixedUpdate()
    {
        
    }

    public void NextCamera()
    {
        if (currentCamIndex == listOfPositions.Length-1)
            currentCamIndex = 0;
        else
            currentCamIndex++;
        transform.position = listOfPositions[currentCamIndex];
        gameObject.GetComponent<CamShake>().GetNewCamPos();
    }

    public void SetInitialCamera(int index)
    {
        currentCamIndex = index;
        transform.position = listOfPositions[currentCamIndex];
        gameObject.GetComponent<CamShake>().GetNewCamPos();
    }
}
