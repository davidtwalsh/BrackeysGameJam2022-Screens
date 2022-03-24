using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSmasherBall : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    public Vector3 spawnPos;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        //Respawn();
    }

    public void Respawn()
    {
        transform.position = spawnPos;
        //rb.velocity = Random.insideUnitCircle.normalized * speed;
        //rb.velocity = Vector2.up * speed;
        rb.velocity = new Vector2(Random.Range(-.5f,.5f),1) * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource.isPlaying == true)
            audioSource.Stop();
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BrickTopWall")
        {
            ScreenController.Instance.PlayerBeatBrickSmasher();
        }
    }
}
