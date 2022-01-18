using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle_move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider2;

    Vector3 pos; //현재위치
    float delta = 2.0f; //상하로 이동가능한 (y)최댓값
    float speed = 3.0f; //이동 속도

    void Start()
    {
        pos = transform.position;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {                                
        if (gameObject.tag == "fly") {
            if (pos.y >= 10)
            {
                Vector3 v = pos;
                v.y += delta * Mathf.Sin(Time.time * speed);
                // 좌우 이동의 최대치 및 반전 처리
                transform.position = v;
            }
            else
            {
                Vector3 v = pos;
                v.y += delta * Mathf.Sin(Time.time * speed) * -1;
                // 좌우 이동의 최대치 및 반전 처리
                transform.position = v;
            }
        }
        else
        {
            Vector3 v = pos;
            transform.position = v;
        }
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
