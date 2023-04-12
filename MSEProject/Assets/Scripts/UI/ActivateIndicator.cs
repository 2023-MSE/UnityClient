using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIndicator : MonoBehaviour
{
    public Transform targetTransform;

    public void OtherTargetActivated(Transform inputTransform)
    {
        if (targetTransform != null)
            targetTransform.gameObject.SetActive(false);
        
        targetTransform = inputTransform;
        inputTransform.gameObject.SetActive(true);
    }
}
