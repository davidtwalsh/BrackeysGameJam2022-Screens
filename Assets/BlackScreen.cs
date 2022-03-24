using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public void BlackScreenDone()
    {
        ChaseController.Instance.StartCoroutine(ChaseController.Instance.ResetToComputer());
    }
}
