using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour {

    public Animator anim;

    public LayerMask whatIsPlayer;

    public GameObject playerContact;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            playerContact = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            playerContact = collision.gameObject;
            anim.SetBool("attack", true);
            anim.SetBool("explosion", true);
            
        }


    }




    

    public void OnHit()
    {
        
        if (playerContact != null)
        {
            playerContact.GetComponent<CharController>().GetDamaged(20);
        }
    }

    public void stopAttack()
    {
        anim.SetBool("explosion", false);
        anim.SetBool("attack", false);
    }

    
}
