using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject endScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText;
    public TextMeshProUGUI endScreenTimeText;

    public GameObject pauseScreen;

    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
        UpdateTimeText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }

    public void UpdateTimeText()
    {
        timeText.text = "Time: " + GameManager.instance.totalTime.ToString("f2");
    }

    public void SetEndScreen(bool hasWon)
    {
        endScreen.SetActive(true);

        endScreenScoreText.text = "Score: " + GameManager.instance.score;
        endScreenTimeText.text = "Time: " + GameManager.instance.totalTime.ToString("f2");

        if (hasWon)
        {
            endScreenHeader.text = "You Win";
        }
        else
        {
            endScreenHeader.text = "Game Over";
        }
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuButton()
    {
        if (GameManager.instance.paused)
        {
            GameManager.instance.TogglePauseGame();
        }
        SceneManager.LoadScene(0);
    }

    public void TogglePauseScreen(bool paused)
    {
        pauseScreen.SetActive(paused);
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePauseGame();
    }
}
