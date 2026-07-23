using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField]
    int reloadTime;
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
    // Start is called before the first frame update
    void Start()
    {
        projectile = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (shots+1)*reloadTime)
        {
            shots++;
            Instantiate(projectile).
                GetComponent<ProjectileController>().
                    SetProjectileController(transform.position, direction/direction.magnitude, projectileSpeed, projectileSize, knockback);
        }
    }
}
