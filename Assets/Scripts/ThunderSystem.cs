using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSystem : MonoBehaviour
{
    float timeTillNextThunder;
    float thunderTimer = 0f;
    public GameObject thunderLight;
    AudioSource audioSource;
    public Material lampMat;
    public GameObject lampLight;

    float timeTillNextLampFlicker;
    float lampTimer = 0f;
 

    void Start()
    {
        ResetForNextThunder();
        ResetLampFlicker();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        thunderTimer += Time.deltaTime;
        lampTimer += Time.deltaTime;

        if (thunderTimer >= timeTillNextThunder)
        {
            thunderTimer = 0f;
            ResetForNextThunder();
            audioSource.Play();
            StartCoroutine(ThunderStrike());
        }

        if (lampTimer >= timeTillNextLampFlicker)
        {
            lampTimer = 0f;
            ResetLampFlicker();
            StartCoroutine(LampFlicker());
        }
    }

    IEnumerator ThunderStrike()
    {
        thunderLight.SetActive(true);
        lampLight.SetActive(false);
        lampMat.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(.2f);
        thunderLight.SetActive(false);

        yield return new WaitForSeconds(.2f);
        lampMat.EnableKeyword("_EMISSION");
        lampLight.SetActive(true);
    }

    IEnumerator LampFlicker()
    {
        lampLight.SetActive(false);
        lampMat.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(.1f);
        lampMat.EnableKeyword("_EMISSION");
        lampLight.SetActive(true);
        yield return new WaitForSeconds(.1f);
        lampLight.SetActive(false);
        lampMat.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(.1f);
        lampMat.EnableKeyword("_EMISSION");
        lampLight.SetActive(true);

    }

    void ResetForNextThunder()
    {
        timeTillNextThunder = Random.Range(15f, 60f);
    }

    void ResetLampFlicker()
    {
        timeTillNextLampFlicker = Random.Range(10f, 30f);
    }
}
