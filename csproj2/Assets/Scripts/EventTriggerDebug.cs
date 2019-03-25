using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerDebug : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with: " + other.name);
    }
}
