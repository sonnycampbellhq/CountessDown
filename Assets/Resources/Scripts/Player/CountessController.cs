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
    int maxHealth;
    int health;

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
    float blockKnockbackReduction = 5;


    bool isAttacking = false;
    bool canAttack = true;
    float lastAttack=-5;
    [SerializeField]
    float attackCooldown;
    [SerializeField]
    float attackDuration;
    [SerializeField]
    Vector3 attackSize;
    [SerializeField]
    float attackOffset;
    GameObject attack;
    
    bool isInvincible = false;
    [SerializeField]
    float invincibilityDuration;
    float lastDamageTaken=-5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health=maxHealth;
        attack = Resources.Load<GameObject>("Prefabs/Attack");
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityCheck();
        blockCheck();
        if (!isBlocking)
        {
            attackCheck();
        }

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        knockbackVelocity = Vector2.MoveTowards(knockbackVelocity, Vector2.zero, 15*Time.deltaTime);

        if (isBlocking)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, knockbackVelocity, Time.deltaTime) + knockbackVelocity;
        }
        else
        {
            rb.velocity = movementDirection * movementSpeed + knockbackVelocity;
        }
    }

    void blockCheck()
    {
        if (canBlock)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lastBlock = Time.time;
                isBlocking=true;
                canBlock=false;
                //block starts
            }
        }
        else
        {
            float timeSinceBlock=Time.time-lastBlock;
            if (timeSinceBlock > blockCooldown)
            {
                canBlock=true;
                //can block
            }
            else if(timeSinceBlock > blockDuration&&isBlocking)
            {
                isBlocking=false;
                //can't block
            }
        }
    }

    void attackCheck()
    {
        if (canAttack)
        {
            float fire1 = Input.GetAxis("Fire1");
            float fire2 = Input.GetAxis("Fire2");
            if (fire1 != 0 || fire2 != 0)
            {
                lastAttack = Time.time;
                isAttacking=true;
                canAttack=false;
                if (fire1 != 0)
                {
                    fire2 = 0;
                }
                Instantiate(attack).
                GetComponent<AttackController>().
                    SetAttackController(transform.position, attackOffset, new Vector2(fire1, fire2), attackSize);
            }
        }
        else
        {
            float timeSinceAttack=Time.time-lastAttack;
            if (timeSinceAttack > attackCooldown)
            {
                canAttack=true;// can attack
            }
            else if(timeSinceAttack > attackDuration&&isAttacking)
            {
                isAttacking=false;// on cooldown
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Projectile")
        {
            ProjectileController pc = go.GetComponent<ProjectileController>();
            
            go.SetActive(false);
            if (isBlocking)
            {
                knockbackVelocity = pc.GetDirection()*pc.GetKnockback()/blockKnockbackReduction;
            }
            else
            {
                knockbackVelocity = pc.GetDirection()*pc.GetKnockback();
                takeDamage();
            }
            
        }
        else if(go.tag == "Down")
        {
            // next level (down)
            // delete everything and load a prefab of the next level?
        }
        else if(go.tag == "Spike")
        {
            //add some random knockback when on spike?
            //knockbackVelocity+=-movementDirection/movementDirection.magnitude;
            takeDamage();
        }
        else if(go.tag == "Health")
        {
            heal();
            Destroy(go, 0);
        }
        else if(go.tag == "Money")
        {
            Debug.Log(go.GetComponent<Variables>());
        }
    }

    void heal()
    {
        health++;
        Debug.Log(health);
    }

    void takeDamage()
    {
        if (!isInvincible)
        {
            health--;
            Debug.Log(health);
            if (health <= 0)
            {
                // die
                // death should take you back to a menu of some sort
            }

            lastDamageTaken=Time.time;
            isInvincible=true;
            gameObject.GetComponent<SpriteRenderer>().color=Color.magenta;
        }
        
    }

    void invincibilityCheck()
    {
        if (isInvincible&&Time.time-lastDamageTaken>invincibilityDuration)
        {
            isInvincible=false;
            gameObject.GetComponent<SpriteRenderer>().color=Color.white;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision: "+collision);
    }
}
