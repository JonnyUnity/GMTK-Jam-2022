using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Object Event Channel")]
public class GOEventChannelSO : ScriptableObject
{

    public UnityAction<GameObject, int> OnEventRaised;

    public void RaiseEvent(GameObject obj, int score)
    {
        OnEventRaised?.Invoke(obj, score);
    }

}