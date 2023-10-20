using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;

public class GameManager : MonoBehaviour
{
    public fruitSpawner fSpawner;
    // GameUI elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    // EndGameUI elements
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
    }
    void Start()
    {
        highscore = PlayerPrefs.GetInt("HIGHSCORE");
        highscoreText.text = highscore.ToString();
        endHighscoreText.text = highscore.ToString();

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
            PlayerPrefs.SetInt("HIGHSCORE", score);
            //highscore = PlayerPrefs.GetInt("HIGHSCORE");
            highscoreText.text = highscore.ToString();
            endHighscoreText.text = highscore.ToString();
        }
        else { return; }
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        ScoreUI.SetActive(true);
        Spawner.SetActive(true);
        StartCoroutine(fSpawner.SpawnFruits());
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
        ScoreUI.SetActive(false);
        EndGameUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
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
