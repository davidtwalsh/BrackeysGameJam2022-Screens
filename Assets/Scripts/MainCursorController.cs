using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCursorController : MonoBehaviour
{
    public float mouseSpeed;
    public float xMin, xMax, yMin, yMax;

    bool isTouchingStartButton = false;
    bool isTouchingIcon = false;

    float clickTimer = 0f;
    bool hasClickedOnce = false;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //comment
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");

        //Debug.Log($"xInput: {xInput}, yInput: {yInput}");

        float newX = transform.position.x + (xInput * mouseSpeed);
        float newY = transform.position.y + (yInput * mouseSpeed);

        newX = Mathf.Clamp(newX, xMin, xMax);
        newY = Mathf.Clamp(newY, yMin, yMax);

        transform.position = new Vector3(newX, newY, 0f);

        //for start
        if (isTouchingStartButton == true && Input.GetMouseButtonDown(0))
        {
            ScreenController.Instance.CursorTouchedStart();
        }

        //for icon interaction
        if (isTouchingIcon == true && Input.GetMouseButtonDown(0) && hasClickedOnce == false)
        {
            ScreenController.Instance.CursorTouchedIcon(false);
            hasClickedOnce = true;
        }
        else if (isTouchingIcon == true && Input.GetMouseButtonDown(0) && clickTimer < .5f)
        {
            ScreenController.Instance.CursorTouchedIcon(true);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ScreenController.Instance.IconUnclicked();
            hasClickedOnce = false;
            clickTimer = 0f;
        }

        if (hasClickedOnce == true)
            clickTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (audioSource.isPlaying == true)
                audioSource.Stop();
            audioSource.Play();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StartButton")
        {
            isTouchingStartButton = true;
        }
        else if (collision.gameObject.tag == "SelectIcon")
        {
            isTouchingIcon = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StartButton")
        {
            isTouchingStartButton = false;
        }
        else if (collision.gameObject.tag == "SelectIcon")
        {
            isTouchingIcon = false;
        }
    }
}
