using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float health;


    private Rigidbody2D rBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = true;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        CheckIfGrounded();
    }

    void HandleInputs()
    {
        float axis = CrossPlatformInputManager.GetAxis("Horizontal");
        if (axis != 0)
        {
            if (axis < 0) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
            rBody.velocity = new Vector2(axis * walkSpeed, rBody.velocity.y);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            rBody.velocity = new Vector2(0, rBody.velocity.y);
            animator.SetBool("IsWalking", false);
        }
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }

        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
                if (touch.position.x > Screen.width / 2)
                    Jump();
        }
    }

    public void Jump()
    {
        if(isGrounded)
            rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
    }


    void CheckIfGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.5f);
        bool foundGround = false;
        if(hits.Length > 0)
        {

            foreach(RaycastHit2D hit in hits)
            {
                if(hit.collider.tag == "Grass")
                {
                    foundGround = true;
                }
            } 
        }

        if(foundGround)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
