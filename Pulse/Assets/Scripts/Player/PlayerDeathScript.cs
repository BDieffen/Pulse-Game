using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour {

    public GameManagerScript gameManagerScript;
    public GoalZoneScript goalScript;
    public CameraFollow camScript;
    PlayerController playerCon;
    BoxCollider playerBounds;
    public PlayerOutsides[] playerOutsides = new PlayerOutsides[4];
    bool dying = false;

	// Use this for initialization
	void Start () {
        gameManagerScript = GameObject.Find("GameManagerObj").GetComponent<GameManagerScript>();
        goalScript = GameObject.Find("GameManagerObj").GetComponent<GoalZoneScript>();
        camScript = Camera.main.GetComponent<CameraFollow>();
        playerBounds = gameObject.GetComponent<BoxCollider>();
        playerCon = gameObject.GetComponent<PlayerController>();
        dying = false;

        playerOutsides = GetComponentsInChildren<PlayerOutsides>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        //PLAYER CURRENTLY NOT RESPAWNING CORRECTLY WHEN DYING BY COLLIDING WITH A WALL
        if(col.gameObject.tag == "Wall" && dying == false)
        {
            dying = true;
            playerBounds.enabled = false;
            ContactPoint contact = col.contacts[0];
            gameManagerScript.KillPlayer(1, contact.point);
        }
        if(col.gameObject.tag == "LaserBeam")
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            gameManagerScript.TimerCheck(goalScript.startPosIndex);
            camScript.NextCamera();
            goalScript.NextLevel();
        }
        /*else if (other.gameObject.tag == "Wall")
        {
            playerBounds.enabled = false;
            gameManagerScript.KillPlayer(1;
        }*/
        else if(other.gameObject.tag == "LaserBeam")
        {
            playerBounds.enabled = false;
            gameManagerScript.rbSpeed = new Vector3(playerCon.horiSpeed, 0, playerCon.vertSpeed);
            gameManagerScript.KillPlayer(2, new Vector3(0,0,0));
        }
    }
}
