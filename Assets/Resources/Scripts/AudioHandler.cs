using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    
    AudioClip attack;
    AudioClip coin;
    AudioClip down;
    AudioClip heal;
    AudioClip kill;
    AudioClip lose;
    AudioClip projectileHit;
    AudioClip projectileShoot;
    AudioClip takeDamage;
    AudioClip win;
    [SerializeField] AudioSource source;

    void Start()
    {
        attack=Resources.Load<AudioClip>("Audio/Attack");
        coin=Resources.Load<AudioClip>("Audio/Coin");
        down=Resources.Load<AudioClip>("Audio/Down");
        heal=Resources.Load<AudioClip>("Audio/Heal");
        kill=Resources.Load<AudioClip>("Audio/Kill");
        lose=Resources.Load<AudioClip>("Audio/Lose");
        projectileHit=Resources.Load<AudioClip>("Audio/ProjectileHit");
        projectileShoot=Resources.Load<AudioClip>("Audio/ProjectileShoot");
        takeDamage=Resources.Load<AudioClip>("Audio/TakeDamage");
        win=Resources.Load<AudioClip>("Audio/Win");
    }

    public void playAttack()
    {
        source.PlayOneShot(attack);
    }

    public void playCoin()
    {
        source.PlayOneShot(coin);
    }

    public void playDown()
    {
        source.PlayOneShot(down);
    }

    public void playHeal()
    {
        source.PlayOneShot(heal);
    }

    public void playKill()
    {
        source.PlayOneShot(kill);
    }

    public void playLose()
    {
        source.PlayOneShot(lose);
    }

    public void playProjectileHit()
    {
        source.PlayOneShot(projectileHit);
    }

    public void playProjectileShoot()
    {
        source.PlayOneShot(projectileShoot);
    }

    public void playTakeDamage()
    {
        source.PlayOneShot(takeDamage);
    }

    public void playWin()
    {
        source.PlayOneShot(win);
    }
}