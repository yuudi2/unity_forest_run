using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Monster
{
    public Animator anim;
    public Transform[] wallCheck;

    private void Awake ( )
    {
        base.Awake ( );
        moveSpeed = 3f;
        jumpPower = 15f;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ( )
    {
        if ( !isHit )
        {
            rb.velocity = new Vector2 ( -transform.localScale.x * moveSpeed, rb.velocity.y );

            if ( !Physics2D.OverlapCircle ( wallCheck[0].position, 0.01f, layerMask ) &&
                Physics2D.OverlapCircle ( wallCheck[1].position, 0.01f, layerMask ) &&
                 !Physics2D.Raycast ( transform.position, -transform.localScale.x * transform.right, 1f, layerMask ) )
            {
                rb.velocity = new Vector2 ( rb.velocity.x, jumpPower );
            }
            else if ( Physics2D.OverlapCircle ( wallCheck[1].position, 0.01f, layerMask ) )
            {
                MonsterFlip ( );
            }
        }

    }
    protected void OnTriggerEnter2D ( Collider2D collision )
    {
        base.OnTriggerEnter2D ( collision );
        if ( collision.transform.CompareTag ( "PlayerHitBox" ) )
        {
            MonsterFlip ( );
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
