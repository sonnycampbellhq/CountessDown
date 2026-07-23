using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class CountessController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    Vector2 movementDirection;
    Rigidbody2D rb;
    Vector2 knockbackVelocity;

    bool isBlocking = false;
    bool canBlock = true;
    float lastBlock=-5;
    [SerializeField]
    float blockCooldown;
    [SerializeField]
    float blockDuration;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBlock)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lastBlock = Time.time;
                isBlocking=true;
                canBlock=false;
                Debug.Log("Blocking");
            }
        }
        else
        {
            float timeSinceBlock=Time.time-lastBlock;
            if (timeSinceBlock > blockCooldown)
            {
                canBlock=true;
                Debug.Log("Can block");
            }
            else if(timeSinceBlock > blockDuration&&isBlocking)
            {
                isBlocking=false;
                Debug.Log("On cooldown");
            }
        }

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed + knockbackVelocity;

        knockbackVelocity = Vector2.MoveTowards(knockbackVelocity, Vector2.zero, 10*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Projectile")
        {
            ProjectileController pc = go.GetComponent<ProjectileController>();
            gameObject.GetComponent<Rigidbody2D>().AddForce(pc.GetDirection()*pc.GetKnockback(), ForceMode2D.Impulse);
            knockbackVelocity = pc.GetDirection()*pc.GetKnockback();
            go.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("C");
    }
}
