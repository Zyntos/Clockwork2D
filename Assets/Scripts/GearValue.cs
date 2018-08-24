using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearValue : MonoBehaviour {


    public int value = 0;
    public LayerMask whatIsPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            
            collision.gameObject.GetComponent<CharController>().AddGears(value);
            Destroy(this.gameObject);
        }
    }
}
