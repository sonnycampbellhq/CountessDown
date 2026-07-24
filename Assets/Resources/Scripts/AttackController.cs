using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackController : MonoBehaviour
{
    float offset;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetAttackController(Vector3 basePosition, float offsetIn, Vector2 directionIn, Vector3 sizeIn)
    {
        offset=offsetIn;
        direction=directionIn;
        Vector3 pos = offset*direction;
        transform.position =basePosition+pos;
        transform.localScale=sizeIn;
        transform.up=direction;
    }
}
