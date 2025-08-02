using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal bool gameIsActive = true;
    public TextMeshProUGUI scoreText;
    private int score = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        gameIsActive = true;
    }

    internal void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    internal void GameOver()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        gameIsActive = false;
    }
}
