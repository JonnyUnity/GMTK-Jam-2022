using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanningPhaseManager : MonoBehaviour
{

    [SerializeField] private GameObject _rollButton;
    
    [SerializeField] private GameObject _goButton;

    [SerializeField] private GameObject _rerollPanel;
    [SerializeField] private GameObject _rerollButton;
    [SerializeField] private TextMeshProUGUI _remainingText;

    [SerializeField] private GameObject _escapeWarning;

    [SerializeField] private GameObject _buttonContainer;
    [SerializeField] private GameObject _dicePanel;

    private int _numDice;


    [Header("Event Channels")]
    [SerializeField] private EventChannelSO _loadFloorChannelSO;
    [SerializeField] private EventChannelSO _setupDiceChannelSO;
    [SerializeField] private BoolEventChannelSO _rollDiceChannelSO;
    [SerializeField] private EventChannelSO _diceRolledChannelSO;
    [SerializeField] private EventChannelSO _startCombatChannelSO;
    [SerializeField] private EventChannelSO _quitToMenuChannelSO;
    [SerializeField] private EventChannelSO _gameOverChannelSO;
 
    [Header("Dice SO")]
    [SerializeField] private FloorDice _dice;

    private void OnEnable()
    {
        _loadFloorChannelSO.OnEventRaised += LoadFloor;
        _diceRolledChannelSO.OnEventRaised += DiceRolled;
        _quitToMenuChannelSO.OnEventRaised += HideControls;
        _gameOverChannelSO.OnEventRaised += HideControls;
    }

    private void OnDisable()
    {
        _loadFloorChannelSO.OnEventRaised -= LoadFloor;
        _diceRolledChannelSO.OnEventRaised -= DiceRolled;
        _quitToMenuChannelSO.OnEventRaised -= HideControls;
        _gameOverChannelSO.OnEventRaised -= HideControls;
    }

    

    private void HideControls()
    {
        _dicePanel.SetActive(false);
        _buttonContainer.SetActive(false);
        _rerollPanel.SetActive(false);
    }

    private void LoadFloor()
    {
        // get number of dice!
        _buttonContainer.SetActive(true);
        _rollButton.SetActive(true);
        _goButton.SetActive(false);
        _escapeWarning.SetActive(false);

        _dicePanel.SetActive(true);
        //_remainingText.gameObject.SetActive(false);

        _rerollPanel.SetActive(false);

        //SetRerollDetail();

        Init();

        // now load dice!
        _setupDiceChannelSO.RaiseEvent();


    }


    private void SetRerollDetail()
    {
        _rerollPanel.SetActive(true);

        int rerollsRemaining = GameManager.Instance.RerollsRemaining;

        _rerollButton.SetActive(rerollsRemaining > 0);
        _remainingText.gameObject.SetActive(true);
        _remainingText.text = $"Rerolls: {rerollsRemaining}";
    }


    private void DiceRolled()
    {
        Debug.Log("Dice Rolled!");

        _rollButton.SetActive(false);

        SetRerollDetail();
        _goButton.SetActive(true);

    }

    private void Awake()
    {
        //_buttonContainer.SetActive(false);
        //_dicePanel.SetActive(false);

        HideControls();
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    LoadFloor();
         
    //}




    public void Init()
    {
        // The floor determines how many dice are available (also what those dice are?)

        _dice.Empty();
        _numDice = GameManager.Instance.GetNumDice();

        for (int i = 0; i < _numDice; i++)
        {
            Die newDie = new Die();
            _dice.Add(newDie);
        }


    }


    public void RollDice()
    {
        _dicePanel.SetActive(false);
        _rollButton.SetActive(false);
        _rollDiceChannelSO.RaiseEvent(false);
    }


    public void RerollDice()
    {

        GameManager.Instance.UseReroll();
        SetRerollDetail();

        _rerollButton.SetActive(false);
        _goButton.SetActive(false);
        _rollDiceChannelSO.RaiseEvent(true);
    }

    public void StartCombat()
    {
        // deactivate/hide roll buttons when combat starts!
        _rerollPanel.SetActive(false);
        _goButton.SetActive(false);



        _startCombatChannelSO.RaiseEvent();
    }


}