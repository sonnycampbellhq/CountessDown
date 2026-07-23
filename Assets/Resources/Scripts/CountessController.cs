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
    int blockCooldown=0;
    [SerializeField]
    int maxBlockCooldown;
    int blockDuration=0;
    [SerializeField]
    int maxBlockDuration;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blockCooldown <= 0)
        {
            isBlocking = Input.GetKeyDown(KeyCode.Space);
            if (isBlocking)
            {
                blockCooldown=maxBlockCooldown;
                blockDuration=maxBlockDuration;
                Debug.Log("START");
                GetComponent<SpriteRenderer>().color=Color.blue;
            }
            else
            {
                GetComponent<SpriteRenderer>().color=Color.green;
            }
        }
        else
        {
            blockCooldown--;
            if (blockDuration <= 0)
            {
                if (isBlocking)
                {
                    isBlocking=false;
                    Debug.Log("STOP");
                    GetComponent<SpriteRenderer>().color=Color.black;
                }
            }
            else
            {
                blockDuration--;
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
