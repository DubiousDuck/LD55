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
    private int jumpVar = 1;

    protected Rigidbody rb;
    protected Vector3 size;
    protected SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        size = this.GetComponent<Collider>().bounds.size;
        rb = this.GetComponent<Rigidbody>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newVel = Input.GetAxis("Horizontal") * Vector3.right * moveSpeed;     //Edit in project settings input manager
        sr.flipX = newVel.x > 0 ? false : newVel.x < 0 ? true : sr.flipX;
        walking = newVel.x != 0;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, size.y / 2 + 0.05f))
            groundVar = 0;
        else if (groundVar == 0)
            groundVar = 1;

        if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Jump") > 0) && groundVar < jumpVar)
        {
            Debug.Log("Jump");
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
