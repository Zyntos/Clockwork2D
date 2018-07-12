using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {


    public float maxSpeed = 10f;
    bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    public bool gloveHit = false;
    float move;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
	}
	

	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (!gloveHit)
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
        if(grounded && Input.GetButtonDown("Jump") && !gloveHit)
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if (!gloveHit && Input.GetButtonDown("Fire1") && grounded)
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
}
