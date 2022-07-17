using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    [SerializeField] private EventChannelSO _exitFloorChannelSO;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            _exitFloorChannelSO.RaiseEvent();
            GameManager.Instance.GoToNextFloor();
        }

    }

}