using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLockScript : MonoBehaviour {

    public GameObject player;
    MovementInput movementInput;
    public bool toggleMovement;

	// Use this for initialization
	void Start () {
        movementInput = player.GetComponent<MovementInput>();

    }

    void Update()
    {
        if(toggleMovement)
        {
            movementInput.moveLock = false;
            enabled = false;
        }
        
    }

}
