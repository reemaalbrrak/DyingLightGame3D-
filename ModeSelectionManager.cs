using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelectionManager : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button playButton;

    private DifficultyManager.DifficultyLevel selectedMode;

    void Start()
    {
        playButton.interactable = false;

        easyButton.onClick.AddListener(() => SelectGameMode(DifficultyManager.DifficultyLevel.Easy));
        mediumButton.onClick.AddListener(() => SelectGameMode(DifficultyManager.DifficultyLevel.Medium));
        hardButton.onClick.AddListener(() => SelectGameMode(DifficultyManager.DifficultyLevel.Hard));

        playButton.onClick.AddListener(StartGame);
    }

    void SelectGameMode(DifficultyManager.DifficultyLevel mode)
    {
        selectedMode = mode;
        DifficultyManager.SelectedDifficulty = mode;
        playButton.interactable = true;
        Debug.Log("Selected Mode: " + mode);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
