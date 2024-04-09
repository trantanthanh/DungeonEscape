using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5.0f;
    private bool grounded = false;
    private Rigidbody2D myRigidbody;
    public Vector2 velocityDebug;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down);

        if (hitInfo.collider != null)
        {
            grounded = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            myRigidbody.velocity += new Vector2(0, jumpForce);
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        myRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, myRigidbody.velocity.y);
    }
}
