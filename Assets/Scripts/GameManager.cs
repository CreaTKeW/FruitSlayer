using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public IEnumerator myCoroutine;
    public fruitSpawner fSpawner;
    // GameUI elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    // EndGameUI elements
    public GameObject newHighscoreDisplay;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI endHighscoreText;
    // Full UI
    public GameObject MainMenu;
    public GameObject ScoreUI;
    public GameObject SettingsUI;
    public GameObject EndGameUI;
    public GameObject Spawner;
    // Storage values
    public int score = 0;
    public int highscore = 0;


    void Awake()
    {       
        fSpawner = fSpawner.GetComponent<fruitSpawner>();
        myCoroutine = fSpawner.SpawnFruits();
    }
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = highscore.ToString();
        endHighscoreText.text = highscore.ToString();

        newHighscoreDisplay.SetActive(false);
        MainMenu.SetActive(true);
        ScoreUI.SetActive(false);
        EndGameUI.SetActive(false);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        endScoreText.text = score.ToString();
        UpdateScoreBoard();
    }
    public void UpdateScoreBoard()
    {
        if(score > highscore)
        {            
            PlayerPrefs.SetInt("highscore", score);
            highscore = PlayerPrefs.GetInt("highscore");
            highscoreText.text = highscore.ToString();
            if (EndGameUI != null) newHighscoreDisplay.SetActive(true); 
        }
        else { return; }
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        ScoreUI.SetActive(true);
        StartCoroutine(myCoroutine);
    }

    public void GoToSettings()
    {
        MainMenu.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SettingsUI.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OnBombCollision()
    {
        StopCoroutine(myCoroutine);
        ScoreUI.SetActive(false);
        EndGameUI.SetActive(true);
        CleanScene();       
    }

    public void CleanScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits)
        { Destroy(fruit.gameObject); }

        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs) 
        { Destroy(bomb.gameObject); }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnApplicationQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            Application.Quit();
        #endif
    }
}
