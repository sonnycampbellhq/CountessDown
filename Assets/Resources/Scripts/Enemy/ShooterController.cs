using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    float startTime;
    [SerializeField]
    float reloadTime;
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

    [SerializeField]
    float timeOffsetProportion;
    AudioHandler audioHandler;
    // Start is called before the first frame update
    void Start()
    {
        projectile = Resources.Load<GameObject>("Prefabs/Projectile");
        countess = GameObject.FindGameObjectWithTag("Countess");
        startTime=Time.time+2/reloadTime-2+timeOffsetProportion*reloadTime; //this adds start delay
        audioHandler = GameObject.FindGameObjectWithTag("AudioHandler").GetComponent<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time-startTime > (shots+1)*reloadTime&&countess!=null)
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
            audioHandler.playProjectileShoot();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            takeDamage();
            countess.GetComponent<CountessController>().gainMoney();
        }
    }

  void takeDamage()
    {
        //add health system???
        audioHandler.playKill();
        Destroy(gameObject, 0);
    }
}
