using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    GameManagerScript gameManagerScript;
    PlayerMotor playerMotor;
    public float vertSpeed;
    public float horiSpeed;

	// Use this for initialization
	void Start () {
        playerMotor = gameObject.GetComponent<PlayerMotor>();
        gameManagerScript = GameObject.Find("GameManagerObj").GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameManagerScript.gameState == 1)
        {
            vertSpeed = Input.GetAxisRaw("Vertical");
            horiSpeed = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.R))
            {
                gameManagerScript.gameState = 0;
                gameManagerScript.ResetToSpawn();
            }
        }
        //A key press that sends the player back to the main menu. (As of right now the escape key goes back to main manu at any time during game play)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TimeKeeper tempTime = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();
            tempTime.theCanvas.SetActive(true);
            tempTime.ToggleMenuObjs(1);

            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }

    private void FixedUpdate()
    {
        if (gameManagerScript.gameState == 1)
        {
            if (vertSpeed != 0 || horiSpeed != 0)
            {
                playerMotor.ApplyMovement(vertSpeed, horiSpeed);
            }
        }
    }
}
