using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public GameObject timeline;

    private void OnTriggerEnter(Collider other)
    {
        timeline.SetActive(true);
        enabled = false;
    }
}
