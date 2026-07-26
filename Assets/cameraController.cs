using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraController : MonoBehaviour
{
    float referenceAspect = (float)1920/1080;
    float referenceSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect <= referenceAspect)
        {
            cam.orthographicSize = referenceSize * referenceAspect/currentAspect;
        }
    }
}
