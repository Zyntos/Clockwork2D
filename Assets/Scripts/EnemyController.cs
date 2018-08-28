using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("ENEMY VALUES")]
    public float maxlife = 100;
    public float life;
    public string enemyType;
    public Animator anim;

    public Material onHit;
    public Material norm;
    public bool dead = false;
    public bool destroyed = false;
    public List<GameObject> masteryList;

	// Use this for initialization
	void Start () {
        life = maxlife;
	}
	
	// Update is called once per frame
	void Update () {
		
        //DESTROY ENEMY
        if(life <= 0 && !dead)
        {
           
            Instantiate(masteryList[1], this.gameObject.transform.position, Quaternion.identity);
            this.gameObject.GetComponent<GearsDropController>().DropGears();
            

            Destroy(this.gameObject);
        }
	}

    //ENEMY GET DAMAGED
    public void getDamaged(float dmg)
    {
        
        life -= dmg;
        GetComponent<SpriteRenderer>().material = onHit;
        StartCoroutine(MatChange());
    }

    IEnumerator MatChange()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().material = norm;
    }

    public void DestroyEnemyMine()
    {
        if (!destroyed)
        {
            destroyed = true;
            
        }
        
    }

    public void RemoveTrigger()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

}
