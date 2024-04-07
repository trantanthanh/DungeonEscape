using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
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
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        myRigidbody.velocity = new Vector2(horizontalInput, 0) * moveSpeed;
    }
}
