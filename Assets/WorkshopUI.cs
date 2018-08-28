using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopUI : MonoBehaviour
{

    public LayerMask whatIsPlayer;
    public GameObject E;
    public Text text;
    public Text t1;
    public Text t2;
    public Text t3;
    public GameObject Player;
    public GameObject Panel;
    public GameObject gears;
    public bool done2 = false;
    bool clicked = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void done()
    {
        done2 = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            E.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !clicked)
            {

                clicked = true;
                StartCoroutine(MasteryChoose());

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
        {
            E.SetActive(false);
        }
    }

    public IEnumerator MasteryChoose()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        text.gameObject.SetActive(true);
        t1.gameObject.SetActive(true);
        t2.gameObject.SetActive(true);
        t3.gameObject.SetActive(true);

        yield return WaitForInput();

    }

    IEnumerator WaitForInput()
    {
        
        bool done = false;
        while (!done)
        {
            Debug.Log("WAIT1");

            if (Input.GetKey(KeyCode.Alpha1))
            {
                Debug.Log("TEST");
                if (Player.GetComponent<CharController>().enabledMasteries[2] != null)
                {
                    Player.GetComponent<CharController>().enabledMasteries[2].LoseSkill(Player.GetComponent<CharController>());
                    Player.GetComponent<CharController>().enabledMasteries.RemoveAt(2);
                    Player.GetComponent<CharController>().AddGears(10);
                }
                if (Player.GetComponent<CharController>().enabledMasteries[1] != null)
                {
                    Player.GetComponent<CharController>().enabledMasteries[1].LoseSkill(Player.GetComponent<CharController>());
                    Player.GetComponent<CharController>().enabledMasteries.RemoveAt(1);
                    Player.GetComponent<CharController>().AddGears(10);
                }
                done = true;
                Time.timeScale = 1;
                text.gameObject.SetActive(false);
                t1.gameObject.SetActive(false);
                t2.gameObject.SetActive(false);
                t3.gameObject.SetActive(false);




            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                Debug.Log("TEST");
                if (Player.GetComponent<CharController>().enabledMasteries[2] != null)
                {
                    Player.GetComponent<CharController>().enabledMasteries[2].LoseSkill(Player.GetComponent<CharController>());
                    Player.GetComponent<CharController>().enabledMasteries.RemoveAt(2);
                    Player.GetComponent<CharController>().AddGears(10);
                }
                if (Player.GetComponent<CharController>().enabledMasteries[0] != null)
                {
                    Player.GetComponent<CharController>().enabledMasteries[0].LoseSkill(Player.GetComponent<CharController>());
                    Player.GetComponent<CharController>().enabledMasteries.RemoveAt(0);
                    Player.GetComponent<CharController>().AddGears(10);
                }
                done = true;
                Time.timeScale = 1;
                text.gameObject.SetActive(false);
                t1.gameObject.SetActive(false);
                t2.gameObject.SetActive(false);
                t3.gameObject.SetActive(false);





            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                Debug.Log("TEST");
                if (Player.GetComponent<CharController>().enabledMasteries[1] != null)
                {
                    Player.GetComponent<CharController>().enabledMasteries[1].LoseSkill(Player.GetComponent<CharController>());
                    Player.GetComponent<CharController>().enabledMasteries.RemoveAt(1);
                    Player.GetComponent<CharController>().AddGears(10);
                }
                if (Player.GetComponent<CharController>().enabledMasteries[0] != null)
                {
                    Player.GetComponent<CharController>().enabledMasteries[0].LoseSkill(Player.GetComponent<CharController>());
                    Player.GetComponent<CharController>().enabledMasteries.RemoveAt(0);
                    Player.GetComponent<CharController>().AddGears(10);
                }
                done = true;
                Time.timeScale = 1;
                text.gameObject.SetActive(false);
                t1.gameObject.SetActive(false);
                t2.gameObject.SetActive(false);
                t3.gameObject.SetActive(false);
                



            }

            yield return null;

        }
        yield return WaitForInput2();
        
    }

    IEnumerator WaitForInput2()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);
        gears.SetActive(true);
        
        while (!done2)
        {
            gears.GetComponentInChildren<Text>().text = Player.GetComponent<CharController>().gearValue.ToString();
            yield return null;
            
        }

        Time.timeScale = 1;
        Panel.SetActive(false);
        gears.SetActive(false);
        



       
    }


}

