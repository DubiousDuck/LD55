using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float groundDist = 0.1f; // Adjust this value according to your player's size
    public float jumpForce = 5f;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDist, terrainLayer);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y).normalized;
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        // Flip sprite
        if (x != 0)
        {
            sr.flipX = (x < 0); // Flip if moving left
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

