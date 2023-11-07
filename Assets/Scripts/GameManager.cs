using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private adsManager adsManager;
    public IEnumerator spawnObjects;
    public fruitSpawner fSpawner;

    [Header("Audio elements")]
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioClip looseHealthSound;
    [SerializeField] private AudioClip bombAudioSound;


    [Header("InGame UI elements")]
    [SerializeField] private GameObject playerBlade;
    [SerializeField] private GameObject hearts;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;

    [Header("Endgame UI elements")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject newHighscoreDisplay;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI endHighscoreText;

    [Header("Complete UI objects")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject ScoreUI;
    [SerializeField] private GameObject SettingsUI;
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private GameObject Spawner;

    [Header("Stored values")]
    [SerializeField] private int score = 0;
    [SerializeField] private int highscore = 0;


    void Awake()
    {
        adsManager = FindObjectOfType<adsManager>();       
        mainAudioSource = GetComponent<AudioSource>();
        fSpawner = fSpawner.GetComponent<fruitSpawner>();
        spawnObjects = fSpawner.SpawnFruits();

        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
    }

    void Start()
    {
        // Gets the current highscore and displays it
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = highscore.ToString();
        endHighscoreText.text = highscore.ToString();

        // Sets all UI prefabs to default state
        playerBlade.SetActive(false);
        hearts.SetActive(false);
        SettingsUI.SetActive(false);
        newHighscoreDisplay.SetActive(false);
        MainMenu.SetActive(true);
        ScoreUI.SetActive(false);
        EndGameUI.SetActive(false);
    }

    public void IncreaseScore(int points) //Increase the score by the input value and update the scoreboard
    {        
        score += points;
        scoreText.text = score.ToString();
        endScoreText.text = score.ToString();
        UpdateScoreBoard();
    }
    private void UpdateScoreBoard()
    {
        if(score > highscore) // checks if the current score is higher than our highscore
        {            
            PlayerPrefs.SetInt("highscore", score);
            highscore = PlayerPrefs.GetInt("highscore");
            highscoreText.text = highscore.ToString();
            newHighscoreDisplay.SetActive(true); 
        }
        else { return; }
    }

    private void StartGame() 
    {
        playerBlade.SetActive(true);
        hearts.SetActive(true);
        MainMenu.SetActive(false);
        ScoreUI.SetActive(true);
        StartCoroutine(spawnObjects);
    }

    private void GoToSettings() 
    {
        MainMenu.SetActive(false);
        SettingsUI.SetActive(true);
    }

    private void BackToMainMenu() 
    {
        SettingsUI.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OnBombCollision() // This method is called once bomb gameobject is triggered
    {
        adsManager.LoadInterstitial();
        CleanScene();

        playerBlade.SetActive(false);
        hearts.SetActive(false);

        StopCoroutine(spawnObjects);
        StartCoroutine(ExplodeSequence());                                     
    }

    public void EndGame() // This method is called when player runs out of lives
    {
        adsManager.LoadInterstitial();
        CleanScene();
        StopCoroutine(spawnObjects);

        playerBlade.SetActive(false);
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

    public void RandomSliceSound() // plays a random clip from an array
    {
        AudioClip randomSound = clips[Random.Range(0, clips.Length)];
        mainAudioSource.PlayOneShot(randomSound);
    }

    private void CleanScene()
    {
        // Destroy all the objects that contain Fruit script
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits)
        { Destroy(fruit.gameObject); }

        // Destroy all the objects that contain Bomb script
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs) 
        { Destroy(bomb.gameObject); }
        
    }

    public void Restart()
    {
        // Need to change that
        SceneManager.LoadScene(0);
    }

    private IEnumerator ExplodeSequence()
    {

        float elapsed = 0f;
        float duration = .35f;

        // Changes from clear screen to white Image object
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

        // Changes from flash to clear screen
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
        #if UNITY_EDITOR // Exits the play state of the editor
            UnityEditor.EditorApplication.isPlaying = false;

        #else // When pressed exits the game
            Application.Quit();
        #endif
    }
}
