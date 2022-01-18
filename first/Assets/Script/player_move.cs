using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
   
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider2;
    Animator anim;
    AudioSource audioSource;
    
    public Vector3 checkpoint;

    public float maxSpeed;
    public float jumpPower;

    public GameObject effect;
    public GameObject notfinish;
    public GameObject finish;
    public GameObject openbox;
    public GameObject doorkey;
    public GameObject notice;

    public static bool canMove = true;

   
    void Awake()
    {
       
      // DontDestroyOnLoad(transform.gameObject);
       
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
    }

    void Update()
    {
        //Jump
        if (Input.GetButton("Jump") && !anim.GetBool("isjumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isjumping", true);
            PlaySound("JUMP");
            audioSource.Play();
        }

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;


        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("iswalking", false);
        else
            anim.SetBool("iswalking", true);

        if (Input.GetKeyDown(KeyCode.Return) && GameObject.FindGameObjectWithTag("notice"))
        {
            notice.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            notice.gameObject.SetActive(false);
        }


    }
    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 3, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //Right MaxSpeed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //Left MaxSpeed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down * 10, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 3f)
                    anim.SetBool("isjumping", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            //Point
            bool isAcorn = collision.gameObject.name.Contains("acorn");

            if (isAcorn)
            {
                gameManager.stagePoint += 50;
            }
            //Deactive Item
            collision.gameObject.SetActive(false);

            //Sound
            PlaySound("ITEM");
            audioSource.Play();
        }

        if (collision.gameObject.tag == "strong")
        {
            //Point
            bool isJam = collision.gameObject.name.Contains("jam");

            if (isJam)
                gameManager.stagePoint += 500;

            //Deactive Item
            collision.gameObject.SetActive(false);

            //Change Layer (Immortal Active)
            gameObject.layer = 11;

            maxSpeed = 10;
            jumpPower = 35;

            //Animation
            //anim.SetTrigger("dostrong");
            GameObject powerup = Instantiate(effect, 
                new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                Quaternion.identity) as GameObject;
            powerup.transform.parent = GameObject.Find("Player").transform;
            Destroy(powerup, 4f);


            Invoke("offDamaged", 4);

            //Sound
            PlaySound("ITEM");
            audioSource.Play();
        }
        
        if(collision.gameObject.tag == "treasurebox")
        {
            collision.gameObject.SetActive(false);

            openbox.gameObject.SetActive(true);
            doorkey.gameObject.SetActive(true);
        }

        if(collision.gameObject.tag == "key")
        {
            bool isKey = collision.gameObject.name.Contains("key");

            if (isKey)
                gameManager.stagePoint += 1000;

            //Deactive Item
            collision.gameObject.SetActive(false);

            notfinish.gameObject.SetActive(false);
            finish.gameObject.SetActive(true);
        }

        if (collision.gameObject.tag == "FallDetector")
        {
            transform.position = checkpoint;
            if (gameManager.health > 0)
                gameManager.HealthDown();

        }

        // 체크포인트 표지판
        if (collision.gameObject.tag == "checkpoint")
        {
            checkpoint = collision.transform.position;           
        }

        
        if (collision.transform.CompareTag("Monster") || collision.transform.CompareTag("Projectile"))
            {
                if (collision.transform.CompareTag("Projectile"))
            {
                Destroy(collision.gameObject, 0.02f);
                onDamaged(collision.transform.position);
            }
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
                
            }
            else //Damaged
                onDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag == "fly" || collision.gameObject.tag == "onlyfly")
        {
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack2(collision.transform);
            }
            else //Damaged
                onDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag == "trackfly")
        {
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack3(collision.transform);
            }
            else //Damaged
                onDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag == "Monster")
        {
           if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
                {
                   OnAttack4(collision.transform);
                }
             else //Damaged
                onDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag == "checkmonster")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack5(collision.transform);
            }
            else //Damaged
                onDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag == "Monsterupgrade")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack6(collision.transform);
            }
            else //Damaged
                onDamaged(collision.transform.position);
        }
    }

    void OnAttack(Transform enemy)
    {
        //Point
        gameManager.stagePoint += 100;

        //Reaction Force
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        //Enemy Die
        fox_move enemyMove = enemy.GetComponent<fox_move>();
        enemyMove.OnDamaged();

        //Sound
        PlaySound("ATTACK");
        audioSource.Play();
    }

    void OnAttack2(Transform enemy2)
    {
        //Point
        gameManager.stagePoint += 150;

        //Reaction Force
        rigid.AddForce(Vector2.up * 33, ForceMode2D.Impulse);

        //Enemy Die
        eagle_move enemyMove2 = enemy2.GetComponent<eagle_move>();
        enemyMove2.OnDamaged();

        //Sound
        PlaySound("ATTACK");
        audioSource.Play();
    }

    void OnAttack3(Transform enemy3)
    {
        //Point
        gameManager.stagePoint += 300;

        //Reaction Force
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        //Enemy Die
        eagle_track enemyMove3 = enemy3.GetComponent<eagle_track>();
        enemyMove3.OnDamaged();

        //Sound
        PlaySound("ATTACK");
        audioSource.Play();
    }

    void OnAttack4(Transform trunk)
    {
        //Point
        gameManager.stagePoint += 250;

        //Reaction Force
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        //Enemy Die
        Trunk trunkmove = trunk.GetComponent<Trunk>();
        trunkmove.OnDamaged();

        //Sound
        PlaySound("ATTACK");
        audioSource.Play();
    }

    void OnAttack5(Transform fox)
    {
        //Point
        gameManager.stagePoint += 250;

        //Reaction Force
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        //Enemy Die
        Mushroom foxmove = fox.GetComponent<Mushroom>();
        foxmove.OnDamaged();

        //Sound
        PlaySound("ATTACK");
        audioSource.Play();
    }

    void OnAttack6(Transform foxupgrade)
    {
        //Point
        gameManager.stagePoint += 400;

        //Reaction Force
        rigid.AddForce(Vector2.up * 30, ForceMode2D.Impulse);

        //Enemy Die
        Monster monster = foxupgrade.GetComponent<Monster>();
        monster.OnDamaged();

        //Sound
        PlaySound("ATTACK");
        audioSource.Play();
    }


    void onDamaged(Vector2 targetPos)
    {
        //Health Down
        gameManager.HealthDown();

        //Change Layer (Immortal Active)
        gameObject.layer = 11;

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force 적에 맞으면 튕겨나가기
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 2, ForceMode2D.Impulse);

        //Animation
        anim.SetTrigger("dodamaged");

        Invoke("offDamaged", 2);

        //Sound
        PlaySound("DAMAGED");
        audioSource.Play();

    }

    void offDamaged()
    {
        maxSpeed = 6;
        jumpPower = 24;
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite FlipY
        spriteRenderer.flipY = true;
        //Collider Disable
        collider2.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //Sound
        PlaySound("DIE");
        audioSource.Play();

    }


    void powerup()
    {
        maxSpeed = 6;
        jumpPower = 24;
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
