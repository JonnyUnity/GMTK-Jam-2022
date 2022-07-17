using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeWarning : MonoBehaviour
{

    [SerializeField] private GameObject _flashingText;

    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _loadFloorChannelSO;
    [SerializeField] private EventChannelSO _gateOpenChannelSO;
    [SerializeField] private EventChannelSO _quitToMenuChannelSO;
    [SerializeField] private EventChannelSO _gameOverChannelSO;

    private void OnEnable()
    {
        _loadFloorChannelSO.OnEventRaised += HideText;
        _gateOpenChannelSO.OnEventRaised += ShowText;
        _quitToMenuChannelSO.OnEventRaised += HideText;
        _gameOverChannelSO.OnEventRaised += HideText;
    }


    private void OnDisable()
    {
        _loadFloorChannelSO.OnEventRaised -= HideText;
        _gateOpenChannelSO.OnEventRaised -= ShowText;
        _quitToMenuChannelSO.OnEventRaised -= HideText;
        _gameOverChannelSO.OnEventRaised -= HideText;
    }

    private void HideText()
    {
        _flashingText.SetActive(false);
    }

    private void ShowText()
    {
        _flashingText.SetActive(true);
    }
}
