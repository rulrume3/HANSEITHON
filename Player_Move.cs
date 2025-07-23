using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float slideDuration = 0.5f;
    public Vector3 slideColliderSize = new Vector2(1f, 0.5f);

    Rigidbody2D rb;
    CapsuleCollider2D playerCollider;
    Vector2 originalColliderSize;
    private bool isJumping = false;
    private bool isSliding = false;
    float sliderTimer = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = rb.GetComponent<CapsuleCollider2D>();
        originalColliderSize = playerCollider.bounds.size;
    }

    void Update()
    {
        transform.position += Vector3.right * moveSpeed
              * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()
            && !isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding
            && IsGrounded())
        {
            isSliding = true;
            sliderTimer = 0.0f;
            if (playerCollider is CapsuleCollider2D box)
                box.size = slideColliderSize;
        }
        if (isSliding)
        {
            sliderTimer += Time.deltaTime;
            if (sliderTimer >= slideDuration)
            {
                isSliding = false;
                if (playerCollider is CapsuleCollider2D box)
                    box.size = originalColliderSize;
            }
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
