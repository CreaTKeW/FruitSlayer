using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public IEnumerator spawnObjects;
    public fruitSpawner fSpawner;

    [Header("Audio elements")]
    private AudioSource mainAudioSource;
    public AudioClip[] clips;
    [SerializeField] private AudioClip looseHealthSound;
    [SerializeField] private AudioClip bombAudioSound;   


    [Header("InGame UI elements")]
    public GameObject hearts;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    [Header("Endgame UI elements")]
    public Image fadeImage;
    public GameObject newHighscoreDisplay;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI endHighscoreText;

    [Header("Complete UI objects")]
    public GameObject MainMenu;
    public GameObject ScoreUI;
    public GameObject SettingsUI;
    public GameObject EndGameUI;
    public GameObject Spawner;

    [Header("Stored values")]
    public int score = 0;
    public int highscore = 0;


    void Awake()
    {
        mainAudioSource = GetComponent<AudioSource>();
        fSpawner = fSpawner.GetComponent<fruitSpawner>();
        spawnObjects = fSpawner.SpawnFruits();
    }
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = highscore.ToString();
        endHighscoreText.text = highscore.ToString();

        hearts.SetActive(false);
        SettingsUI.SetActive(false);
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
        hearts.SetActive(true);
        MainMenu.SetActive(false);
        ScoreUI.SetActive(true);
        StartCoroutine(spawnObjects);
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
        CleanScene();
        hearts.SetActive(false);
        StopCoroutine(spawnObjects);
        StartCoroutine(ExplodeSequence());                                     
    }

    public void EndGame()
    {
        CleanScene();
        StopCoroutine(spawnObjects);

        ScoreUI.SetActive(false);
        EndGameUI.SetActive(true);
        hearts.SetActive(false);
    }

    public void lostHealthSound()
    {
        mainAudioSource.PlayOneShot(looseHealthSound);
    }

    public void explosionSound()
    {
        mainAudioSource.PlayOneShot(bombAudioSound);        
    }

    public void RandomSliceSound()
    {
        AudioClip randomSound = clips[Random.Range(0, clips.Length)];
        mainAudioSource.PlayOneShot(randomSound);
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

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = .35f;

        while(elapsed < duration)
        {
            float time = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, time);

            Time.timeScale = 1f - time;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
        elapsed = 0f;

        ScoreUI.SetActive(false);
        EndGameUI.SetActive(true);

        while (elapsed < duration)
        {
            float time = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, time);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
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
