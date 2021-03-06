﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText = null;

    [SerializeField]
    private TextMeshProUGUI BestText = null;

    private void Awake()
    {
        ScoreText.text = "Score: 00000";
        BestText.text = "Best: 00000";
    }

    private void Start()
    {
        SetScoreTexts();

        ScoreManager.ScoreChanged += ScoreManager_ScoreChanged;
    }

    private void ScoreManager_ScoreChanged()
    {
        SetScoreTexts();
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreChanged -= ScoreManager_ScoreChanged;
    }

    private void SetScoreTexts()
    {
        ScoreText.text = "Score: " + ScoreManager.Score.ToString("00000");
        BestText.text = "Best: " + ScoreManager.HighScore.ToString("00000");
    }
}
