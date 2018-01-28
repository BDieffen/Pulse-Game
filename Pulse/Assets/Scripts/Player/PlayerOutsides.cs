using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutsides : MonoBehaviour {

    public GameManagerScript gameManager;
    Renderer objRenderer;
    Color objColor;
    float colorAlpha;
    public bool fadeOut = false;
    float fadeSpeed = 1f;

	// Use this for initialization
	void Start () {
        objRenderer = gameObject.GetComponent<Renderer>();
        objColor = objRenderer.material.color;
        colorAlpha = objColor.a;
        gameManager = GameObject.Find("GameManagerObj").GetComponent<GameManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Detects if a piece of the player hits a laserbeam and then activates it's fading away and starting a particle effect.
        if (other.gameObject.tag == "LaserBeam")
        {
            fadeOut = true;
            if (!gameObject.GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
    private void FixedUpdate()
    {
        if (fadeOut && colorAlpha > 0)
        {
            colorAlpha -= fadeSpeed * Time.deltaTime;
            objColor = new Color(objColor.r, objColor.g, objColor.b, colorAlpha);
            objRenderer.material.color = objColor;
        }else if (colorAlpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
