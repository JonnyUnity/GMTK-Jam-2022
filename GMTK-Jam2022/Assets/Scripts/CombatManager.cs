using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{


    [Header("Prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _treasurePrefab;
    [SerializeField] private GameObject _elitePrefab;
    [SerializeField] private GameObject _skullPrefab;
    [SerializeField] private GameObject _skeletonPrefab;
 

    [Header("Dice data")]
    [SerializeField] private FloorDice _dice;


    [Header("Arena Objects")]
    [SerializeField] private GameObject _arenaObj;
    [SerializeField] private GameObject _walls;
    [SerializeField] private GameObject _gate;


    private List<GameObject> _spawnedObjects;
    

    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _startCombatChannelSO;
    [SerializeField] private EventChannelSO _exitFloorChannelSO;
    [SerializeField] private EventChannelSO _loadFloorChannelSO;
    [SerializeField] private EventChannelSO _checkFloorCompleteChannelSO;
    [SerializeField] private EventChannelSO _gateOpenChannelSO;


    private WaitForSeconds _pauseBetweenFloors = new WaitForSeconds(1f);


    private void OnEnable()
    {
        _startCombatChannelSO.OnEventRaised += SpawnLevelObjects;
        _exitFloorChannelSO.OnEventRaised += FloorCleared;
        _checkFloorCompleteChannelSO.OnEventRaised += CheckFloorCleared;
        //_openGateChannelSO.OnEventRaised += OpenGate;
    }


    private void OnDisable()
    {
        _startCombatChannelSO.OnEventRaised -= SpawnLevelObjects;
        _exitFloorChannelSO.OnEventRaised -= FloorCleared;
        _checkFloorCompleteChannelSO.OnEventRaised -= CheckFloorCleared;
        //_openGateChannelSO.OnEventRaised -= OpenGate;
    }



    private void SpawnLevelObjects()
    {

        _spawnedObjects = new List<GameObject>();

        foreach (var die in _dice.Dice)
        {

            var diePosition = die.Data.Transform.position;
            Debug.Log(die.Data.DieValue + " " + diePosition);
            GameObject newObj = null;

            switch (die.Data.DieValue)
            {
                case 1:

                    Debug.Log("SPAWN ELITE!");

                    //newObj = Instantiate(_elitePrefab, _arenaObj.transform, false);
                    break;
                case 2:

                    Debug.Log("EMPTY?!");
                    break;

                case 3:

                    Debug.Log("SPAWN SKULL!");
                    //newObj = Instantiate(_skullPrefab, _arenaObj.transform, false);
                    
                    break;

                case 4:

                    Debug.Log("SPAWN SKELETON!");
                   // newObj = Instantiate(_skeletonPrefab, _arenaObj.transform, false);
                    break;

                case 5:

                    Debug.Log("SPAWN SKELETON2!");
                    //newObj = Instantiate(_skeletonPrefab, _arenaObj.transform, false);
                    break;

                case 6:

                    Debug.Log("SPAWN TREASURE!");
                    break;

            }

            if (newObj != null)
            {
                newObj.transform.position = new Vector3(diePosition.x, diePosition.y, 0);
                _spawnedObjects.Add(newObj);
            }


        }

        Debug.Log("SPAWN PLAYER! " + _dice.SpawnDie.Data.Transform.position);
        Debug.Log("ARENA TRANSFORM " + _arenaObj.transform.position);
        var playerTransform = _dice.SpawnDie.Data.Transform;
        var playerObj = Instantiate(_playerPrefab, _arenaObj.transform, false);

        playerObj.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);

        Debug.Log(playerObj.transform.position);

        _spawnedObjects.Add(playerObj);

        _walls.SetActive(true);
        //_gate.SetActive(true);

        CheckFloorCleared();
                
    }


    public void FloorCleared()
    {
        StartCoroutine(FloorClearedCoroutine());
    }





    private IEnumerator FloorClearedCoroutine()
    {
        for (int i = _spawnedObjects.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedObjects[i]);
        }
        _spawnedObjects.Clear();
        _walls.SetActive(false);

        yield return _pauseBetweenFloors;

        //_loadFloorChannelSO.RaiseEvent();

        _gate.SetActive(true);

    }

    private void CheckFloorCleared()
    {
        if (_spawnedObjects.Count == 1)
        {
            // now only the player, so open the gate!
            //_openGateChannelSO.RaiseEvent(true);
            _gate.SetActive(false);
            _gateOpenChannelSO.RaiseEvent();
        }
    }


    //private void OpenGate(bool open)
    //{
    //    _gate.SetActive(!open);
    //}

}