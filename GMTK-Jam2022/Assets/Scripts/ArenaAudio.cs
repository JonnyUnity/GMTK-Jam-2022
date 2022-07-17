using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAudio : MonoBehaviour
{

    [SerializeField] private AudioClip _musicClip;


    // Start is called before the first frame update
    void Start()
    {
        if (_musicClip != null)
        {
            AudioManager.Instance.FadeMusicIn(_musicClip, 1f);
        }
    }

}