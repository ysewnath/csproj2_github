using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent myEvent;

    public bool isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            myEvent.Invoke();
        }
    }

}
