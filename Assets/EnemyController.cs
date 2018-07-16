using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("ENEMY VALUES")]
    public float maxlife = 100;
    float life;

	// Use this for initialization
	void Start () {
        life = maxlife;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
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
    }

}
