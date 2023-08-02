using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    public TMP_Text scoreText;
    public TMP_Text highScoreText; // Add a reference to the high score text component
    public int currentScore = 0;
    public int highScore = 0; // Add a variable to store the high score

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load the high score from PlayerPrefs and update the high score text
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void IncreaseScore(int v)
    {
        currentScore += v;
        scoreText.text = "Score: " + currentScore.ToString();

        // Check if the current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "High Score: " + highScore.ToString();

            // Save the new high score to PlayerPrefs
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
