﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog_move : MonoBehaviour
{

    public float movePower;
    public float jumpPower;
    Animator animator;
    Vector3 movement;
    int movementFlag = 0;
    bool isTracing;
    GameObject traceTarget;
    Rigidbody2D rb;

    void Awake()
    {
        // DontDestroyOnLoad(transform.gameObject);

        rb = GetComponent<Rigidbody2D>();
        
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isjumping", false);
        else
            animator.SetBool("isjumping", true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else
        {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            rb.velocity = new Vector2(-transform.localScale.x * movePower, jumpPower);
            //transform.localScale = new Vector3(1, 1, 1);

        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            //transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(transform.localScale.x * movePower, jumpPower);
        };

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            traceTarget = other.gameObject;

            StopCoroutine("ChangeMovement");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            isTracing = true;
            animator.SetBool("isjumping", true);


        }


    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            isTracing = false;
            StartCoroutine("ChangeMovement");
        }
    }
}