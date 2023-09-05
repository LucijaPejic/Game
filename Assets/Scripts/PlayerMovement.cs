using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private Transform sprite;
    private float dirX = 0f;
    private enum MovementState {idle, walk, jump, fall }
    [SerializeField]private LayerMask jumpableGround;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 9f;
    [SerializeField]private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {   
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.walk;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (dirX < 0f)
        {
            state = MovementState.walk;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f) 
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
