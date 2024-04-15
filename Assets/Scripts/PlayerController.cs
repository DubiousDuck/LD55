using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    public bool walking = false;
    public bool grounded = false;
    public int jumpNum = 0;
    public int maxJumps = 2;
    private bool facingRight = true;

    protected Rigidbody2D rb;
    protected Vector3 size;
    protected float sizeBuffer = 0.01f;
    protected SpriteRenderer sr;
    protected bool stunned = false;
    protected Web web = null;
    protected int origLayer;

    // Start is called before the first frame update
    void Start()
    {
        size = this.GetComponent<Collider2D>().bounds.size * (1 + sizeBuffer * 2);
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        this.origLayer = this.gameObject.layer;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!stunned)
        {
            Vector2 newVel = Input.GetAxis("Horizontal") * Vector2.right * moveSpeed;     //Edit in project settings input manager
            if ((newVel.x > 0 && !facingRight) || (newVel.x < 0 && facingRight))
            {
                transform.Rotate(0, 180, 0);
                facingRight = !facingRight;
            }
            walking = newVel.x != 0;

            this.gameObject.layer = 2;
            grounded = Physics2D.Raycast(this.transform.position, Vector2.down, this.size.y/2, ~(1 << 2));
            Debug.DrawRay(this.transform.position, Vector2.down * this.size.y / 2);
            this.gameObject.layer = origLayer;

            if (grounded)
                jumpNum = 0;
            else if (jumpNum == 0)
                jumpNum = 1;
            if ((jumpNum == 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) ||
                (jumpNum < maxJumps && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))))
            {
                jumpNum += 1;
                newVel.y = jumpForce;
            }
            else
                newVel.y = rb.velocity.y;
            rb.velocity = newVel;
            if (this.web)
                rb.velocity *= web.slowModifier;
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (transform.position.y < -10)
            Respawn();
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Web>() && other.gameObject.tag != this.gameObject.tag)
            this.web = other.GetComponent<Web>();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        this.web = null;
    }
    public IEnumerator stun(float stunTime)
    {
        stunned = true;
        sr.color = Color.yellow;
        yield return new WaitForSeconds(stunTime);
        stunned = false;
        sr.color = Color.white;
    }
}
