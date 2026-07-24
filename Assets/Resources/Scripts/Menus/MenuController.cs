using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    bool pauseMenuActive = false;
    public GameObject deathMenu;
    bool deathMenuActive = false;
    bool inLevel=false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            inLevel=true;
        }
    }

  void Update()
    {
        escapePressCheck();
    }

  public void onStartPress()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale=1;
    }

    public void onMenuPress()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void deathMenuSpawn(int moneyIn, int floorsIn)
    {
        //other menus can't spawn
        //set floor and money text (take as params)
        deathMenu.SetActive(true);
        deathMenuActive=true;
        TextMeshProUGUI[] menuTexts = deathMenu.GetComponentsInChildren<TextMeshProUGUI>();
        for(int i=0; i<menuTexts.Length; i++)
        {
            if (menuTexts[i].text == "Money")
            {
                menuTexts[i].text = $"Money\n\n{moneyIn}";
            }
            else if(menuTexts[i].text == "Floors")
            {
                menuTexts[i].text = $"Floors\n\n{floorsIn}";
            }
        }
        Time.timeScale=0;
    }

    void escapePressCheck()
    {
        if (!deathMenuActive&&inLevel)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                resume();
            }
        }
    }

    public void onResumePress()
    {
        resume();
    }

    void resume()
    {
        pauseMenuActive=!pauseMenuActive;
        pauseMenu.SetActive(pauseMenuActive);
        Time.timeScale=-Time.timeScale+1;
    }

    public void onRespawnPress()
    {
        if (pauseMenuActive)
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("Countess"));
            resume();
        }
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        for(int i=0; i<projectiles.Length; i++)
        {
            Destroy(projectiles[i]);
        }


        Instantiate(Resources.Load("Prefabs/Countess"));
        Instantiate(Resources.Load("Prefabs/Levels/LevelTest"));
        Time.timeScale=1;
        deathMenu.SetActive(false);
        deathMenuActive=false;
    }
}