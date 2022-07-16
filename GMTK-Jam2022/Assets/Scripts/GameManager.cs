using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [Header("Event Channels")]
    [SerializeField] private FadeChannelSO _fadeChannelSO;
    [SerializeField] private EventChannelSO _loadFloorChannelSO;

    private float _fadeDuration = 2f;
    private int _sceneIndex;
    private int _currentSceneIndex;

    private int _numDice = 1;
    private int _numRerolls = 3;
    private int _floor = 1;
    private int _score = 0;


    public int Floor
    {
        get
        {
            return _floor;
        }
    }


    public int Score
    {
        get
        {
            return _score;
        }
    }


    public int RerollsRemaining
    {
        get
        {
            return _numRerolls;
        }
    }


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
            LoadArena();
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
        //AudioManager.Instance.FadeMusicOut(0.5f);
        _sceneIndex = 3;

        StartCoroutine(UnloadPreviousScene());
    }

    public void QuitToMenu()
    {
        _sceneIndex = 2;

        StartCoroutine(UnloadPreviousScene());
    }


    public int LoadArena()
    {
        

        _loadFloorChannelSO.RaiseEvent();
        return _numDice;
               

    }

    public int GetNumDice(int floor)
    {
        return 1;
    }



    public void UseReroll()
    {
        _numRerolls--;
    
    }

    public void GoToNextFloor()
    {
        _floor++;
        _score += 100;
    }


}