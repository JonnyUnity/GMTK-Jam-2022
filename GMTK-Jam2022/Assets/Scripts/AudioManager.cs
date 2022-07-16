using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void FadeMusicIn(AudioClip musicClip, float fadeDuration)
    {
        if (_audioSource.isPlaying && _audioSource.clip.name == musicClip.name)
            return;


        if (_audioSource.isPlaying && _audioSource.clip.name != musicClip.name)
        {
            FadeMusicOut(0.5f);
            _audioSource.Stop();
        } 

        _audioSource.clip = musicClip;
        _audioSource.Play();
        _audioSource.volume = 0;

        _audioSource.DOFade(80f, fadeDuration);


    }

    public void FadeMusicOut(float fadeDuration)
    {
        _audioSource.DOFade(0f, fadeDuration);
    }



}
