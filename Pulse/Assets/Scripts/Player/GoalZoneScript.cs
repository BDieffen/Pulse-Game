using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZoneScript : MonoBehaviour {
    //public CameraFollow camScript;
    public int startPosIndex = 0;
    public Vector3[] listOfStartPos = new Vector3[5];
    GameManagerScript gameManagerScript;
    public GameObject player;
    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        listOfStartPos[0] = GameObject.Find("PlayerStart1").transform.position;
        listOfStartPos[1] = GameObject.Find("PlayerStart2").transform.position;
        listOfStartPos[2] = GameObject.Find("PlayerStart3").transform.position;
        listOfStartPos[3] = GameObject.Find("PlayerStart4").transform.position;
        listOfStartPos[4] = GameObject.Find("PlayerStart5").transform.position;

        player.transform.position = listOfStartPos[startPosIndex];

        gameManagerScript = gameObject.GetComponent<GameManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            gameManagerScript.TimerCheck(startPosIndex);
            camScript.NextCamera();
            NextLevel();
        }
    }*/

    public void NextLevel()
    {
        if (startPosIndex == listOfStartPos.Length-1)
            startPosIndex = 0;
        else
            startPosIndex++;
        player.transform.position = listOfStartPos[startPosIndex];
    }

    public void ResetToLastSpawn()
    {
        //gameObject.GetComponent<PlayerController>().vertSpeed = 0;
        //gameObject.GetComponent<PlayerController>().horiSpeed = 0;

        //transform.position = listOfStartPos[startPosIndex];
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

        //Destroy(player);
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("PlayerRB"))
        {
            Destroy(obj);
        }
        player = Instantiate(playerPrefab, listOfStartPos[startPosIndex], playerPrefab.transform.rotation);
        player.name = "OutlinePlayer";
        gameManagerScript.player = player;
    }
}
