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
    [SerializeField] private GameObject _enemyPrefab;
 

    [Header("Dice data")]
    [SerializeField] private FloorDice _dice;

    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _startCombatChannelSO;


    private void OnEnable()
    {
        _startCombatChannelSO.OnEventRaised += SpawnLevelObjects;
    }


    private void OnDisable()
    {
        _startCombatChannelSO.OnEventRaised -= SpawnLevelObjects;
    }


    private void SpawnLevelObjects()
    {

        foreach (var die in _dice.Dice)
        {

            switch (die.Data.DieValue)
            {
                case 1:

                    Debug.Log("SPAWN ELITE!");
                    break;
                case 2:

                    Debug.Log("EMPTY?!");
                    break;

                case 3:

                    Debug.Log("SPAWN MOB!");
                    break;

                case 4:

                    Debug.Log("SPAWN 4!");
                    break;

                case 5:

                    Debug.Log("SPAWN 5!");
                    break;

                case 6:

                    Debug.Log("SPAWN LOOT!");
                    break;

            }
            
        }

        var playerPosition = _dice.SpawnDie.Data.Position;

        Debug.Log("SPAWN PLAYER!");

    }


}