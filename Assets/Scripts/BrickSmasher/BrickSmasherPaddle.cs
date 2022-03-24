using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSmasherPaddle : MonoBehaviour
{

    public float speed = 1f;

    float input;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * input * speed;
    }
}
