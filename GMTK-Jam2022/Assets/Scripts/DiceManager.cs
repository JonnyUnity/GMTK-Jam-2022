using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    [Header("Dice Prefabs")]
    [SerializeField] private GameObject _standardDiePrefab;
    [SerializeField] private GameObject _spawnDiePrefab;

    [SerializeField] private Transform _diceSpawn;

    private int _numDice;
    private List<GameObject> _diceObjects = new List<GameObject>();
    private List<Roll> _rollingDice = new List<Roll>();

    private List<DiceSpawn> _diceData = new List<DiceSpawn>();

    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _setupDiceChannelSO;
    [SerializeField] private BoolEventChannelSO _rollDiceChannelSO;
    [SerializeField] private EventChannelSO _diceRolledChannelSO;
    [SerializeField] private EventChannelSO _startCombatChannelSO;


    [Header("Dice SO")]
    [SerializeField] private FloorDice _dice;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _diceRollClip;


    [Header("Dice Spawns")]
    [SerializeField] private Transform _spawnDieSpawn;
    [SerializeField] private Transform[] _diceSpawns;


    private WaitForSeconds _rollDelay = new WaitForSeconds(0.25f);


    private void OnEnable()
    {
        _setupDiceChannelSO.OnEventRaised += Setup;
        _rollDiceChannelSO.OnEventRaised += RollDice;
        _startCombatChannelSO.OnEventRaised += RemoveDice;
    }


    private void OnDisable()
    {
        _setupDiceChannelSO.OnEventRaised -= Setup;
        _rollDiceChannelSO.OnEventRaised -= RollDice;
        _startCombatChannelSO.OnEventRaised -= RemoveDice;
    }

    private void RemoveDice()
    {
        for (int i = _diceObjects.Count - 1; i >= 0; i--)
        {
            //Destroy(_diceObjects[i]);
            _diceObjects[i].SetActive(false);
        }
        _diceObjects.Clear();
    }


    public void Setup()
    {

        RemoveDice();

        for (int i = 0; i < _dice.Dice.Count; i++)
        {
            GameObject newDiceObject = Instantiate(_standardDiePrefab, _diceSpawns[i]);
            Roll roll = newDiceObject.GetComponent<Roll>();
            roll.Init(_dice.Dice[i]);
            _diceObjects.Add(newDiceObject);
            _rollingDice.Add(roll);

        }
        GameObject spawnDieObject = Instantiate(_spawnDiePrefab, _spawnDieSpawn);
        Roll spawnRoll = spawnDieObject.GetComponent<Roll>();
        spawnRoll.Init(_dice.SpawnDie);
        _diceObjects.Add(spawnDieObject);
        _rollingDice.Add(spawnRoll);


    }


    private void RollDice(bool reRoll = false)
    {
        if (reRoll)
        {
            
            Setup();
            //StartCoroutine(SetupWithDelay());

        }

        StartCoroutine(RollDiceCoroutine());
    }


    private IEnumerator RollDiceCoroutine()
    {

        foreach (var obj in _rollingDice)
        {
            obj.RollDie();
        }
        if (_diceRollClip != null)
        {
            _audioSource.PlayOneShot(_diceRollClip);
        }

        yield return new WaitUntil(() => GotAllDiceData());

        _diceRolledChannelSO.RaiseEvent();

    }


    private bool GotAllDiceData()
    {

        foreach (var die in _rollingDice)
        {
            if (!die.HasLanded)
            {
                return false;
            }
        }

        return true;

    }


}
