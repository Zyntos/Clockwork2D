using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharController : MonoBehaviour
{
    [Header("Player Attributes")]
    public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();
    public List<Skills> enabledSkills = new List<Skills>();

    [Header("Player Masteries")]
    public List<PlayerMasterys> Masteries = new List<PlayerMasterys>();
    public List<Mastery> enabledMasteries = new List<Mastery>();

    //DAMAGE TYPES
    [Header ("DAMAGE TYPES")]
    public float glovedamage;
    public float staffdamage;
    public float shootdamage;


    //CHARACTER MOVEMENT VALUES
    [Header("CHARACTER MOVEMENT VALUES")]
    public float maxSpeed = 10f;
    public float jumpForce = 700f;

    //MASTERIES
    [Header("MASTERIES")]
    public bool doublejumpEnabled = false;
    public bool gearheal = false;
    public bool moregears = false;
    public bool focus = false;
    public bool staffheal = false;
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
   
    public float combocount = 0;
    public float staffcombocount = 0;
    public bool canAttackStaff = true;
    public bool staffCombo = false;
    public bool staffCooldown = true;
    

    public List<int> buttons;

    public bool stafffirst = false;
    public bool hitfirst = false;

    //TEST
    public List<int> buttonpresses;
    public bool start = false;
    public bool end = false;
    public string buttonList = "";
    public int buttonCount = 0;

    public List<int> combos = new List<int>{ 1, 11, 111, 2, 22, 222, 2222 };
    public int parsing = 0;
    public bool pressed = false;
    public float distance = 1.0f;
    public LayerMask whatisLadder;
    public bool isClimbing = false;
    public float inputVertical;

    public GameObject bullet;
    public GameObject bulletStart;
    public Quaternion rot;

    public LayerMask whatisPlatform;
    private PlatformEffector2D platform;
    public GameObject platformGo;
    private bool canFall = false;
    public Image lifeBar;

    public GameObject gearIcon;
    public Text gearText;
    public Camera GameCamera;

    public Image m1;
    public Image m2;
    public Image m3;

    public Text mtext;
    public Text mtext1;
    public Text mtext2;
    public Text mtext3;
    public GameObject e;

    public Image masteryPreview;
    public Image masteryPicturePreview;
    public Text masteryDescription;

    public GameObject goTemp;
    

    [Header("PlayerValues")]
    public int gearValue;
    public float maxlife;
    public float life;


    [Header("SKILLTESTING DEBUG")]
    public Skills TestSkill;
    





    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        



    }

    private void Start()
    {
        maxlife = 100;
        life = maxlife;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //MOVEMENT
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (!isInvin && !evading && anim.GetInteger("currentstate") == 0)
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
        //Update Skillvalues

        for (int i = 0; i < Attributes.Count; i++)
        {
            if (Attributes[i].attribute.name == "GloveDamage")
            {
                glovedamage = Attributes[i].amount;
            }
            if (Attributes[i].attribute.name == "StaffDamage")
            {
                staffdamage = Attributes[i].amount;
            }
        }

        //Update Masteries
     
        if(enabledMasteries[0] != null)
        {
            m1.sprite = enabledMasteries[0].Icon;
            m1.gameObject.SetActive(true);
        }
        if (enabledMasteries[1] != null)
        {
            m2.sprite = enabledMasteries[1].Icon;
            m2.gameObject.SetActive(true);
        }
        if (enabledMasteries[2] != null)
        {
            m3.sprite = enabledMasteries[2].Icon;
            m3.gameObject.SetActive(true);
        }






        //JUMPING AND DOUBLEJUMPING
        if (Input.GetButtonDown("Jump") && !isInvin && !evading && anim.GetInteger("currentstate") == 0)
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

      




        //HandleAttacks
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(1);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Attack(2);
        }





        if (Input.GetKeyDown(KeyCode.S) && canFall == true)
        {
            StartCoroutine(PlatformHandler());
        }


        lifeBar.fillAmount = life / 100;
        gearText.text = gearValue.ToString();









    }

    private void Attack(int button)
    {
        
        
        if (buttonpresses.Count == 0 && staffCooldown && move == 0 && grounded)
        {
            
            Debug.Log("FirstHit");
            staffCooldown = false;
           
            buttonpresses.Add(button);
            buttonList += button;
            anim.SetInteger("currentstate", int.Parse(buttonList));
            
            buttonCount = buttonpresses.Count;

        }
        else if (start && !pressed && move == 0 && grounded)
        {
            
            Debug.Log("Combo");
           
            buttonList += button;
            int.TryParse(buttonList, out parsing);
            if (combos.Contains(parsing))
            {
                
                pressed = true;
                buttonpresses.Add(button);
                anim.SetInteger("currentstate", int.Parse(buttonList));
                buttonCount = buttonpresses.Count;
            }
            
            
            
        }

        if (button == 2 && !grounded)
        {
            anim.SetTrigger("JumpAttack");
        }
        




    }

    void ClearButtons()
    {
        
        buttonCount = 0;
        buttonpresses.Clear();
        buttonList = "";
        StartCoroutine(StaffAttackCooldown());
        pressed = false;
        anim.SetInteger("currentstate", 0);


    }

    void ClearButtonsOnStop()
    {
        buttonCount--;
        if (pressed == false)
        {
            buttonpresses.Clear();
            buttonList = "";
            
            buttonCount = 0;
            StartCoroutine(StaffAttackCooldown());
            Debug.Log("CLEAR");
            anim.SetInteger("currentstate", 0);

        }
       
        




    }

    public void StartCountdown(float cd)
    {
        StartCoroutine(Countdown(cd));
    }

    IEnumerator Countdown(float cd)
    {
        yield return new WaitForSeconds(cd - 0.1f);
        start = false;
        pressed = false;
        

    }





    //FLIP CHARACTER SPRITE
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = GetComponent<Transform>().localScale;
        theScale.x *= -1;
        GetComponent<Transform>().localScale = theScale;
       
        
    }





    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatisPlatform) != 0)
        {
            platformGo = collision.gameObject;
            canFall = true;
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatisPlatform) != 0)
        {
            
            canFall = false;

        }
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
            knockback = -4;
        }
        else if (lefthit == true)
        {
            knockback = 4;
        }
        GetComponent<SpriteRenderer>().material = invincibleFrame;
        isInvin = true;
        move = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(knockback * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        righthit = false;
        lefthit = false;
        GetDamaged(10);

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

    

    IEnumerator StaffAttackCooldown()
    {
        yield return new WaitForSeconds(0.8f);
        staffCooldown = true;
        anim.SetBool("Attack", false);
        Debug.Log("COOLDOWN");
        buttonpresses.Clear();
        buttonList = "";
        anim.SetInteger("currentstate", 0);
        buttonCount = 0;
       
    }

    void GloveDamaged()
    {
        float gloved = glovedamage;
        if (focus)
        {
            gloved *= 2; 
        }
        foreach( GameObject enemy in glove.GetComponent<GloveHit>().collisionObj)
        {


            
            enemy.GetComponent<EnemyController>().getDamaged(gloved);
            GameCamera.GetComponent<CameraControl>().Shake(0.15f, 3, 7);
        }
    }

    void StaffDamaged()
    {
        float staffd = staffdamage;
        if (focus)
        {
            staffd *= 2;
        }
        if (staffheal)
        {
            life += 2;
        }
        foreach (GameObject enemy in staff.GetComponent<StaffHit>().collisionObj)
        {
            enemy.GetComponent<EnemyController>().getDamaged(staffd);
            GameCamera.GetComponent<CameraControl>().Shake(0.15f, 3, 7);
        }
    }

    void Shoot()
    {
        float shootd = shootdamage;
        if (focus)
        {
            shootd *= 2;
        }

        GameObject bul = Instantiate(bullet, bulletStart.transform.position, Quaternion.identity);
        bul.GetComponent<BulletMove>().bulletDamage = shootd;
        if (!facingRight)
        {
            bul.GetComponent<BulletMove>().maxSpeed = -3;

        }
        GameCamera.GetComponent<CameraControl>().Shake(0.15f, 3, 7);
    }

    IEnumerator PlatformHandler()
    {
        platform = platformGo.transform.GetComponent<PlatformEffector2D>();
        platform.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.3f);
        platform.rotationalOffset = 0;
    }

    public void AddSkill()
    {
        enabledSkills.Add(TestSkill);
        TestSkill.GetSkill(this);
    }

    public void AddMastery(Mastery mastery, GameObject go)
    {

        goTemp = go;
        if (enabledMasteries[0] == null)
        {
            enabledMasteries[0] = mastery;
            mastery.GetSkill(this);
            Destroy(goTemp.transform.parent.gameObject);
        }
        else if (enabledMasteries[1] == null)

        {
            enabledMasteries[1] = mastery;
            mastery.GetSkill(this);
            Destroy(goTemp.transform.parent.gameObject);
        }
        else if (enabledMasteries[2] == null)
        {
            enabledMasteries[2] = mastery;
            mastery.GetSkill(this);
            Destroy(goTemp.transform.parent.gameObject);
        }
        else
        {
            go.GetComponent<AddMastery>().clicked = true;
            StartCoroutine(WaitIE(mastery));
        }
           

        
        

       
    }

    IEnumerator WaitIE(Mastery mastery)
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        mtext.gameObject.SetActive(true);
        mtext1.gameObject.SetActive(true);
        mtext2.gameObject.SetActive(true);
        mtext3.gameObject.SetActive(true);


        yield return WaitForInput(mastery);
    }
    IEnumerator WaitForInput(Mastery mastery)
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                enabledMasteries[0].LoseSkill(this);
                enabledMasteries[0] = mastery;
                done = true; // breaks the loop
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                enabledMasteries[1].LoseSkill(this);
                enabledMasteries[1] = mastery;
                done = true;
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                enabledMasteries[2].LoseSkill(this);
                enabledMasteries[2] = mastery;
                done = true;
            }


            yield return null;

        }

        Time.timeScale = 1;
        mtext.gameObject.SetActive(false);
        mtext1.gameObject.SetActive(false);
        mtext2.gameObject.SetActive(false);
        mtext3.gameObject.SetActive(false);

        mastery.GetSkill(this);
        Destroy(goTemp.transform.parent.gameObject);
        goTemp = null;
    }
    public void GetDamaged(int value)
    {
        if (focus)
        {
            value *= 2;
        }
        life -= value;
    }

    public void AddGears(int value)
    {
        if (moregears)
        {
            value *= 2;
        }
        gearValue += value;
        StartCoroutine(ShowGears());
        if (gearheal)
        {
            life += 10;
        }
    }

    public void JumpAttackStart()
    {
        anim.SetBool("JumpAttacking", true);
    }

    public void JumpAttackStop()
    {
        foreach (GameObject enemy in staff.GetComponent<StaffHit>().collisionObj)
        {
            enemy.GetComponent<EnemyController>().getDamaged(staffdamage);
            GameCamera.GetComponent<CameraControl>().Shake(0.15f, 3, 7);
        }
        anim.SetBool("JumpAttacking", false);
        
    }

    public void ShowMastery(Mastery mastery)
    {
        masteryPreview.gameObject.SetActive(true);
        masteryPicturePreview.sprite = mastery.Icon;
        masteryDescription.text = mastery.Description;
    }

    public void hideMastery()
    {
        masteryPreview.gameObject.SetActive(false);
    }

    IEnumerator ShowGears()
    {
        gearIcon.SetActive(true);
        yield return new WaitForSeconds(3f);
        gearIcon.SetActive(false);
    }



    

}
