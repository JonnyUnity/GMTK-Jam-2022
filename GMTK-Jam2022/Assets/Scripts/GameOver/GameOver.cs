using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        int score = GameManager.Instance.Score;

        _scoreText.text = $"SCORE: {score}";
    }


    public void GoToMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }


}
