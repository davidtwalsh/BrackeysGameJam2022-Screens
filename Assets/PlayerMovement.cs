using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 1f;

    float horizontalMove = 0f;

    bool jump = false;
    

    // Update is called once per frame
    void Update()
    {
        if (ChaseController.Instance.hasWon == false)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (ChaseController.Instance.hasWon == false)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BeginChase")
        {
            ChaseController.Instance.StartChase();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Zombie")
        {
            ChaseController.Instance.StartCoroutine(ChaseController.Instance.DeathOnChase());
        }

        if (collision.gameObject.tag == "Flag")
        {
            ChaseController.Instance.PlayerReachedFlag();
        }
    }
}
