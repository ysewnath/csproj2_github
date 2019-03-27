using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger4 : MonoBehaviour
{
    public UnityEvent myEventStay;

    public bool isTrigger = true;

    private void OnTriggerStay(Collider other)
    {
        if (isTrigger && other.name == "akai_e_espiritu")
        {
            myEventStay.Invoke();
        }
    }

}
