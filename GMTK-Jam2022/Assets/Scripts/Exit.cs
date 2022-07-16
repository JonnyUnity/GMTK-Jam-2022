using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    [SerializeField] private EventChannelSO _exitFloorChannelSO;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // exiting floor!
        
        _exitFloorChannelSO.RaiseEvent();
        GameManager.Instance.GoToNextFloor();
    }

}