using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesAndLevelsScript : MonoBehaviour {
    public int levelID = 1;
    public int currentZone = 1;
    TimeKeeper timeKeeper;

    private void Awake()
    {
        timeKeeper = GameObject.Find("TimeManager").GetComponent<TimeKeeper>();
        levelID = timeKeeper.level;
        currentZone = timeKeeper.zone;
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateZoneID()
    {
        timeKeeper.zone = currentZone;
        timeKeeper.level = levelID;
    }
}
