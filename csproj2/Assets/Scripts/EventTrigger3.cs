using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger3 : MonoBehaviour
{
    public UnityEvent myEventEnter;
    public UnityEvent myEventExit;

    public bool isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger && other.name == "akai_e_espiritu")
        {
            myEventEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTrigger && other.name == "akai_e_espiritu")
        {
            myEventExit.Invoke();
        }
    }

}
