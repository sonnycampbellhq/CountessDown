using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void loadLevel(int levelNum)
    {
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        for(int i=0; i<projectiles.Length; i++)
        {
            Destroy(projectiles[i]);
        }
        if (levelNum == 10)
        {
            Instantiate(Resources.Load("Prefabs/Countess"));
        }

        Instantiate(Resources.Load($"Prefabs/Levels/Level{levelNum}"));
        Time.timeScale=1;
    }

    public static void loadBlackBackground()
    {
        Instantiate(Resources.Load("Prefabs/Levels/Black"));
    }
}
