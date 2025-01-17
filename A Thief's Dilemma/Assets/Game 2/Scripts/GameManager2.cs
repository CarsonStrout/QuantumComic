using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private SoundFadeManager soundFadeManager;
    [SerializeField] private AudioSource song;

    [Space(5)]
    public float gameLength;

    private int pageLoad = 1;

    [HideInInspector] public bool loseGame;
    [HideInInspector] public bool gameComplete;
    [HideInInspector] public bool freeEnd;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameComplete = false;
        freeEnd = false;
    }

    private void Update()
    {
        if (loseGame) // sets game over ui
        {
            soundFadeManager.FadeAudio();
            gameOverText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(3);
        }

        if (gameComplete) // sends user to second page
            PlayerPrefs.SetInt("PageNumber", pageLoad);

        if (gameLength < 57 && gameLength > 56)
        {
            instructionText.SetText("GO!");
            if (!song.isPlaying)
                song.Play();
        }

        if (gameLength < 56)
            instructionText.SetText("");

        if (gameLength < 5) // stops spawning objects after 5 seconds left
            gameComplete = true;

        if (gameLength < 3) // moves characters to end positions
        {
            soundFadeManager.FadeAudio();
            freeEnd = true;
        }

        if (gameLength < 0)
            levelLoader.LoadNextLevel();

        if (gameLength > 0 && !loseGame) // will stop timer if player dies
        {
            gameLength -= Time.deltaTime;
            int rounded = (int)gameLength;
            timerText.text = rounded.ToString();
        }

    }
}
