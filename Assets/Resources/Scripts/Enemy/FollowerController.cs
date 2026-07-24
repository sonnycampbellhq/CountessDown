using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    GameObject countess;
    // Start is called before the first frame update
    void Start()
    {
        countess = GameObject.FindGameObjectWithTag("Countess");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
