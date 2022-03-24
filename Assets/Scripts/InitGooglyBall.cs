using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGooglyBall : MonoBehaviour
{
    public GameObject leftEye;
    public GameObject rightEye;

    float eyeTimer = 0f;
    bool hasHitGround = false;

    Rigidbody2D rb;
    public float speed;

    AudioSource audioSource;

    bool hasPlayedLeftEye = false;
    bool hasPlayedRightEye = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHitGround = true;
        leftEye.SetActive(true);

        if (hasPlayedLeftEye == false)
        {
            audioSource.Play();
            hasPlayedLeftEye = true;
        }

        if (collision.gameObject.tag == "Cursor")
        {
            //rb.velocity = Random.insideUnitCircle.normalized * speed;
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hole")
        {
            ScreenController.Instance.InitAsteroidsIcon();
            
        }
    }

    private void Update()
    {
        if (hasHitGround == true)
        {
            eyeTimer += Time.deltaTime;
        }

        if (eyeTimer >= 1.5f)
        {
            rightEye.SetActive(true);
            if (hasPlayedRightEye == false)
            {
                audioSource.Play();
                hasPlayedRightEye = true;
            }
            
        }
    }
}
