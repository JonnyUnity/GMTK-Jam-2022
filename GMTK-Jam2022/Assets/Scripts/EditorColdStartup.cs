using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorColdStartup : MonoBehaviour
{
    private int _thisSceneIndex;
    private bool _isColdStart;

    private void Awake()
    {
        //Debug.Log("LoadThisScene: " + SceneManager.GetActiveScene().name);
        _thisSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (!SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            _isColdStart = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_isColdStart)
        {
            SceneManager.LoadSceneAsync(1).completed += LoadThisScene;
        }
    }

    private void LoadThisScene(AsyncOperation obj)
    {
        
        GameManager.Instance.LoadSceneFromEditorStartup(_thisSceneIndex);
    }


    //private void LoadThisScene()
    //{
    //    SceneManager.LoadSceneAsync(_thisSceneIndex, LoadSceneMode.Additive);


    //}


}
