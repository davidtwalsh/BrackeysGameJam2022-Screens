using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsRocket : MonoBehaviour
{
    public float rotSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;
    public float bulletSpeed;

    bool isReadyToShoot = true;
    float shootTimer = 0f;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AsteroidsController.Instance.isGamePlaying == true)
        {
            float rot = Input.GetAxisRaw("Horizontal");
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * rot * -rotSpeed));

            shootTimer += Time.deltaTime;
            if (shootTimer >= .3f)
                isReadyToShoot = true;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isReadyToShoot == true)
            {
                GameObject b = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
                b.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
                b.transform.parent = AsteroidsController.Instance.gameObject.transform;
                isReadyToShoot = false;
                shootTimer = 0f;
                audioSource.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Asteroid")
        {
            AsteroidsController.Instance.RestartGame();
        }
    }
}
