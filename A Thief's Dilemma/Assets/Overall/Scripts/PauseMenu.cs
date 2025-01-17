using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [Header("References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private LevelLoader levelLoader;

    [Space(5)]
    [SerializeField] private int pageLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GameIsPaused = true;
    }

    public void LoadComic()
    {
        PlayerPrefs.SetInt("PageNumber", pageLoad);
        levelLoader.LoadNextLevel();
        GameIsPaused = false;
    }
}
