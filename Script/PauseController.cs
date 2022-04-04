using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    public static bool Paused = false;
    [SerializeField] GameObject menu, deathScreen, victoryScreen;

    public GameObject questCanvas, actionCanvas, weaponCanvas;

    public PlayerHealthManager health;
    public BossHealthManager bossHealth;


    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){

            if(Paused){
                resume();
            } else {
                pause();
            }
        }

        if(health.health <= 0)
        {
            dead();
        }

        if(bossHealth.isActiveAndEnabled && bossHealth.health <= 0)
        {
            victory();
        }
    }

    public void pause()
    {
        questCanvas.SetActive(false);
        actionCanvas.SetActive(false);
        weaponCanvas.SetActive(false);
        menu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.visible = true;
    }

    public void dead()
    {
        questCanvas.SetActive(false);
        actionCanvas.SetActive(false);
        weaponCanvas.SetActive(false);
        deathScreen.SetActive(true);
        Cursor.visible = true;
    }

    public void victory()
    {
        questCanvas.SetActive(false);
        actionCanvas.SetActive(false);
        weaponCanvas.SetActive(false);
        victoryScreen.SetActive(true);
        Cursor.visible = true;
    }

    public void quit(){
        menu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void resume()
    {
        questCanvas.SetActive(true);
        actionCanvas.SetActive(true);
        weaponCanvas.SetActive(true);
        menu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.visible = false;
    }
}
