using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaInfoManager : MonoBehaviour
{

    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private TextMeshProUGUI _floorText;
    [SerializeField] private TextMeshProUGUI _scoreText;


    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _loadFloorChannelSO;
    [SerializeField] private EventChannelSO _scoreUpdatedChannelSO;
    [SerializeField] private EventChannelSO _quitToMenuChannelSO;
    [SerializeField] private EventChannelSO _gameOverChannelSO;


    private void OnEnable()
    {
        _loadFloorChannelSO.OnEventRaised += UpdateFloorText;
        _scoreUpdatedChannelSO.OnEventRaised += UpdateScoreText;
        _gameOverChannelSO.OnEventRaised += HideInfo;
    }

    private void OnDisable()
    {
        _loadFloorChannelSO.OnEventRaised -= UpdateFloorText;
        _scoreUpdatedChannelSO.OnEventRaised -= UpdateScoreText;
        _gameOverChannelSO.OnEventRaised -= HideInfo;
    }

    private void Awake()
    {
        HideInfo();
    }

    private void HideInfo()
    {
        _infoPanel.SetActive(false);
    }

    private void UpdateScoreText()
    {
        int score = GameManager.Instance.Score;
        _scoreText.text = $"SCORE:{Environment.NewLine}{score}";
    }

    private void UpdateFloorText()
    {
        _infoPanel.SetActive(true);

        int floor = GameManager.Instance.Floor;
        _floorText.text = $"FLOOR:{Environment.NewLine}{floor}";

        UpdateScoreText();

    }

    public void QuitGame()
    {
        _infoPanel.SetActive(false);
        _quitToMenuChannelSO.RaiseEvent();
        GameManager.Instance.QuitToMenu();
    }

}