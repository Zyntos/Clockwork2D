﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsDropController : MonoBehaviour {

    public GameObject gear;
    public GameObject mastery1;
    public GameObject mastery2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropGears()
    {
        GameObject gears = Instantiate(gear, this.gameObject.transform.position, Quaternion.identity);
        gears.GetComponent<GearValue>().value = Random.Range(1, 6);
        int masteryDrop = Random.Range(1, 6);
        if(masteryDrop > 3)
        {
            GameObject mastery = Instantiate(mastery1, this.gameObject.transform.position, Quaternion.identity);
        } 
    }


}
