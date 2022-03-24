using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InceptionController : MonoBehaviour
{
    public static InceptionController Instance { private set; get; }
    public GameObject victoryScreen;
    AudioSource audioSource;
    public AudioSource success;
    public GameObject staticScreenBG;
    public GameObject skull;
    public GameObject secondInceptionScreen;
    public GameObject youLoseText;
    public GameObject final;
    public AudioClip win;

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
    public IEnumerator FirstInception()
    {
        yield return new WaitForSeconds(6f);

        victoryScreen.SetActive(false);
        audioSource.Play();
        success.Stop();
        staticScreenBG.SetActive(true);
        skull.SetActive(true);

    }

    public void BeginSecondInception()
    {
        skull.SetActive(false);
        secondInceptionScreen.SetActive(true);
        youLoseText.SetActive(true);
        victoryScreen.SetActive(false);
        audioSource.Stop();
        staticScreenBG.SetActive(false);

        StartCoroutine(Final());
        //audioSource.clip = win;
        //audioSource.Play();
        //audioSource.loop = false;
    }

    public IEnumerator Final()
    {
        yield return new WaitForSeconds(3f);
        final.SetActive(true);

    }

}
