﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("ENEMY VALUES")]
    public float maxlife = 100;
    float life;

    public Material onHit;
    public Material norm;

	// Use this for initialization
	void Start () {
        life = maxlife;
	}
	
	// Update is called once per frame
	void Update () {
		
        //DESTROY ENEMY
        if(life <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    //ENEMY GET DAMAGED
    public void getDamaged(float dmg)
    {
        life -= dmg;
        GetComponent<SpriteRenderer>().material = onHit;
        StartCoroutine(MatChange());
    }

    IEnumerator MatChange()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().material = norm;
    }

}
