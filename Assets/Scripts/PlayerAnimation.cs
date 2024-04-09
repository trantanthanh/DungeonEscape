using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator[] myAnimator;
    private Animator playerAnim;
    private Animator effectAnim;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentsInChildren<Animator>();
        playerAnim = myAnimator[0];
        effectAnim = myAnimator[1];
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlipSprite();
    }

    void CheckFlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1.0f);
        }
    }

    public void Moving(float move)
    {
        playerAnim.SetFloat("Moving", move);
    }

    public void Jump(bool isJump)
    {
        playerAnim.SetBool("isJumping", isJump);
    }

    public void Attack()
    {
        playerAnim.SetTrigger("Attack");
        effectAnim.SetTrigger("Attack");
    }
}
