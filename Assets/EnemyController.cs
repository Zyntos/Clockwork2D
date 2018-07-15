using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float maxlife = 100;
    public float life;

	// Use this for initialization
	void Start () {
        life = maxlife;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(life <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void getDamaged(float dmg)
    {
        life -= dmg;
    }

}
