using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalZoneScript : MonoBehaviour {
    //public CameraFollow camScript;
    public int startPosIndex = 0;
    public Vector3[] listOfStartPos = new Vector3[5];
    GameManagerScript gameManagerScript;
    public GameObject player;
    public GameObject playerPrefab;
    bool isZone3 = false;
    public FloorManagerScript floorManage;
    public ZonesAndLevelsScript zoneManager;
    public TimeKeeper timeKeeps;

    public CameraFollow camFollow;

    public bool soloLevel = false;

    private void Awake()
    {
        zoneManager = GameObject.Find("LevelID").GetComponent<ZonesAndLevelsScript>();
        timeKeeps = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();

        startPosIndex = 0;
        soloLevel = false;
    }

    // Use this for initialization
    void Start () {
        listOfStartPos[0] = GameObject.Find("PlayerStart1").transform.position;
        listOfStartPos[1] = GameObject.Find("PlayerStart2").transform.position;
        listOfStartPos[2] = GameObject.Find("PlayerStart3").transform.position;
        listOfStartPos[3] = GameObject.Find("PlayerStart4").transform.position;
        listOfStartPos[4] = GameObject.Find("PlayerStart5").transform.position;

        //Gets the starting index from the menu selection in previous menu scene.
        startPosIndex = CalculateStartPos(timeKeeps.startingPositionFromMenu);

        //Initially places the camera in the correct spot
        camFollow.SetInitialCamera(startPosIndex);

        //Calculates which level the player has selected in the menu and starts them in the right spot.
        player.transform.position = listOfStartPos[startPosIndex];
        //Lets the time keeping script to know what zone and level is being timed.
        zoneManager.levelID = startPosIndex;
        zoneManager.UpdateZoneID();

        gameManagerScript = gameObject.GetComponent<GameManagerScript>();
        if (SceneManager.GetActiveScene().name == "Zone3")
        {
            floorManage = GameObject.Find("Floor Manager").GetComponent<FloorManagerScript>();
            isZone3 = true;
            floorManage.levelTraps[0].SetActive(false);
            floorManage.levelTraps[1].SetActive(false);
            floorManage.levelTraps[2].SetActive(false);
            floorManage.levelTraps[3].SetActive(false);
            floorManage.levelTraps[4].SetActive(false);

            floorManage.levelTraps[startPosIndex].SetActive(true);
        }
        else floorManage = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextLevel()
    {
        if (!soloLevel)
        {
            if (startPosIndex == listOfStartPos.Length - 1)
            {
                startPosIndex = 0;
                if (isZone3)
                    floorManage.ActivateFloor(startPosIndex);
            }
            else
            {
                if (isZone3)
                    floorManage.DeactivateFloor(startPosIndex);
                startPosIndex++;
                if (isZone3)
                    floorManage.ActivateFloor(startPosIndex);
            }
            player.transform.position = listOfStartPos[startPosIndex];
        }
        else
        {
            timeKeeps.ToggleMenuObjs(1);
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }

    public void ResetToLastSpawn()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("PlayerRB"))
        {
            Destroy(obj);
        }
        player = Instantiate(playerPrefab, listOfStartPos[startPosIndex], playerPrefab.transform.rotation);
        player.name = "OutlinePlayer";
        gameManagerScript.player = player;
    }

    public int CalculateStartPos(int level)
    {
        switch (level)
        {
            case 1:
                soloLevel = true;
                return 0;
            case 2:
                soloLevel = true;
                return 1;
            case 3:
                soloLevel = true;
                return 2;
            case 4:
                soloLevel = true;
                return 3;
            case 5:
                soloLevel = true;
                return 4;
            default:
                soloLevel = false;
                return 0;
        }
    }
}
