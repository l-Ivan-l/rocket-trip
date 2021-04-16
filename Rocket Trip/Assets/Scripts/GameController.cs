using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public ObstaclesManager obstacles;
    public bool gameOver;
    public bool gameStarted;

    public GameObject titlePanel;
    public GameObject gameOverPanel;
    public GameObject instructionsPanel;

    public GameObject rocket;

    public string currentSafeStar;

    private int score = 0;
    public Text scoreText;
    public GameObject scoreUI;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreUI.GetComponent<Text>().text = "Score: " + score.ToString();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        rocket.SetActive(true);
        titlePanel.GetComponent<Animator>().SetTrigger("TitleOut");
        gameStarted = true;
        obstacles.StartObstacles();
        instructionsPanel.SetActive(true);
        scoreUI.SetActive(true);
    }

    public void GameOver()
    {
        scoreText.text = "Your Score: " + score.ToString();
        instructionsPanel.GetComponent<Animator>().SetTrigger("InstructionsOut");
        scoreUI.GetComponent<Animator>().SetTrigger("ScoreOut");
        gameOver = true;
        obstacles.CancelObstacles();
        gameOverPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
