using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public void HitByBullet()
    {
        AsteroidsController.Instance.score += 1;
        AsteroidsController.Instance.PlayAsteroidSound();
        Destroy(gameObject);
    }
}
