using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private AudioClip _musicClip;

    private void Awake()
    {
        int score = GameManager.Instance.Score;
        _scoreText.text = $"SCORE: {score}";

        if (_musicClip != null)
        {
            AudioManager.Instance.FadeMusicIn(_musicClip, 1f);
        }

    }


    public void GoToMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }


}
