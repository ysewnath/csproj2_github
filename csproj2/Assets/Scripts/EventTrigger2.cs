using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2 : MonoBehaviour
{

    public UnityEvent myEvent;

    private void OnEnable()
    {
        Debug.Log("invoked event");
        myEvent.Invoke();
    }


}
