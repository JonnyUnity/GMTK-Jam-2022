using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    [SerializeField] private GOEventChannelSO _removeObjectChannelSO;
    [SerializeField] private AudioClip _openChestClip;

    [SerializeField] private int _score = 500;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_openChestClip);
            _removeObjectChannelSO.RaiseEvent(gameObject, _score);
        }
    }
}
