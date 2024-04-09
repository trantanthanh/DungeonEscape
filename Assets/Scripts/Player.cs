using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float timeBetweenJump = 0.1f;
    private float timeNextJump = 0f;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider2D;
    private Animator myAnimator;

    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckJump();
        CheckFlipSprite();
    }

    void CheckFlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1.0f);
        }
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > timeNextJump && IsGrounded())
        {
            timeNextJump = Time.time + timeBetweenJump;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }
    }

    bool IsGrounded()
    {
        float heightOfPlayer = boxCollider2D.size.y * 2/3 + 0.1f;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, heightOfPlayer, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * heightOfPlayer, Color.green);

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
    }
}
