using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Screens")]
    [Tooltip("The parent GameObject for the start screen.")]
    [SerializeField] private GameObject startScreen;
    
    [Tooltip("The parent GameObject for the game over screen.")]
    [SerializeField] private GameObject gameOverScreen;
    
    [Tooltip("The UI text displaying the current score during gameplay.")]
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Tooltip("The UI text displaying the final score on the game over screen.")]
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private int score = 0;
    private bool isGameActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 0f;
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1f; 
        
        startScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        if (!isGameActive) return;
        
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0f; 
        
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + score;
        }

        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}