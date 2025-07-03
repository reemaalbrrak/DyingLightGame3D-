using UnityEngine;

public static class DifficultyManager
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    public static DifficultyLevel SelectedDifficulty = DifficultyLevel.Easy;
}
