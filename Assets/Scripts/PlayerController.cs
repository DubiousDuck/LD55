using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    public bool walking = false;
    private int groundVar = 0;
    public int jumpVar = 1;
    private bool facingRight = true;

    protected Rigidbody2D rb;
    protected Vector3 size;
    protected float sizeBuffer = 0.01f;
    protected SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        size = this.GetComponent<Collider2D>().bounds.size * (1 + sizeBuffer * 2);
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newVel = Input.GetAxis("Horizontal") * Vector2.right * moveSpeed;     //Edit in project settings input manager
        if((newVel.x > 0 && !facingRight) || (newVel.x < 0 && facingRight)){
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;
        }
        walking = newVel.x != 0;

        if (Physics2D.Raycast(transform.position + Vector3.down * size.y/2, Vector2.down, sizeBuffer, ~(1 << 2)))
            groundVar = 0;
        else if (groundVar == 0)
            groundVar = 1;

        if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Jump") > 0) && groundVar < jumpVar)
        {
            groundVar += 1;
            newVel.y = jumpForce;
        }
        else
        {
            newVel.y = rb.velocity.y;
        }

        rb.velocity = newVel;

        if (transform.position.y < -10)
            Respawn();
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
