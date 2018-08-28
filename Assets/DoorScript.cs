using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level;

public class DoorScript : MonoBehaviour {

    public LayerMask whatisplayer;
    public GameManager gm;
    public GameObject player;
    public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatisplayer) != 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                player.GetComponent<CharController>().SpawnAt(LevelManager.Instance.StartRoom.SpawnPosition);
            }
        }
    }
}
