using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    public void PlaySkullAudio()
    {
        //GetComponent<AudioSource>().Play();
    }

    public void SkullDone()
    {
        ScreenController.Instance.SkullFinished();
    }
}
