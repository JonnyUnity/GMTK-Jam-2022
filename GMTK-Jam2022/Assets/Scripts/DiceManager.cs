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
    [SerializeField] private EventChannelSO _rollDiceChannelSO;
    [SerializeField] private EventChannelSO _diceRolledChannelSO;


    [Header("Dice SO")]
    [SerializeField] private FloorDice _dice;


    private WaitForSeconds _rollDelay = new WaitForSeconds(0.25f);


    private void OnEnable()
    {
        _rollDiceChannelSO.OnEventRaised += RollDice;   
    }


    private void OnDisable()
    {
        _rollDiceChannelSO.OnEventRaised -= RollDice;
    }





    private void RollDice()
    {
        StartCoroutine(RollDiceCoroutine());
    }



    private IEnumerator RollDiceCoroutine()
    {
        for (int i = _diceObjects.Count - 1; i >= 0; i--)
        {
            Destroy(_diceObjects[i]);
        }
        _diceObjects.Clear();
        var currTransform = _diceSpawn;

        foreach (var die in _dice.Dice)
        {

            GameObject newDiceObject = Instantiate(_standardDiePrefab, currTransform);
            Roll roll = newDiceObject.GetComponent<Roll>();
            roll.Init(die);
            _diceObjects.Add(newDiceObject);
            _rollingDice.Add(roll);

            currTransform.position = new Vector3(0, 10, currTransform.position.z + 1);

            //newDiceObject.RollDie();
           // yield return _rollDelay;
        }
        GameObject spawnDieObject = Instantiate(_spawnDiePrefab, currTransform);
        Roll spawnRoll = spawnDieObject.GetComponent<Roll>();
        spawnRoll.Init(_dice.SpawnDie);
        _diceObjects.Add(spawnDieObject);
        _rollingDice.Add(spawnRoll);
        //spawnDieObject.RollDie();


        foreach (var obj in _rollingDice)
        {
            obj.RollDie();
        }

        //yield return new WaitForSeconds(1f); // dice will always take some time to roll

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
