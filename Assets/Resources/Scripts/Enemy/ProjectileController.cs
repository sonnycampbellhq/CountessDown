using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float speed = 5;
    float knockback = 1;
    Vector2 initialPos;
    Vector2 direction;
    Vector2 pos;

    AudioHandler audioHandler;
    // Start is called before the first frame update
    void Start()
    {
        audioHandler = GameObject.FindGameObjectWithTag("AudioHandler").GetComponent<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        pos=transform.position;
        pos+=direction*speed*Time.deltaTime;
        transform.position=pos;
    }

    public void SetProjectileController(Vector2 initialPosIn, Vector2 directionIn, float speedIn, float sizeIn, float knockbackIn)
    {
        initialPos=initialPosIn;
        transform.position=initialPos;
        direction=directionIn;
        speed = speedIn;
        transform.localScale=new Vector3(sizeIn, sizeIn, 1);
        knockback=knockbackIn;
    }

    public float GetKnockback()
    {
        return knockback;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject, 0);
            audioHandler.playProjectileHit();
        }
    }
}
