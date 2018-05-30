using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepCanvas : MonoBehaviour {

    public static GameObject canvasScores;

    private void Awake()
    {
        if (canvasScores)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            canvasScores = gameObject;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
