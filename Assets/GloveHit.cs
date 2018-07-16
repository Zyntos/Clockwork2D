using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveHit : MonoBehaviour {

    public List<GameObject> collisionObj;
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

    }


    //LOAD ENEMIES INTO COLLISIONLIST
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & hittable) != 0)
            if (!collisionObj.Contains(collision.gameObject))
            {
                collisionObj.Add(collision.gameObject);
            }
            
    }

    //REMOVE ENEMIES FROM COLLISIONLIST
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collisionObj.Contains(collision.gameObject))
        {
            collisionObj.Remove(collision.gameObject);
        }
    }


}
