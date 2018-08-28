using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsDropController : MonoBehaviour {

    public GameObject gear;
    public List<GameObject> mastery1;
    public List<GameObject> mastery2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropGears()
    {

        
        GameObject gears = Instantiate(gear, this.gameObject.transform.position, Quaternion.identity);
        gears.GetComponent<GearValue>().value = Random.Range(1, 6);
        
    }


}
