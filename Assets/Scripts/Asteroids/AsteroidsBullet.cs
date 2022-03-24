using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsBullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<Asteroid>().HitByBullet();
            Destroy(gameObject);
        }
    }
}
