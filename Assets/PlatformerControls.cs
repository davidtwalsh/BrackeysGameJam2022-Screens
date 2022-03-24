using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerControls : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public bool isTouchingGround = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown("Jump") && isTouchingGround)
            Jump();

    }

    void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void Jump() => rb.velocity = new Vector2(0, jumpForce);



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BeginChase")
        {
            ChaseController.Instance.StartChase();
        }
    }
}
