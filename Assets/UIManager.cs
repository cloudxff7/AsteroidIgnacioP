using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI scoreTxt;
    [SerializeField] protected TextMeshProUGUI highScoreTxt;
    [SerializeField] protected TextMeshProUGUI livesTxt;

    public void UpdateLives(int lives)
    {
        livesTxt.text = $"Lives: {lives}";
    }
    public void UpdateScore(int score)
    {

        scoreTxt.text = $"{score}";
    }
    public void UpdateHighScore(int highScore)
    {
        highScoreTxt.text = $"{highScore}";
    }
}
