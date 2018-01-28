using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    [Range(.01f, 1)]
    public float playerSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyMovement(float vertSpeed, float horiSpeed)
    {
        transform.Translate(new Vector3(horiSpeed, 0, vertSpeed) * playerSpeed);
    }

}
