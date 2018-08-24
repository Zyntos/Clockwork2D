using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMastery : MonoBehaviour
{


    public Mastery mastery;
    public LayerMask whatIsPlayer;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.gameObject.GetComponent<CharController>().AddMastery(mastery, this.gameObject);
                
            }


        }
    }
}
