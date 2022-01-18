﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoplayer_girl : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    public GameObject wall;
    public float speed;
    private float Dist;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider2;
    Animator anim;
    public float jumpPower;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Dist = Vector3.Distance(player.transform.position, wall.transform.position);
        Debug.Log(Dist);
        if (Dist < 1f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            if (speed == 0)
            {
                anim.SetBool("iswalking", false);
            }
            else
                anim.SetBool("iswalking", true);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ending")
        {
            speed = 0;
        }
    }

}

