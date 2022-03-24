using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrickSmasherController : MonoBehaviour
{
    public Transform brickParent;
    public GameObject brickPrefab;
    public Vector3 brickStartPos;
    public float brickXOffset;
    public float brickYOffset;

    public List<Color> brickColors;

    public bool isPlaying = false;

    public static BrickSmasherController Instance { get; private set; }
    public BrickSmasherBall ball;

    Stack<GameObject> bricks;

    public GameObject restartText;

    public int numberRows;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        bricks = new Stack<GameObject>();
        SpawnBricks();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && isPlaying == false)
        {
            ball.gameObject.SetActive(true);
            ball.Respawn();
            ResetBricks();
            isPlaying = true;
        }

        if (isPlaying == true)
            restartText.SetActive(false);
        else
            restartText.SetActive(true);

    }

    public void SpawnBricks()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < numberRows; j++)
            {
                Color brickColor = brickColors[j];
                Vector3 spawnPos = new Vector3(brickStartPos.x + brickXOffset * i, brickStartPos.y - j * brickYOffset, brickStartPos.z);
                GameObject b = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
                bricks.Push(b);


                b.transform.parent = brickParent;
                b.GetComponent<SpriteRenderer>().color = brickColor;
            }
        }
    }

    public void ResetBricks()
    {
        while (bricks.Count > 1)
        {
            GameObject b = bricks.Pop();
            Destroy(b);
        }
        SpawnBricks();
    }


}
