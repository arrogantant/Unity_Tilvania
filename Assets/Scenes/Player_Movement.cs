using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f,10f);
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    float gravityScaleAtstart;

    bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtstart = myRigidbody.gravityScale;
        myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLaddeer();
        Die();
    }

    void OnMove(InputValue value)
    {   
        if(!isAlive)
        {
            return;
        }
        moveInput =value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed , myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Run", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLaddeer()
    {
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody.gravityScale = gravityScaleAtstart;
            myAnimator.SetBool("Up",false);
            return;
        }
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x , moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Up",playerHasVerticalSpeed);
    }

    void Die()
    {
        if(myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("die");
            myRigidbody.velocity = deathKick;
        }
    }
}
