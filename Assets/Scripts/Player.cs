using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float timeBetweenJump = 0.1f;
    private float timeNextJump = 0f;
    private bool grounded = false;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckJump();
    }

    private void CheckJump()
    {
        float heightOfPlayer = boxCollider2D.size.y / 2 + 0.1f;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, heightOfPlayer, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * heightOfPlayer, Color.green);

        if (hitInfo.collider != null)
        {
            //Debug.Log($"hit : {hitInfo.collider.name}");
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space) && Time.time > timeNextJump)
        {
            timeNextJump = Time.time + timeBetweenJump;
            myRigidbody.velocity += new Vector2(0, jumpForce);
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        myRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, myRigidbody.velocity.y);
    }
}
