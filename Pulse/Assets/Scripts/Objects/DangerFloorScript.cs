using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerFloorScript : MonoBehaviour {

    GameManagerScript mainGameScript;
    public bool isBeingActivated = false;
    public bool hasBeenActivated = false;
    public Material deactivatedMat;
    public Material activatedMat;
    GameObject playerInTrigger;
    bool isPlayerInTrigger = false;

	// Use this for initialization
	void Start () {
        mainGameScript = GameObject.Find("GameManagerObj").GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if(playerInTrigger != null)
        {
            if (!playerInTrigger.activeInHierarchy){
                playerInTrigger = null;
            }
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInTrigger = true;
            playerInTrigger = other.gameObject;

            if (!isBeingActivated && !hasBeenActivated)
            {
                isBeingActivated = true;
                StartCoroutine(ActivationDelay());
            }
            else if (hasBeenActivated)
            {
                other.gameObject.GetComponent<PlayerDeathScript>().DedzFromFloor();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInTrigger = false;
        }
    }

    IEnumerator ActivationDelay()
    {
        yield return new WaitForSeconds(.25f);
        Activation();
    }

    public void Activation()
    {
        hasBeenActivated = true;
        isBeingActivated = false;

        gameObject.GetComponent<Renderer>().material = activatedMat;

        //Send another check to see if the player is onn this square
        if (isPlayerInTrigger)
        {
            if (playerInTrigger != null)
            {
                playerInTrigger.GetComponent<PlayerDeathScript>().DedzFromFloor();
            }
            playerInTrigger = null;
            isPlayerInTrigger = false;
        }
    }

    public void ResetForSpawn()
    {
        hasBeenActivated = false;
        isBeingActivated = false;
        gameObject.GetComponent<Renderer>().material = deactivatedMat;
    }
}
