using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour
{

    public GameObject station1;
    Animator animator;
    public bool locked = false;
    public bool detected = false;

    public GameObject detectedPrompt;
    public GameObject detectedPlayer;
    DetectedHandler detectedPlayerHandler;

    // Use this for initialization
    void Start()
    {
        animator = station1.GetComponent<Animator>();
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectedPlayerHandler.detected || detectedPlayerHandler.searchDetected)
            detectedPrompt.SetActive(true);
        else if (!locked)
            animator.SetBool("isOpen", true);

    }

    private void OnTriggerStay(Collider other)
    {
        if (detectedPlayerHandler.detected || detectedPlayerHandler.searchDetected)
        {
            detectedPrompt.SetActive(true);
            animator.SetBool("isOpen", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        detectedPrompt.SetActive(false);
        animator.SetBool("isOpen", false);
    }
}
