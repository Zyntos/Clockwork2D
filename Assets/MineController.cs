using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {
    public Animator anim;

    public LayerMask whatIsPlayer;

    public GameObject playerContact;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            playerContact = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            playerContact = collision.gameObject;
            anim.SetBool("explosion", true);
            StartCoroutine(Explosion());
        }

        
    }

    
    

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("explo");
    }

    public void OnDestruction()
    {
        if(playerContact != null)
        {
            playerContact.GetComponent<CharController>().GetDamaged(40);
        }
    }

    public void DestroyMine()
    {
        Destroy(this.gameObject);
    }

}
