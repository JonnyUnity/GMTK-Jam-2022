using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioClip _mainMenuMusic;



    private void Start()
    {
        
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
