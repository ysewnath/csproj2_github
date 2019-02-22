using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLockScript : MonoBehaviour
{

    public GameObject player;
    MovementInput movementInput;

    // Use this for initialization
    void Start()
    {
        movementInput = player.GetComponent<MovementInput>();

    }

    private void OnEnable()
    {
        movementInput.moveLock = false;
        enabled = false;
    }

}
