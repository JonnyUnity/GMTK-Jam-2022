using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private GameObject _quitButton;


    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _quitButton.SetActive(false);
#endif
    }


    public void StartGame()
    {
        GameManager.Instance.StartGame();
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
