using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRunController : MonoBehaviour {

    public LayerMask whatIsPlayer;
    public Animator anim;
    public GameObject player;
    public GameObject parent;
    
    public float MoveSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        
    }

    private void FixedUpdate()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0 && anim.GetBool("explosion") != true && anim.GetBool("attack") != true)
        {
            anim.SetTrigger("ausbuddeln");
            anim.SetBool("run", true);
            player = collision.gameObject;
            if(anim.GetBool("run") == true)
            {
                
                parent.transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, MoveSpeed);
                Debug.Log(transform.position.x);
                Debug.Log(collision.transform.position.x);
                if (transform.position.x < collision.transform.position.x)
                {
                    parent.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    parent.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            anim.SetBool("run", false);
        }
    }
}
