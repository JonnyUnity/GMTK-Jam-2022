using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _quitButton;


    private void Start()
    {
        AudioManager.Instance.FadeMusicIn(_mainMenuMusic, 1f);
#if UNITY_WEBGL && !UNITY_EDITOR
        _quitButton.SetActive(false);
#endif
    }


    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }


    public void Credits()
    {
        _credits.SetActive(true);
    }


    public void CloseCredits()
    {
        _credits.SetActive(false);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }


}
