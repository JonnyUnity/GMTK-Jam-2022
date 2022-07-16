using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitlializationLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AsyncOperation handle = SceneManager.LoadSceneAsync(1);
        handle.completed += ManagersLoaded;
    }


    private void ManagersLoaded(AsyncOperation obj)
    {

    }


}
