using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManagerScript : MonoBehaviour {

    public List<List<GameObject>> laserSections = new List<List<GameObject>>();
    public List<GameObject> section1 = new List<GameObject>();
    public List<GameObject> section2 = new List<GameObject>();
    public List<GameObject> section3 = new List<GameObject>();
    float timer = 0;

    // Use this for initialization
    void Start () {

        laserSections.Add(section1);
        laserSections.Add(section2);
        laserSections.Add(section3);

        for(int i=0; i<laserSections.Count; i++)
        {
            if(laserSections[i] == null)
            {
                laserSections.RemoveAt(i);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0;
            for (int i = 0; i < laserSections.Count; i++) 
            {
                //StartCoroutine(ToggleLaser(laserSections[i]));
                StartCoroutine(Toggle(laserSections[i]));
            }
        }
    }

    IEnumerator Toggle(List<GameObject> list)
    {
        yield return new WaitForSeconds(1);
        //StartCoroutine(ToggleLaser(list));
        ToggleLaser(list);
    }

    public void ToggleLaser(List<GameObject> list)
    {
        //yield return new WaitForSeconds(1);
        for (int i = 0; i < list.Count; i++)
        {
            aLaserScript laser;
            laser = list[i].GetComponent<aLaserScript>();
            if (laser.laserBeam.activeInHierarchy)
            {
                laser.laserBeam.SetActive(false);
            }else if(!laser.preLaser.activeInHierarchy && !laser.laserBeam.activeInHierarchy)
            {
                laser.preLaser.SetActive(true);
            }
            else if(laser.preLaser.activeInHierarchy)
            {
                laser.preLaser.SetActive(false);
                laser.laserBeam.SetActive(true);
            }
        }
    }

    IEnumerator ToggPreBeam(List<GameObject> list)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < list.Count; i++)
        {
            aLaserScript laser;
            laser = list[i].GetComponent<aLaserScript>();
            if (!laser.preLaser.activeInHierarchy && !laser.laserBeam.activeInHierarchy)
            {
                laser.preLaser.SetActive(true);
            }
            else if (laser.preLaser.activeInHierarchy)
            {
                laser.preLaser.SetActive(false);
            }
        }
    }

    //HAVE NOT EDITED FROM COPYING OVER FROM TogglePreBeam COROUTINE
    IEnumerator ToggleBeam(List<GameObject> list)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < list.Count; i++)
        {
            aLaserScript laser;
            laser = list[i].GetComponent<aLaserScript>();
            if (laser.laserBeam.activeInHierarchy)
            {
                laser.laserBeam.SetActive(false);
            }
            else if (!laser.preLaser.activeInHierarchy && !laser.laserBeam.activeInHierarchy)
            {
                laser.preLaser.SetActive(true);
            }
            else if (laser.preLaser.activeInHierarchy)
            {
                laser.preLaser.SetActive(false);
                laser.laserBeam.SetActive(true);
            }
        }
    }
}
