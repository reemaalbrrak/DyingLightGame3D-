using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public void SetEasy()
    {
        DifficultyManager.SelectedDifficulty = DifficultyManager.DifficultyLevel.Easy;
        Debug.Log("Selected: Easy");
    }

    public void SetMedium()
    {
        DifficultyManager.SelectedDifficulty = DifficultyManager.DifficultyLevel.Medium;
        Debug.Log("Selected: Medium");
    }

    public void SetHard()
    {
        DifficultyManager.SelectedDifficulty = DifficultyManager.DifficultyLevel.Hard;
        Debug.Log("Selected: Hard");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); 
    }
}
