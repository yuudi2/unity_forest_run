using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trunk_move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider2;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2 = GetComponent<CapsuleCollider2D>();
        Think();

        Invoke("Think", 5); //주어진 시간이 지난 뒤, 지정된 함수를 실행하는 함수
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down * 10, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    //재귀 함수 (같은 함수를 불렀을 때 - 딜레이를 줘야함)
    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        anim.SetInteger("walkspeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == -1;
        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        //Collider Disable
        anim.SetTrigger("trunkdead");
        //Destroy
        Invoke("DeActive", 0.4f);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}