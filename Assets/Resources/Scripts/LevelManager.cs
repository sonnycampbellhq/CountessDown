using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void loadLevel(int levelNum)
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
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
    }

    public static void loadBlackBackground()
    {
        
    }
}
