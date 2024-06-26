using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float timeBetweenJump = 0.2f;
    private float timeNextJump = 0f;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider2D;
    private bool isGrounded = false;
    private bool needResetJump = false;

    [SerializeField] private LayerMask groundLayer;

    PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();

        if (isGrounded)
        {
            playerAnimation.Jump(false);
            CheckJump();
            CheckAttack();
        }
        if (!needResetJump)
        {
            Movement();
        }
    }

    private void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimation.Attack();
        }
    }

    private void CheckJump()
    {
        if (Time.time > timeNextJump)
        {
            needResetJump = false;
            playerAnimation.Jump(false);
        }
        else
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            needResetJump = true;
            playerAnimation.Jump(true);
            timeNextJump = Time.time + timeBetweenJump;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }
    }

    bool IsGrounded()
    {
        float heightOfPlayer = boxCollider2D.size.y + 0.1f;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, heightOfPlayer, groundLayer);
        //Debug.DrawRay(transform.position, Vector2.down * heightOfPlayer, Color.green);

        if (hitInfo.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        myRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, myRigidbody.velocity.y);
        playerAnimation.Moving(Mathf.Abs(myRigidbody.velocity.x));
    }
}
