using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDie : Die
{

    public bool IsSpawnDie { get; private set; }


    public SpawnDie()
    {
        FaceValues = new int[] { 0, 0, 0, 0, 0, 0 };
    }

}
