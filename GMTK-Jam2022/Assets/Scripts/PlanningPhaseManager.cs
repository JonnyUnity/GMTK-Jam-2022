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
    private List<Roll> _dice = new List<Roll>();


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
            Roll newDice = Instantiate(_diePrefab);
            _dice.Add(newDice);
        }


    }


    public void RollDice()
    {
        foreach(var die in _dice)
        {
            die.RollDie();
        }


        _rollButton.SetActive(false);
        _goButton.SetActive(true);

    }


    public void StartAction()
    {
        // transition to the action phase...

        List<DiceSpawn> diceData = new List<DiceSpawn>();

        // record dice positions
        foreach (var die in _dice)
        {
            diceData.Add(die.GetDieData());
        }

        // spawn stuff based on their values...



    }


}
