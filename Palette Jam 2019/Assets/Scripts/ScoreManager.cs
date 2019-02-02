using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }
    public static int HighScore { get; private set; }

    public static event Action ScoreChanged;

    private void Start()
    {
        Score = 0;
    }

    public static void AddPoints(int points)
    {
        Score += points;

        if (Score > HighScore)
            HighScore = Score;

        ScoreChanged?.Invoke();
    }
}
