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
    [SerializeField] private GameObject _redSkeletonPrefab;
 

    [Header("Dice data")]
    [SerializeField] private FloorDice _dice;


    [Header("Arena Objects")]
    [SerializeField] private GameObject _arenaObj;
    [SerializeField] private GameObject _walls;
    [SerializeField] private GameObject _gate;
    [SerializeField] private GameObject _exitCollider;


    private GameObject _playerObj;
    private List<GameObject> _spawnedObjects;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _spawnObjectsClip;
    [SerializeField] private AudioClip _gateOpeningClip;


    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _startCombatChannelSO;
    [SerializeField] private EventChannelSO _exitFloorChannelSO;
    [SerializeField] private EventChannelSO _loadFloorChannelSO;
    [SerializeField] private EventChannelSO _checkFloorCompleteChannelSO;
    [SerializeField] private EventChannelSO _gateOpenChannelSO;
    [SerializeField] private GOEventChannelSO _removeObjectChannelSO;
    [SerializeField] private EventChannelSO _playerDiedChannelSO;
    [SerializeField] private EventChannelSO _gameOverChannelSO;


    private WaitForSeconds _pauseBetweenFloors = new WaitForSeconds(1f);


    private void OnEnable()
    {
        _startCombatChannelSO.OnEventRaised += SpawnLevelObjects;
        _exitFloorChannelSO.OnEventRaised += FloorCleared;
        _checkFloorCompleteChannelSO.OnEventRaised += CheckFloorCleared;
        _removeObjectChannelSO.OnEventRaised += RemoveSpawnedObject;
        _playerDiedChannelSO.OnEventRaised += PlayerDied;
    }


    private void OnDisable()
    {
        _startCombatChannelSO.OnEventRaised -= SpawnLevelObjects;
        _exitFloorChannelSO.OnEventRaised -= FloorCleared;
        _checkFloorCompleteChannelSO.OnEventRaised -= CheckFloorCleared;
        _removeObjectChannelSO.OnEventRaised -= RemoveSpawnedObject;
        _playerDiedChannelSO.OnEventRaised -= PlayerDied;
    }

    private void PlayerDied()
    {

        StartCoroutine(PlayerDiedCoroutine());


    }

    private IEnumerator PlayerDiedCoroutine()
    {
        // pause before changing scenes when the player dies...
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadGameOver();
    }


    private void RemoveSpawnedObject(GameObject obj, int score)
    {
        if (_spawnedObjects.Contains(obj))
        {
            GameManager.Instance.AddScore(score);
            _spawnedObjects.Remove(obj);
            Destroy(obj);
            CheckFloorCleared();
        }
    }

    private void SpawnLevelObjects()
    {

        _spawnedObjects = new List<GameObject>();
        _audioSource.PlayOneShot(_spawnObjectsClip);

        foreach (var die in _dice.Dice)
        {

            var diePosition = die.Data.Transform.position;
            Debug.Log(die.Data.DieValue + " " + diePosition);
            GameObject newObj = null;

            switch (die.Data.DieValue)
            {
                case 1:

                    Debug.Log("SPAWN ELITE!");

                    newObj = Instantiate(_elitePrefab, _arenaObj.transform, false);
                    break;
                case 2:

                    Debug.Log("SPAWN RED SKELETON");
                    newObj = Instantiate(_redSkeletonPrefab, _arenaObj.transform, false);
                    break;

                case 3:

                    Debug.Log("SPAWN SKULL!");
                    newObj = Instantiate(_skullPrefab, _arenaObj.transform, false);
                    
                    break;

                case 4:

                    Debug.Log("SPAWN SKELETON!");
                    newObj = Instantiate(_skeletonPrefab, _arenaObj.transform, false);
                    break;

                case 5:

                    Debug.Log("SPAWN EMPTY!");
                    break;

                case 6:

                    Debug.Log("SPAWN TREASURE!");
                    newObj = Instantiate(_treasurePrefab, _arenaObj.transform, false);
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
        _playerObj = Instantiate(_playerPrefab, _arenaObj.transform, false);

        _playerObj.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);

        
        Debug.Log(_playerObj.transform.position);

        _walls.SetActive(true);

        CheckFloorCleared();
                
    }

    private IEnumerator SpawnLevelObjectsCoroutine()
    {
        _spawnedObjects = new List<GameObject>();
        _audioSource.PlayOneShot(_spawnObjectsClip);

        foreach (var die in _dice.Dice)
        {

            var diePosition = die.Data.Transform.position;
            Debug.Log(die.Data.DieValue + " " + diePosition);
            GameObject newObj = null;

            switch (die.Data.DieValue)
            {
                case 1:

                    Debug.Log("SPAWN ELITE!");

                    newObj = Instantiate(_elitePrefab, _arenaObj.transform, false);
                    break;
                case 2:

                    Debug.Log("SPAWN RED SKELETON");
                    newObj = Instantiate(_redSkeletonPrefab, _arenaObj.transform, false);
                    break;

                case 3:

                    Debug.Log("SPAWN SKULL!");
                    newObj = Instantiate(_skullPrefab, _arenaObj.transform, false);

                    break;

                case 4:

                    Debug.Log("SPAWN SKELETON!");
                    newObj = Instantiate(_skeletonPrefab, _arenaObj.transform, false);
                    break;

                case 5:

                    Debug.Log("SPAWN EMPTY!");
                    break;

                case 6:

                    Debug.Log("SPAWN TREASURE!");
                    newObj = Instantiate(_treasurePrefab, _arenaObj.transform, false);
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
        _playerObj = Instantiate(_playerPrefab, _arenaObj.transform, false);

        _playerObj.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);


        Debug.Log(_playerObj.transform.position);

        _walls.SetActive(true);

        yield return new WaitForSeconds(1.5f);

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
        Destroy(_playerObj);

        _walls.SetActive(false);

        yield return _pauseBetweenFloors;

        //_loadFloorChannelSO.RaiseEvent();

        _gate.SetActive(true);
        _exitCollider.SetActive(false);

    }

    private void CheckFloorCleared()
    {
        if (_spawnedObjects.Count == 0)
        {
            // now only the player, so open the gate!
            _audioSource.PlayOneShot(_gateOpeningClip);

            _gate.SetActive(false);
            _exitCollider.SetActive(true);
            _gateOpenChannelSO.RaiseEvent();
        }
    }

}