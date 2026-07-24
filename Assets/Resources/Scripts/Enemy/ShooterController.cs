using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField]
    int reloadTime;
    [SerializeField]
    bool tracking;
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    float projectileSize;
    [SerializeField]
    float knockback;
    int shots=0;
    GameObject projectile;
    GameObject countess;
    // Start is called before the first frame update
    void Start()
    {
        projectile = Resources.Load<GameObject>("Prefabs/Projectile");
        countess = GameObject.FindGameObjectWithTag("Countess");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (shots+1)*reloadTime&&countess!=null)
        {
            if (tracking)
            {
                //calc direction
                direction = countess.transform.position - transform.position;
            }

            shots++;
            Instantiate(projectile).
                GetComponent<ProjectileController>().
                    SetProjectileController(transform.position, direction/direction.magnitude, projectileSpeed, projectileSize, knockback);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            takeDamage();
        }
    }

  void takeDamage()
    {
        //add health system???
        Destroy(gameObject, 0);
    }
}
