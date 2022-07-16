using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private FadeChannelSO _fadeChannelSO;
    [SerializeField] private Image _fadeOutRectange;


    private void OnEnable()
    {
        _fadeChannelSO.OnEventRaised += DoFade;
    }

    private void OnDisable()
    {
        _fadeChannelSO.OnEventRaised -= DoFade;
    }

    private void DoFade(bool fadeIn, float duration, Color color)
    {
        //_fadeOutRectange.DOBlendableColor(color, duration);
        float endValue = fadeIn ? 0 : 1f;
        _fadeOutRectange.DOFade(endValue, duration);
    }
}
