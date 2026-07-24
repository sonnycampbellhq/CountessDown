using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject deathMenu;
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
        Debug.Log("AHIUIASC");
        //other menus can't spawn
        //set floor and money text (take as params)
        deathMenu.SetActive(true);
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

    public void onRespawnPress()
    {
        //SceneManager.LoadScene("SampleScene");
        //delete everything in present scene
        //load in a prefab of the first level
        Destroy(GameObject.FindGameObjectWithTag("Level"));

        Instantiate(Resources.Load("Prefabs/Countess"));
        Instantiate(Resources.Load("Prefabs/Levels/LevelTest"));
        Time.timeScale=1;
        deathMenu.SetActive(false);
    }
}