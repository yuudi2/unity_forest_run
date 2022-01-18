using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class eagle_track : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    SpriteRenderer spriteRenderer;

    Animator anim;

    [Header("추격 속도")]
    [SerializeField] [Range(1f, 4f)] float moveSpeed = 3f;

    [Header("근접 거리")]
    [SerializeField] [Range(0f, 3f)] float contactDistance = 1f;

    bool follow = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        Move();
    }

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else
            rb.velocity = Vector2.zero;
    }

    private void Move()
    {
        if (target.transform.position.x > transform.position.x) 
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);
        else
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            follow = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        follow = false;
    }

    public void OnDamaged()
    {
        //Collider Disable
        anim.SetTrigger("eagledead");
        //Destroy
        Invoke("DeActive", 0.3f);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
