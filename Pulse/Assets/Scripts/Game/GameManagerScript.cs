using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour {

    public GameObject pressToStart;
    public int gameState = 0;

    GameObject[] dangerFloors;

    public GameObject player;
    GameObject currentPlayerObj;
    GoalZoneScript goalScript;
    CameraFollow camScript;

    //Overall time in the level
    public TextMeshPro timerText;
    //The overall timer at the completion of each section
    TextMeshPro checkText1;
    TextMeshPro checkText2;
    TextMeshPro checkText3;
    TextMeshPro checkText4;
    TextMeshPro checkText5;
    //Time it took to complete each section individually
    TextMeshPro subTimer1;
    TextMeshPro subTimer2;
    TextMeshPro subTimer3;
    TextMeshPro subTimer4;
    TextMeshPro subTimer5;
    int latestMinuteTime = 0;
    float latestSecondsTime = 0;
    int minuteTimer = 0;
    float timer = 0;
    public GameObject[] playerOutlines = new GameObject[4];
    public Vector3 explosionLoc = new Vector3();
    float minExplosionForce = 75f;
    float explosionForce = 300;
    public Vector3 rbSpeed;
    float rbVelocityMultiplier = 5;

    float respawnTime = 1.5f;

    public GameObject returnToMenu;

	// Use this for initialization
	void Start () {
        camScript = Camera.main.GetComponent<CameraFollow>();
        goalScript = gameObject.GetComponent<GoalZoneScript>();
        //Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(8, 10);
        pressToStart = GameObject.Find("PressToStart");

        checkText1 = GameObject.FindGameObjectWithTag("TimerCheck1").GetComponent<TextMeshPro>();
        checkText2 = GameObject.FindGameObjectWithTag("TimerCheck2").GetComponent<TextMeshPro>();
        checkText3 = GameObject.FindGameObjectWithTag("TimerCheck3").GetComponent<TextMeshPro>();
        checkText4 = GameObject.FindGameObjectWithTag("TimerCheck4").GetComponent<TextMeshPro>();
        checkText5 = GameObject.FindGameObjectWithTag("TimerCheck5").GetComponent<TextMeshPro>();
        subTimer1 = GameObject.FindGameObjectWithTag("SubPoint1").GetComponent<TextMeshPro>();
        subTimer2 = GameObject.FindGameObjectWithTag("SubPoint2").GetComponent<TextMeshPro>();
        subTimer3 = GameObject.FindGameObjectWithTag("SubPoint3").GetComponent<TextMeshPro>();
        subTimer4 = GameObject.FindGameObjectWithTag("SubPoint4").GetComponent<TextMeshPro>();
        subTimer5 = GameObject.FindGameObjectWithTag("SubPoint5").GetComponent<TextMeshPro>();

        dangerFloors = GameObject.FindGameObjectsWithTag("DangerFloor");
    }
	
	// Update is called once per frame
	void Update () {
        if(gameState == 0)
        {
            if (!pressToStart.activeInHierarchy)
            {
                pressToStart.SetActive(true);
            }
            timer = 0.00f;
            minuteTimer = 0;
            timerText.text = minuteTimer + ":" + timer.ToString("F2");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameState = 1;
                pressToStart.SetActive(false);
            }
        }
        //Gamestate 2 is when the player finishes a level so they can view their times and score
        if(gameState == 2)
        {
            //Goes back to the main menu after pressing space once the level is finished.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                returnToMenu.SetActive(false);
                SceneManager.LoadScene(sceneBuildIndex: 0);
            }
        }
        //Gamestate 3 is when the player is dead
        if(gameState == 3)
        {

        }
		
	}

    private void FixedUpdate()
    {
        if (gameState == 1)
        {
            timer += Time.deltaTime;
            if (timer >= 60)
            {
                minuteTimer++;
                timer = 0;
            }
            timerText.text = minuteTimer + ":" +timer.ToString("F2");
        }
    }

    //POI = Point of Impact
    public void KillPlayer(int methodOfKill, Vector3 POI)
    {
        gameState = 3;
        playerOutlines = GameObject.FindGameObjectsWithTag("PlayerRB");
        explosionLoc = player.transform.position;

        for(int i=0; i<playerOutlines.Length; i++)
        {
            playerOutlines[i].GetComponent<Rigidbody>().isKinematic = false;
            playerOutlines[i].transform.parent = null;
            playerOutlines[i].GetComponent<BoxCollider>().enabled = true;
        }        

        if(methodOfKill == 1)
        {
            //Activates the camera shake for x seconds.
            Camera.main.GetComponent<CamShake>().shakeDuration = .03f;
            //THIS MAKES THE PLAYER EXPLODE
            for (int i = 0; i < playerOutlines.Length; i++)
            {
                playerOutlines[i].GetComponent<Rigidbody>().AddExplosionForce(Random.Range(minExplosionForce, explosionForce), POI, 8);
            }
        }
        else if(methodOfKill == 2)
        {
            for (int i = 0; i < playerOutlines.Length; i++)
            {
                playerOutlines[i].GetComponent<Rigidbody>().velocity = rbSpeed * rbVelocityMultiplier;
            }
        }
        else if (methodOfKill == 3)
        {
            //Activates the camera shake for x seconds.
            Camera.main.GetComponent<CamShake>().shakeDuration = .03f;
            for (int i = 0; i < playerOutlines.Length; i++)
            {
                playerOutlines[i].GetComponent<Rigidbody>().velocity = rbSpeed * rbVelocityMultiplier;
            }
        }

        //Destroys current player object and instantiates a new one.
        StartCoroutine(RespawnDelay());

    }

    public void ResetToSpawn()
    {
        camScript.currentCamIndex = camScript.listOfPositions.Length-1;
        camScript.NextCamera();
        goalScript.startPosIndex = goalScript.listOfStartPos.Length - 1;
        goalScript.NextLevel();
    }

    public void TimerCheck(int section)
    {
        switch (section)
        {
            case 0:
                checkText1.text = minuteTimer + ":" + timer.ToString("F2");
                //subTimer should be the amount of time on a specific section in the level. So total time - time from last section completion.
                subTimer1.text = minuteTimer - latestMinuteTime + ":" + (timer - latestSecondsTime).ToString("F2");
                latestMinuteTime = minuteTimer;
                latestSecondsTime = timer;
                break;
            case 1:
                checkText2.text = minuteTimer + ":" + timer.ToString("F2");
                //subTimer should be the amount of time on a specific section in the level. So total time - time from last section completion.
                subTimer2.text = minuteTimer - latestMinuteTime + ":" + (timer - latestSecondsTime).ToString("F2");
                latestMinuteTime = minuteTimer;
                latestSecondsTime = timer;
                break;
            case 2:
                checkText3.text = minuteTimer + ":" + timer.ToString("F2");                
                //subTimer should be the amount of time on a specific section in the level. So total time - time from last section completion.
                subTimer3.text = minuteTimer - latestMinuteTime + ":" + (timer - latestSecondsTime).ToString("F2");
                latestMinuteTime = minuteTimer;
                latestSecondsTime = timer;
                break;
            case 3:
                checkText4.text = minuteTimer + ":" + timer.ToString("F2");                
                //subTimer should be the amount of time on a specific section in the level. So total time - time from last section completion.
                subTimer4.text = minuteTimer - latestMinuteTime + ":" + (timer - latestSecondsTime).ToString("F2");
                latestMinuteTime = minuteTimer;
                latestSecondsTime = timer;
                break;
            case 4:
                checkText5.text = minuteTimer + ":" + timer.ToString("F2");                
                //subTimer should be the amount of time on a specific section in the level. So total time - time from last section completion.
                subTimer5.text = minuteTimer - latestMinuteTime + ":" + (timer - latestSecondsTime).ToString("F2");
                latestMinuteTime = minuteTimer;
                latestSecondsTime = timer;
                gameState = 2;
                returnToMenu.SetActive(true);
                break;         
            default:
                break;
        }
    }

    IEnumerator RespawnDelay()
    {
        GetCurrentPlayer();
        Destroy(currentPlayerObj);
        yield return new WaitForSeconds(respawnTime);
        //For each gameobject with tag DangerFloor, set that material to deactivated material
        if (dangerFloors != null)
        {
            foreach(GameObject floor in dangerFloors)
            {
                floor.GetComponent<DangerFloorScript>().ResetForSpawn();
            }
        }
        goalScript.ResetToLastSpawn();
        gameState = 1;
    }

    public void GetCurrentPlayer()
    {
        currentPlayerObj = GameObject.Find("OutlinePlayer");
        if (currentPlayerObj == null)
        {
            currentPlayerObj = GameObject.Find("OutlinePlayer(Clone)");
        }
    }
}
