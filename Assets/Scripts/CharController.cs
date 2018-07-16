using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{


    //DAMAGE TYPES
    [Header ("DAMAGE TYPES")]
    public float glovedamage = 50;


    //CHARACTER MOVEMENT VALUES
    [Header("CHARACTER MOVEMENT VALUES")]
    public float maxSpeed = 10f;
    public float jumpForce = 700f;

    //MASTERIES
    [Header("MASTERIES")]
    public bool doublejumpEnabled = false;


    
    //OTHER
    [Header("DEBUGGING")]
    public bool secondjump = false;
    public bool jumped = false;
    bool facingRight = true;
    bool isInvin = false;
    bool lefthit;
    bool righthit;
    bool quickHit;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    
    public bool gloveHit = false;
    float move;
    public LayerMask whatisEnemy;
    public Material invincibleFrame;
    public Material standardMat;
    public bool evading = false;

    public GameObject glove;


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

        if (!gloveHit && !isInvin && !evading)
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
        if (Input.GetButtonDown("Jump") && !gloveHit && !isInvin && !evading)
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


        //GLOVEATTACK
        if (!gloveHit && Input.GetButtonDown("Fire1") && grounded && !isInvin && !evading)
        {
            if (facingRight && Input.GetAxis("Horizontal") < 0)
            {
                Flip();
            }
            if (!facingRight && Input.GetAxis("Horizontal") > 0)
            {
                Flip();
            }
            Debug.Log("GLOVE");
            gloveHit = true;
            anim.SetBool("GloveHit", true);
            move = 0;


        }

        //QUICKATTACK
        if (!gloveHit && !quickHit && Input.GetButtonDown("Fire2") && grounded && !isInvin && !evading)
        {
            if (facingRight && Input.GetAxis("Horizontal") < 0)
            {
                Flip();
            }
            if (!facingRight && Input.GetAxis("Horizontal") > 0)
            {
                Flip();
            }
            Debug.Log("QUICK");
            quickHit = true;
            anim.SetBool("QuickHit", true);
            move = 0;
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


    //GLOVEHIT ANIMATION END
    void AnimEnd()
    {
        gloveHit = false;
        anim.SetBool("GloveHit", false);
        Debug.Log("STOP");
    }

    //QUICKHIT ANIMATION END
    void QuickEnd()
    {
        quickHit = false;
        anim.SetBool("QuickHit", false);
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


    //DAMAGE ENEMY WITH GLOVE
    private void OnGloveHit()
    {
        foreach (GameObject hittable in glove.GetComponent<GloveHit>().collisionObj)
        {
            hittable.GetComponent<EnemyController>().getDamaged(glovedamage);
        }
    }



}
