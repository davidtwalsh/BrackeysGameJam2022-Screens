using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSmasherBounds : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //collision.gameObject.GetComponent<BrickSmasherBall>().Respawn();
            BrickSmasherController.Instance.isPlaying = false;
            collision.gameObject.SetActive(false);
        }
    }
}
