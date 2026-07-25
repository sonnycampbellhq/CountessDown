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

    GameObject HUD;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Levels")
        {
            inLevel=true;
        }

        HUD=GameObject.FindGameObjectWithTag("HUD");
    }

  void Update()
    {
        escapePressCheck();
    }

  public void onStartPress()
    {
        SceneManager.LoadScene("Levels");
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

        if (floorsIn == -1)
        {
            menuTexts[2].text ="VICTORY!!";
            LevelManager.loadBlackBackground();
            HUD.SetActive(false);
        }
        menuTexts[3].text = $"Money\n\n{moneyIn}";
        menuTexts[4].text = $"Floors\n\n{10-floorsIn}";

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
        gameObject.GetComponent<LevelManager>().loadLevel(10);

        updateHUD(0, 10);
        updateHUD(1, 5);
        updateHUD(2, 0);
        deathMenu.SetActive(false);
        deathMenuActive=false;
    }

    public void updateHUD(int whichVal, int valueIn)
    {
        HUD.GetComponentsInChildren<TextMeshProUGUI>()[whichVal].text=valueIn.ToString();
    }
}