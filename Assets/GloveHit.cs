using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveHit : MonoBehaviour {

    GameObject collisionObj;
    public bool canHit;
    public LayerMask hittable;
    public Transform glove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        canHit = Physics2D.OverlapCircle(glove.position, glove.GetComponent<CircleCollider2D>().radius*2, hittable);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collisionObj = collision.gameObject;
    }
}
