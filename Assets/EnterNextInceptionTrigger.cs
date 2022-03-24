using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNextInceptionTrigger : MonoBehaviour
{
    public void EnterNext()
    {
        InceptionController.Instance.BeginSecondInception();
    }
}
