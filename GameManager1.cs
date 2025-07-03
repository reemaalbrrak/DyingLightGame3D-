using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public enum GameMode { Easy, Medium, Hard }
    public GameMode currentMode = GameMode.Easy;

    public int totalEnemies;
    private int enemiesKilled = 0;

    public int playerHealth = 3;

    public Text killCountText;   // UI Text to show kills
    public Text healthText;      // UI Text to show health

    public GameObject winPanel;
    public GameObject losePanel;

    public static GameManager1 Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        enemiesKilled = 0;      // Reset kills at start
        playerHealth = 3;       // Reset health at start

        // قراءة وضع الصعوبة من مدير الصعوبة
        switch (DifficultyManager.SelectedDifficulty)
        {
            case DifficultyManager.DifficultyLevel.Easy:
                currentMode = GameMode.Easy;
                break;
            case DifficultyManager.DifficultyLevel.Medium:
                currentMode = GameMode.Medium;
                break;
            case DifficultyManager.DifficultyLevel.Hard:
                currentMode = GameMode.Hard;
                break;
        }

        UpdateKillCountUI();
        UpdateHealthUI();

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateKillCountUI();

        if (enemiesKilled >= totalEnemies)
        {
            ShowWinPanel();
        }
    }

    public void TakeDamage()
    {
        playerHealth--;
        UpdateHealthUI();

        if (playerHealth <= 0)
        {
            ShowLosePanel();
        }
    }

    void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + enemiesKilled + " / " + totalEnemies;
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + playerHealth;
        }
    }

    public void ShowLosePanel()
    {
        if (losePanel != null) losePanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        if (winPanel != null) winPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
