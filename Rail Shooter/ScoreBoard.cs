using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private int score = 0;
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = score.ToString();
    }
}
