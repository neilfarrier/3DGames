using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float totalTime = 0f;
    public float startTime = 0f;

    public AudioSource gameMusic;
    public bool paused;

    public static GameManager instance;

    private void Awake()
    {
        gameMusic = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        startTime = Time.time;
        gameMusic.Play();
    }

    private void Update()
    {
        AddTime();
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        GameUI.instance.TogglePauseScreen(paused);
    }

    public void AddScore(int scoreToGive)
    {
        score += scoreToGive;
        GameUI.instance.UpdateScoreText();
    }

    public void AddTime()
    {
        totalTime += Time.deltaTime;
        GameUI.instance.UpdateTimeText();
    }

    public void LevelEnd()
    {
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            WinGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void WinGame()
    {
        GameUI.instance.SetEndScreen(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        GameUI.instance.SetEndScreen(false);
        Time.timeScale = 0f;
    }
}
