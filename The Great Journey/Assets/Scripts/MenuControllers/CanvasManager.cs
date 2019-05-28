using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public Text healthText;
    public Text experienceText;
    public Text levelText;
    public Text coinText;
    public Text shockCD;
    public Text fireCD;
    public Text lastScore;
    public GameObject pauseMenu;
    public GameObject intro;
    public GameObject gameLostScreen;
    Player player;
    Shooter shooter;
    bool pause = false;
    bool beggining = true;
    bool gameLost = false;

    void Start()
    {
        intro.SetActive(true);
        PauseGame();
        pauseMenu.SetActive(false);
        gameLostScreen.SetActive(false);
        player = PlayerManager.instance.player.GetComponent<Player>();
        shooter = PlayerManager.instance.player.GetComponent<Shooter>();
    }

    void Update()
    {
        if (gameLost == false)
        {
            if (beggining == true && Input.GetKeyDown(KeyCode.J))
            {
                beggining = false;
                intro.SetActive(false);
                ContinueGame();
            }
            healthText.text = "" + player.currentHealth + " \\ " + player.maxHealth;
            experienceText.text = "" + player.experience;
            levelText.text = "" + player.level;
            coinText.text = "" + player.coins;
            fireCD.text = "" + shooter.getFireCD();
            shockCD.text = "" + shooter.getShockCD();
            if (pause == false && Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
            else if (pause == true && Input.GetKeyDown(KeyCode.Escape))
            {
                ContinueGame();
            }
            else if (pause == true && Input.GetKeyDown(KeyCode.M))
            {
                MainMenu();
            }
            else if (pause == true && Input.GetKeyDown(KeyCode.L))
            {

                ExitGame();
            }
        }
        else if(gameLost == true)
        {
            Time.timeScale = 0;
            gameLostScreen.SetActive(true);
            lastScore.text = "" + player.coins;
            if(Input.GetKeyDown(KeyCode.J))
            {
                MainMenu();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                ExitGame();
            }
        }
    }

    public void PauseGame()
    {
        pause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }

    public void ContinueGame()
    {
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        //enable the scripts again
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameLost()
    {
        gameLost = true;
    }
}
