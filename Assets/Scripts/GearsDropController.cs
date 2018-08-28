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
        int masteryDrop = Random.Range(1, 11);
        if(masteryDrop > 8)
        {
            int masteryChoose = Random.Range(1, mastery1.Count + 1);
            GameObject mastery = Instantiate(mastery2[masteryChoose-1], this.gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            masteryDrop = Random.Range(1, 11);
            if(masteryDrop > 6)
            {
                int masteryChoose = Random.Range(1, mastery1.Count + 1);
                GameObject mastery = Instantiate(mastery1[masteryChoose-1], this.gameObject.transform.position, Quaternion.identity);

            }
        } 
    }


}
