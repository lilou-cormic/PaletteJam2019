using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText = null;

    [SerializeField]
    private TextMeshProUGUI BestText = null;

    private void Start()
    {
        ScoreText.text = "Score: 00000";
        BestText.text = "Best: " + ScoreManager.HighScore.ToString("00000");

        ScoreManager.ScoreChanged += ScoreManager_ScoreChanged;
    }

    private void ScoreManager_ScoreChanged()
    {
        ScoreText.text = "Score: " + ScoreManager.Score.ToString("00000"); ;
        BestText.text = "Best: " + ScoreManager.HighScore.ToString("00000");
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreChanged -= ScoreManager_ScoreChanged;
    }
}
