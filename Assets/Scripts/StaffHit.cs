using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffHit : MonoBehaviour {

    public List<GameObject> collisionObj;
    public LayerMask hittable;
    public Transform staff;

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
