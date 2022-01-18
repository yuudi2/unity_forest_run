
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand_Trunk : Monster
{
    public enum State
    {
        Idle,
        Run,
        Attack,
    };
    public State currentState = State.Idle;

    public Transform[] wallCheck;
    public Transform genPoint;
    public GameObject Bullet;
    public float DestroyTime = 2.0f;
    public float DestroyNow = 0f;

    Animator anim;

    Transform target;

    WaitForSeconds Delay1000 = new WaitForSeconds(1f);

    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Awake()
    {
        base.Awake();
        moveSpeed = 0f;
        jumpPower = 0f;
        currentHp = 1;
        atkCoolTime = 2f;
        atkCoolTimeCalc = atkCoolTime;

        StartCoroutine(FSM());
    }



    IEnumerator FSM ()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    IEnumerator Idle()
    {
        yield return null;
        MyAnimSetTrigger("Idle");

        if ( Random.value > 0.5f)
       {
            MonsterFlip();
        }
        yield return Delay1000;
        currentState = State.Run;
    }
    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 3f);
        while ( runTime >= 0f)
        {
            runTime -= Time.deltaTime;
            MyAnimSetTrigger("Idle");
            if( !isHit)
            {
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

                if( Physics2D.OverlapCircle ( wallCheck[1].position, 0.01f, layerMask ))
                {
                    MonsterFlip();
                }
                if ( canAtk && IsPlayerDir ( ))
                {
                    if (Vector2.Distance ( transform.position, PlayerData.Instance.Player.transform.position ) < 15f)
                    {
                        currentState = State.Attack;
                        break;
                    }
                }
            }
            yield return null;
        }
        if (currentState != State.Attack)
        {
            if (Random.value > 0.5f)
            {
                MonsterFlip();
            }
            else
            {
                currentState = State.Idle;
            }
        }
    }
    IEnumerator Attack()
    {
        yield return null;

        canAtk = false;
        //rb.velocity = new Vector2(0, jumpPower);
        MyAnimSetTrigger("Attack");

        yield return Delay1000;
        currentState = State.Run;
    }

    void Fire()
    {

        GameObject bulletClone = Instantiate(Bullet, genPoint.position, transform.rotation);
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
        bulletClone.transform.localScale = new Vector2(transform.localScale.x, 1f);
        Destroy(bulletClone, DestroyTime);

        if (bulletClone.CompareTag("Player"))
        {
            Destroy(bulletClone, DestroyNow);
        }
        
    }

    public void OnDamaged()
    {
        //Collider Disable
        anim.SetTrigger("Hit");
        //Destroy
        Invoke("DeActive", 0.3f);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }


}
