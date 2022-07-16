using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSide : MonoBehaviour
{

    public bool OnGround { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            OnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            OnGround = false;
        }
    }


}
