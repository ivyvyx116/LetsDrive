using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        if (BGMManager.Instance() != null)
        {
            Destroy(BGMManager.Instance().gameObject.GetComponent<AudioSource>());
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        GamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        GamePaused = false;
    }
}
