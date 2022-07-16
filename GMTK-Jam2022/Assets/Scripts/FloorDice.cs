using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Floor Dice")]
public class FloorDice : ScriptableObject
{
    public SpawnDie SpawnDie = new SpawnDie();
    public List<Die> Dice = new List<Die>();


    public void Add(Die die)
    {
        if (!Dice.Contains(die))
        {
            Dice.Add(die);
        }
    }


    public void Empty()
    {
        Dice.Clear();
    }


    public List<DiceSpawn> GetDiceData()
    {
        List<DiceSpawn> _diceData = new List<DiceSpawn>();

        foreach (var die in Dice)
        {
            _diceData.Add(die.Data);
        }

        return _diceData;

    }


}