using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidsController : MonoBehaviour
{
    public float scoreToWin;
    float timeTillNextAsteroid = 1f;
    public GameObject asteroidPrefab;
    public List<Transform> spawnPositions;
    public float minAsteroidSpeed;
    public float maxAsteroidSpeed;
    public float minAngularV;
    public float maxAngularV;

    public Transform player;

    public int score = 0;
    public TextMeshPro scoreText;

    public static AsteroidsController Instance { private set; get; }

    Stack<GameObject> asteroids;
    public AudioClip soundGameOver;
    public AudioClip soundHitAsteroid;
    AudioSource audioSource;

    public bool isGamePlaying = false;
    public GameObject restartText;

    public float newTimeTillNextAsteroid;
    public float difficultScale;

    float baseNewTimeTillNextAsteroid;
    float baseDifficultyScale;

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
        //newTimeTillNextAsteroid = timeTillNextAsteroid;

        baseNewTimeTillNextAsteroid = newTimeTillNextAsteroid;
        baseDifficultyScale = difficultScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        asteroids = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= scoreToWin)
        {
            ScreenController.Instance.BeatAsteroids();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGamePlaying == false)
        {
            restartText.SetActive(false);
            isGamePlaying = true;
        }

        if (isGamePlaying == true)
        {
            timeTillNextAsteroid -= Time.deltaTime;

            if (timeTillNextAsteroid <= 0)
            {
                SpawnAsteroid();
                timeTillNextAsteroid = newTimeTillNextAsteroid;
                newTimeTillNextAsteroid -= difficultScale;
            }
        }

        scoreText.text = $"Score: {score}";
    }

    void SpawnAsteroid()
    {
        int spawnIndex = Random.Range(0, spawnPositions.Count);
        GameObject a = Instantiate(asteroidPrefab, spawnPositions[spawnIndex].position, Quaternion.Euler(0f, 0f, Random.Range(0, 360f)));
        a.transform.parent = transform;

        asteroids.Push(a);

        //b.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        Vector2 dir = (player.transform.position - a.transform.position).normalized;
        a.GetComponent<Rigidbody2D>().velocity = dir * Random.Range(minAsteroidSpeed,maxAsteroidSpeed);
        a.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(minAngularV, maxAngularV);
    }

    public void RestartGame()
    {
        score = 0;
        while (asteroids.Count > 0)
        {
            GameObject a = asteroids.Pop();
            Destroy(a);
        }
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = soundGameOver;
        audioSource.Play();
        restartText.SetActive(true);
        isGamePlaying = false;

        newTimeTillNextAsteroid = baseNewTimeTillNextAsteroid;
        difficultScale = baseDifficultyScale;
    }

    public void PlayAsteroidSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = soundHitAsteroid;
        audioSource.Play();
    }
}
