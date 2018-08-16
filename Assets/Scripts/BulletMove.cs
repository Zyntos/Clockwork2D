using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    public float maxSpeed = 2;
    public LayerMask canHit;
    public LayerMask canDamage;
    public float bulletDamage = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(1 * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

   
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (((1 << collision.gameObject.layer) & canDamage) != 0)
        {
            collision.GetComponent<EnemyController>().getDamaged(bulletDamage);
            Destroy(this.gameObject);
        }
        else if (((1 << collision.gameObject.layer) & canHit) != 0)
        {


            Destroy(this.gameObject);
        }
    }
}
