using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private FadeChannelSO _fadeChannelSO;

    private float _fadeDuration = 2f;
    private int _sceneIndex;
    private int _currentSceneIndex;

    private int _numDice = 1;
    private int _floor = 0;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        _currentSceneIndex = scene.buildIndex;
        _fadeChannelSO.FadeIn(_fadeDuration);

        if (_currentSceneIndex >= 3)
        {
            // we're in the game!
            LoadNextRoom();
        }


    }


    public void LoadSceneFromEditorStartup(int sceneIndex)
    {
        _sceneIndex = sceneIndex;

        StartCoroutine(UnloadPreviousScene());
    }


    private IEnumerator UnloadPreviousScene()
    {
        Time.timeScale = 1;
        _fadeChannelSO.FadeOut(_fadeDuration);

        yield return new WaitForSeconds(_fadeDuration);

        if (_currentSceneIndex > 1)
        {
            SceneManager.UnloadSceneAsync(_currentSceneIndex);
        }

        LoadNewScene();
    }


    private void LoadNewScene()
    {
        AsyncOperation handle = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);
    }


    public void LoadMainMenu()
    {
        _sceneIndex = 2;

        StartCoroutine(UnloadPreviousScene());
    }


    public void StartGame()
    {
        AudioManager.Instance.FadeMusicOut(0.5f);
        _sceneIndex = 3;

        StartCoroutine(UnloadPreviousScene());
    }



    public int LoadNextRoom()
    {
        _floor++;

        return _numDice;


    }

    public int GetNumDice(int floor)
    {
        return 1;
    }



}
