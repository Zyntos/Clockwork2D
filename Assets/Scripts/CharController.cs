using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Player Attributes")]
    public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

    //DAMAGE TYPES
    [Header ("DAMAGE TYPES")]
    public float glovedamage = 50;
    public float staffdamage = 20;


    //CHARACTER MOVEMENT VALUES
    [Header("CHARACTER MOVEMENT VALUES")]
    public float maxSpeed = 10f;
    public float jumpForce = 700f;

    //MASTERIES
    [Header("MASTERIES")]
    public bool doublejumpEnabled = false;
    public int maxQuickHits = 3;


    
    //OTHER
    [Header("DEBUGGING")]
    public bool secondjump = false;
    public bool jumped = false;
    bool facingRight = true;
    bool isInvin = false;
    bool lefthit;
    bool righthit;
   

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    
    
    float move;
    public LayerMask whatisEnemy;
    public Material invincibleFrame;
    public Material standardMat;
    public bool evading = false;

    public GameObject glove;
    public GameObject staff;
    bool follow = false;
    public float combocount = 0;
    public float staffcombocount = 0;
    public bool canAttackStaff = true;
    public bool staffCombo = false;
    public bool staffCooldown = true;

    public List<int> buttons;
   
    


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
         


    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //MOVEMENT
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (!isInvin && !evading && !staffCombo)
        {
            move = Input.GetAxis("Horizontal");
        }

        anim.SetFloat("Speed", Mathf.Abs(move));

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
            Flip();


        //EVADE
        if (move != 0)
        {
            if (Input.GetAxis("Vertical") < 0 && anim.GetFloat("vSpeed") == 0)
            {
                anim.SetBool("Evade", true);
                evading = true;

            }
        }


        //Stop EVADE if falling
        if (anim.GetFloat("vSpeed") != 0)
        {
            anim.SetBool("Evade", false);
            evading = false;
        }

        



    }

    private void Update()
    {
        //JUMPING AND DOUBLEJUMPING
        if (Input.GetButtonDown("Jump") && !isInvin && !evading && !staffCombo)
        {
            if (grounded)
            {
                if (doublejumpEnabled)
                {
                    secondjump = true;
                }
                anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            }
            else
            {
                if (secondjump && doublejumpEnabled)
                {
                    secondjump = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                }
            }


        }


        //STAFFCOMBO
        if (Input.GetButtonDown("Fire1") && !staffCombo && canAttackStaff && staffCooldown && grounded && move == 0 )
        {
            staffCooldown = false;
            anim.SetBool("StaffStart", true);
            anim.SetBool("Attack1", true);
            anim.SetBool("Attack2", false);
            canAttackStaff = false;
            buttons.Add(1);

        }
        if(Input.GetButtonDown("Fire1") && staffCombo && canAttackStaff && grounded && move == 0)
        {
            staffcombocount++;
            canAttackStaff = false;
            anim.SetBool("Attack1", true);
            anim.SetBool("Attack2", false);
            buttons.Add(1);
            Debug.Log("SCHLAGEN");

        }
        if (Input.GetButtonDown("Fire2") && staffCombo && canAttackStaff && grounded && move == 0)
        {
            staffcombocount++;
            canAttackStaff = false;
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", true);
            buttons.Add(2);
            Debug.Log("Schießen");

        }










    }

    //FLIP CHARACTER SPRITE
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = GetComponent<Transform>().localScale;
        theScale.x *= -1;
        GetComponent<Transform>().localScale = theScale;
    }




    //GETTING DAMAGED BY RUNNING INTO ENEMY
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (((1 << collision.gameObject.layer) & whatisEnemy) != 0)
        {
            Vector3 dir = (collision.gameObject.transform.position - gameObject.transform.position).normalized;

            if (Mathf.Abs(dir.z) < 0.05f)
            {
                if (dir.x > 0)
                {
                    print("RIGHT");
                    righthit = true;
                }
                else if (dir.x < 0)
                {
                    print("LEFT");
                    lefthit = true;
                }
            }
            else
            {
                if (dir.z > 0)
                {
                    print("FRONT");
                }
                else if (dir.z < 0)
                {
                    print("BACK");
                }
            }

            
            if (!evading)
            {
                anim.SetBool("GetHit", true);
            }

        }


    }


    //START INVINCIBLE FRAME
    private void StartInvin()
    {
        int knockback = 0;

        if (righthit == true)
        {
            knockback = -15;
        }
        else if (lefthit == true)
        {
            knockback = 15;
        }
        GetComponent<SpriteRenderer>().material = invincibleFrame;
        isInvin = true;
        move = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(knockback * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        righthit = false;
        lefthit = false;


    }


    //STOP INVINCIBLE FRAME
    private void StopInvin()
    {
        anim.SetBool("GetHit", false);
        GetComponent<SpriteRenderer>().material = standardMat;
        isInvin = false;

    }

    //STOP EVASION
    private void StopEvade()
    {
        anim.SetBool("Evade", false);
        evading = false;
    }

    //START STAFFCOMBO
    private void StaffStart()
    {
        staffCombo = true;
        canAttackStaff = true;
    }

    //LAST STAFFCOMBO
    private void LastStaffStart()
    {
        staffCombo = true;
        Debug.Log("LASTSTAFFSTART");
    }

    //STOP STAFFCOMBO
    private void StaffStop()
    {
        if(combocount == staffcombocount){
            staffCombo = false;
            anim.SetBool("StaffStart", false);
            anim.SetFloat("StaffCombo", 0);
            combocount = 0;
            staffcombocount = 0;
            canAttackStaff = true;
            StartCoroutine(StaffAttackCooldown());
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", false);

        }
        else
        {

            if(staffcombocount == 1 && anim.GetBool("Attack2") == true)
            {
                staffCombo = false;
                anim.SetBool("StaffStart", false);
                anim.SetFloat("StaffCombo", 0);
                combocount = 0;
                staffcombocount = 0;
                canAttackStaff = true;
                StartCoroutine(StaffAttackCooldown());
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", false);
            }
            else
            {
                anim.SetFloat("StaffCombo", anim.GetFloat("StaffCombo") + 1);
                combocount++;
            }
            
            


        }
        

    }

    IEnumerator StaffAttackCooldown()
    {
        yield return new WaitForSeconds(1);
        staffCooldown = true;
    }

    
}
