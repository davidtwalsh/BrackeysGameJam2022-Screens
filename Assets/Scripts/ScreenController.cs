using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance { get; private set; }

    public ScreenPhase screenPhase = ScreenPhase.BeforeBrickBreak;

    AudioSource audioSource;

    public float loadTime;

    [Header("Start Screen Stuff")]
    public GameObject startScreenParent;
    public GameObject brickBreakIconParent;

    [Header("Before Asteroids Stuff")]
    public GameObject beforeMazeParent;
    public GameObject insertCoinHereObject;
    public GameObject asteroidsIconParent;
    public GameObject beforeAsteroidsBall;
    public GameObject asteroidsSelect;
    bool hasRevealedInsertCoin = false;
    public GameObject asteroidsParent;

    [Header("Eye Stuff")]
    public SpriteRenderer eyeSpriteRenderer;
    public Sprite eyeOneClosed;
    public Sprite eyeOneOpen;
    public Sprite eyeTwoClosed;
    public Sprite eyeTwoOpen;

    [Header("BeforeBrickBreak")]
    public GameObject brickBreakerSelect;
    bool shouldStartBrickSmasherTimer = false;
    float startBrickSmasherTimer = 0f;

    [Header("Loading Screen Stuff")]
    public GameObject loadingScreenParent;
    public TextMeshPro loadingTitle;
    public Transform loadingCog;
    public float cogSpeed;
    float loadingTimer = 0f;

    [Header("BrickSmasher Stuff")]
    public GameObject brickSmasherParent;

    [Header("Static Screen")]
    public GameObject staticScreenParent;
    float staticTimer = 0f;

    [Header("Face in screen")]
    public GameObject faceInScreenParent;


    [Header("Audio")]
    public AudioClip soundBrickSmasherBG;
    public AudioClip soundMazeDasherBG;

    [Header("BeforeRunner")]
    public GameObject cursor;
    public GameObject startbutton;
    public GameObject beforeRunnerParent;
    public GameObject eyeball;
    public GameObject startText;
    public GameObject lastBall;

    Camera mainCamera;

  

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

    private void Start()
    {
        mainCamera = Camera.main;

        
    }
    // Update is called once per frame
    void Update()
    {
        switch (screenPhase)
        {
            case ScreenPhase.BeforeBrickBreak:

                if (shouldStartBrickSmasherTimer == true)
                {
                    startBrickSmasherTimer += Time.deltaTime;
                }
                if (startBrickSmasherTimer >= .1f)
                {
                    startScreenParent.SetActive(false);
                    screenPhase = ScreenPhase.BrickBreakLoading;
                    loadingTitle.text = "Brick Smasher";
                    loadingScreenParent.SetActive(true);
                    audioSource.clip = soundBrickSmasherBG;
                    audioSource.Play();
                }

                break;

            case ScreenPhase.LoadingAsteroids:

                loadingCog.Rotate(new Vector3(0, 0, Time.deltaTime * cogSpeed));
                loadingTimer += Time.deltaTime;

                if (loadingTimer >= loadTime)
                {
                    screenPhase = ScreenPhase.Asteroids;
                    loadingScreenParent.SetActive(false);
                    //brickSmasherParent.SetActive(true);
                    asteroidsParent.SetActive(true);
                    loadingTimer = 0f;
                }

                break;

            case ScreenPhase.BrickBreakLoading:

                loadingCog.Rotate(new Vector3(0, 0, Time.deltaTime * cogSpeed));
                loadingTimer += Time.deltaTime;

                if (loadingTimer >= loadTime)
                {
                    screenPhase = ScreenPhase.BrickBreak;
                    loadingScreenParent.SetActive(false);
                    brickSmasherParent.SetActive(true);
                    loadingTimer = 0f;
                }

                break;

            case ScreenPhase.StaticAfterBrickBreak:

                staticTimer += Time.deltaTime;
                if (staticTimer >= 4)
                {
                    screenPhase = ScreenPhase.BeforeAsteroids;
                    staticTimer = 0f;
                    staticScreenParent.SetActive(false);
                    startScreenParent.SetActive(true);
                    brickBreakIconParent.SetActive(false);
                    eyeSpriteRenderer.sprite = eyeTwoOpen;
                    beforeMazeParent.SetActive(true);
                }

                break;

            default:

                //Debug.LogWarning("defualt case hit");
                break;
        }
    }

    public void CursorTouchedStart()
    {
        switch (screenPhase)
        {
            case ScreenPhase.BeforeBrickBreak:

                if (eyeSpriteRenderer.sprite == eyeOneOpen)
                    eyeSpriteRenderer.sprite = eyeOneClosed;
                else
                    eyeSpriteRenderer.sprite = eyeOneOpen;

                break;

            case ScreenPhase.BeforeAsteroids:

                if (hasRevealedInsertCoin == false)
                {
                    insertCoinHereObject.SetActive(true);
                    hasRevealedInsertCoin = true;
                }

                if (eyeSpriteRenderer.sprite == eyeTwoOpen)
                    eyeSpriteRenderer.sprite = eyeTwoClosed;
                else
                    eyeSpriteRenderer.sprite = eyeTwoOpen;
                break;

            default:

                Debug.LogWarning("defualt case hit");
                break;
        }
    }

    public void CursorTouchedIcon(bool isSecondClick)
    {
        switch (screenPhase)
        {
            case ScreenPhase.BeforeBrickBreak:

                if (isSecondClick == false)
                {
                    brickBreakerSelect.SetActive(true);
                }
                else if (isSecondClick == true)
                {
                    shouldStartBrickSmasherTimer = true;
                }

                break;

            case ScreenPhase.BeforeAsteroids:

                if (isSecondClick == false)
                {
                    asteroidsSelect.SetActive(true);
                }
                else if (isSecondClick == true)
                {
                    //shouldStartBrickSmasherTimer = true;
                    screenPhase = ScreenPhase.LoadingAsteroids;
                    startScreenParent.SetActive(false);
                    loadingTitle.text = "Meteor Blaster";
                    loadingScreenParent.SetActive(true);
                    audioSource.clip = soundMazeDasherBG;
                    audioSource.Play();
                }

                break;

            default:

                Debug.LogWarning("default hit");
                break;
        }
    }

    public void IconUnclicked()
    {
        switch (screenPhase)
        {
            case ScreenPhase.BeforeBrickBreak:

                brickBreakerSelect.SetActive(false);

                break;

            case ScreenPhase.BeforeAsteroids:

                asteroidsSelect.SetActive(false);

                break;

            default:

                Debug.LogWarning("default hit");
                break;
        }
    }

    public void PlayerBeatBrickSmasher()
    {
        brickSmasherParent.SetActive(false);
        staticScreenParent.SetActive(true);

        screenPhase = ScreenPhase.StaticAfterBrickBreak;
        audioSource.Stop();

        StartCoroutine(CameraZoomInAndOut());

    }

    public void InitAsteroidsIcon()
    {
        asteroidsIconParent.SetActive(true);
        insertCoinHereObject.SetActive(false);
        beforeAsteroidsBall.SetActive(false);
    }

    public void BeatAsteroids()
    {
        asteroidsParent.SetActive(false);
        faceInScreenParent.SetActive(true);
        screenPhase = ScreenPhase.FaceInScreen;
        if (audioSource.isPlaying)
            audioSource.Stop();
        StartCoroutine(CameraZoomInAndOutSkull());
        beforeMazeParent.SetActive(false);

    }

    public void SkullFinished()
    {
        faceInScreenParent.SetActive(false);
        startScreenParent.SetActive(true);
        screenPhase = ScreenPhase.BeforeDinoRun;

        cursor.SetActive(false);
        startbutton.SetActive(false);
        beforeRunnerParent.SetActive(true);
        eyeball.SetActive(false);
        startText.SetActive(false);

        
        mainCamera.transform.parent = lastBall.transform;
        mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y + 3.259f, mainCamera.transform.localPosition.z);
        Debug.Log("Moving up");
        
    }

    IEnumerator CameraZoomInAndOut()
    {
        while (mainCamera.fieldOfView > 45)
        {
            mainCamera.fieldOfView -= .6f;
            yield return null;
        }
        yield return new WaitForSeconds(3.5f);

        while (mainCamera.fieldOfView < 60)
        {
            mainCamera.fieldOfView += .3f;
            yield return null;
        }
        mainCamera.fieldOfView = 60;
    }

    IEnumerator CameraZoomInAndOutSkull()
    {
        while (mainCamera.fieldOfView > 45)
        {
            mainCamera.fieldOfView -= .6f;
            yield return null;
        }
        yield return new WaitForSeconds(8f);

        while (mainCamera.fieldOfView < 60)
        {
            mainCamera.fieldOfView += .3f;
            yield return null;
        }
        mainCamera.fieldOfView = 60;
    }

    public enum ScreenPhase
    {
        BeforeBrickBreak,
        BrickBreakLoading,
        BrickBreak,
        StaticAfterBrickBreak,
        BeforeAsteroids,
        LoadingAsteroids,
        Asteroids,
        FaceInScreen,
        BeforeDinoRun,
        DinoRun,
        AfterDinoRun
    }
}
