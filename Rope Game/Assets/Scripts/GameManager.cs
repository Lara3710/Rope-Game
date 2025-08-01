using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal bool gameIsActive = true;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void StartGame()
    {
        gameIsActive = true;
    }

    internal void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    internal void GameOver()
    {
        gameIsActive = false;
    }
}
