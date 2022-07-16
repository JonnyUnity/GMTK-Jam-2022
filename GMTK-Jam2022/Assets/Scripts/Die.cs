using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die
{

    public int[] FaceValues { get; protected set; }

    public DiceSpawn Data { get; set; }

    public Die()
    {
        FaceValues = new int[] { 6, 5, 4, 3, 2, 1 };
    }


    public Die(int[] faceValues)
    {
        FaceValues = faceValues;
    }


    public void SetRolledData(int value, Transform transform)
    {
        Data = new DiceSpawn
        {
            DieValue = value,
            Transform = transform
        };
        
    }

}