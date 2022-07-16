using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlanningPhaseManager : MonoBehaviour
{

    [SerializeField] private GameObject _rollButton;
    [SerializeField] private GameObject _goButton;
    [SerializeField] private Roll _diePrefab;

    private int _numDice;


    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _rollDiceChannelSO;
    [SerializeField] private EventChannelSO _diceRolledChannelSO;
    [SerializeField] private EventChannelSO _startCombatChannelSO;
 
    [Header("Dice SO")]
    [SerializeField] private FloorDice _dice;

    private void OnEnable()
    {
        _diceRolledChannelSO.OnEventRaised += DiceRolled;
    }

    private void OnDisable()
    {
        _diceRolledChannelSO.OnEventRaised -= DiceRolled;
    }

    private void DiceRolled()
    {
        Debug.Log("Dice Rolled!");

        _rollButton.SetActive(false);
        _goButton.SetActive(true);

    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Init(1);
         
    }




    public void Init(int floor)
    {
        // The floor determines how many dice are available (also what those dice are?)

        _numDice = GameManager.Instance.GetNumDice(floor);

        for (int i = 0; i < _numDice; i++)
        {
            Die newDie = new Die();
            _dice.Add(newDie);
        }


    }


    public void RollDice()
    {
        _rollDiceChannelSO.RaiseEvent();
    }


    public void StartCombat()
    {
        _startCombatChannelSO.RaiseEvent();
    }


}