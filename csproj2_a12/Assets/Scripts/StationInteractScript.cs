using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StationInteractScript : MonoBehaviour {

    public GameObject InteractUI;
    public GameObject DialogBox;
    public GameObject player;
    public CinemachineVirtualCamera vcam;
    MovementInput movementInput;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = player.GetComponent<Animator>();
        movementInput = player.GetComponent<MovementInput>();
        enabled = false;
    }

    private void Update()
    {
        //
        // look for user input
        //
        if(Input.GetButtonDown("Interact"))
        {
            // trigger station vcam and player kneel 
            anim.SetBool("isKneeling",true);
            movementInput.moveLock = true;
            InteractUI.SetActive(false);
            DialogBox.SetActive(true);

            vcam.Priority = 15;


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //
        // display interact UI
        //
        InteractUI.SetActive(true);
        enabled = true;

    }

    private void OnTriggerExit(Collider other)
    {
        //
        // close interact UI
        //
        InteractUI.SetActive(false);
        enabled = false;
    }
}
