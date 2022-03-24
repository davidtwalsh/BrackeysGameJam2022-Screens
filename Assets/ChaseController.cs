using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{

    public static ChaseController Instance { private set; get; }

    public bool chaseStarted = false;

    public GameObject zombie;
    public float zombieMoveSpeed;

    [Header("For Chase")]
    public Vector3 playerSpawn;
    public Vector3 zombieSpawn;
    public GameObject player;
    public GameObject beginChaseCollider;
    public GameObject staticPlane;

    AudioSource audioSource;

    public bool hasWon = false;

    public GameObject youWinText;

    Camera camera;

    public GameObject blackScreen;

    public AudioSource success;

    public GameObject victoryScreen;

    public GameObject staticScreenBG;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (chaseStarted == true && hasWon == false)
        {
            zombie.transform.position = new Vector3(zombie.transform.position.x + Time.deltaTime * zombieMoveSpeed, zombie.transform.position.y, zombie.transform.position.z);
        }
    }

    public void StartChase()
    {
        chaseStarted = true;
        zombie.GetComponent<AudioSource>().Play();
    }

    public void ResetChase()
    {
        player.transform.position = playerSpawn;
        zombie.transform.position = zombieSpawn;
        zombie.GetComponent<AudioSource>().Stop();
        chaseStarted = false;
        beginChaseCollider.SetActive(true);

    }

    public IEnumerator DeathOnChase()
    {
        staticPlane.SetActive(true);
        audioSource.Play();
        zombie.GetComponent<AudioSource>().Stop();

        yield return new WaitForSeconds(4f);

        staticPlane.SetActive(false);
        audioSource.Stop();
        ResetChase();
    }

    public void PlayerReachedFlag()
    {
        hasWon = true;
        zombie.SetActive(false);
        youWinText.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        zombie.GetComponent<AudioSource>().Stop();
        blackScreen.SetActive(true);
        success.Play();

    }

    public IEnumerator ResetToComputer()
    {
        blackScreen.SetActive(false);
        staticPlane.SetActive(true);
        audioSource.Play();
        success.Stop();

        yield return new WaitForSeconds(4f);

        camera.transform.parent = null;
        camera.transform.position = new Vector3(26.2f, -7.7f, -65.5f);
        staticPlane.SetActive(false);
        audioSource.Stop();
        success.Play();
        victoryScreen.SetActive(true);
        transform.parent.parent.gameObject.SetActive(false);

        //StartCoroutine(FirstInception());
        InceptionController.Instance.StartCoroutine(InceptionController.Instance.FirstInception());
    }


}
