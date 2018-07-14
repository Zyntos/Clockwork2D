using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{


    public float maxSpeed = 10f;
    bool facingRight = true;
    bool isInvin = false;
    bool lefthit;
    bool righthit;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    public bool gloveHit = false;
    float move;
    public LayerMask whatisEnemy;
    public Material invincibleFrame;
    public Material standardMat;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (!gloveHit && !isInvin)
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



    }

    private void Update()
    {
        if (grounded && Input.GetButtonDown("Jump") && !gloveHit && !isInvin)
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (!gloveHit && Input.GetButtonDown("Fire1") && grounded && !isInvin)
        {
            move = 0;
            gloveHit = true;
            anim.SetBool("GloveHit", true);

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = GetComponent<Transform>().localScale;
        theScale.x *= -1;
        GetComponent<Transform>().localScale = theScale;
    }

    void AnimEnd()
    {
        gloveHit = false;
        anim.SetBool("GloveHit", false);
        Debug.Log("STOP");
    }


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

            //DOSomething
            anim.SetBool("GetHit", true);

          
        }

        
    }

    private void StartInvin()
    {
        int knockback = 0;

        if (righthit == true)
        {
            knockback = -15;
        }
        else if(lefthit == true)
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

    private void StopInvin()
    {
        anim.SetBool("GetHit", false);
        GetComponent<SpriteRenderer>().material = standardMat;
        isInvin = false;
        
    }

}
