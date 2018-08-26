using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMastery : MonoBehaviour
{


    public Mastery mastery;
    public LayerMask whatIsPlayer;
    public bool clicked = false;
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            
            collision.gameObject.GetComponent<CharController>().ShowMastery(mastery);
            if (Input.GetKey(KeyCode.E) && !clicked)
            {
                
                collision.gameObject.GetComponent<CharController>().AddMastery(mastery, this.gameObject);
                
                collision.gameObject.GetComponent<CharController>().hideMastery();
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            
            collision.gameObject.GetComponent<CharController>().hideMastery();


        }

    }
}
