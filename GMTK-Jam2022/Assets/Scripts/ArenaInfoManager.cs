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


    private void OnEnable()
    {
        _loadFloorChannelSO.OnEventRaised += UpdateFloorText;
        _scoreUpdatedChannelSO.OnEventRaised += UpdateScoreText;
    }

    private void OnDisable()
    {
        _loadFloorChannelSO.OnEventRaised -= UpdateFloorText;
        _scoreUpdatedChannelSO.OnEventRaised -= UpdateScoreText;
    }

    private void UpdateScoreText()
    {
        int score = GameManager.Instance.Score;
        _scoreText.text = $"SCORE: {score}";
    }

    private void UpdateFloorText()
    {
        _infoPanel.SetActive(true);

        int floor = GameManager.Instance.Floor;
        _floorText.text = $"FLOOR: {floor}";

        UpdateScoreText();

    }

    public void QuitGame()
    {
        _infoPanel.SetActive(false);
        _quitToMenuChannelSO.RaiseEvent();
        GameManager.Instance.QuitToMenu();
    }

}