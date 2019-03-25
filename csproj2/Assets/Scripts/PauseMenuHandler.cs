using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{

    public GameObject player;
    public GameObject detectedPlayer;
    public GameObject pauseMenu;
    DetectedHandler detectedPlayerHandler;
    MovementInput movementInputHandler;

    bool paused = false;

    // Use this for initialization
    void Start()
    {
        movementInputHandler = player.GetComponent<MovementInput>();
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        //
        // look for user input
        //
        if (Input.GetButtonDown("Pause") && !movementInputHandler.moveLock && !detectedPlayerHandler.isInteracting)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;

        }
        else if(paused)
        {
            if (Input.GetButtonDown("Escape"))
            {
                paused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
