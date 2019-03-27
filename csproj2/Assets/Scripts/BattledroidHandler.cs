﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattledroidHandler : MonoBehaviour
{

    public Transform player;
    Vector3 direction;

    public GameObject indicator1;
    bool indicator1Handler = false;

    public GameObject detectedPlayer;
    DetectedHandler detectedPlayerHandler;

    private Animator anim;
    private NavMeshAgent mNavMeshAgent;

    Vector3 lastKnownPosition;
    Vector3 Battledrid_originalPosition;
    bool isSearching = false;
    bool isPatroling = false;
    public bool returnToPosition = false;
    bool returnToPositionFlag = false;

    bool seeking = false;
    bool returning = false;
    bool searching = false;
    bool searchPaused = false;

    float pathX;
    float upperPathX;
    float lowerPathX;
    float currentPosX;

    // Use this for initialization
    void Start()
    {
        //
        // battledroid is disabled by default
        //
        anim = this.GetComponent<Animator>();
        mNavMeshAgent = this.GetComponent<NavMeshAgent>();
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();
        Battledrid_originalPosition = this.transform.position;
        enabled = false;
    }

    public void PlayerDetect(bool option)
    {
        if (option)
        {
            //Debug.Log("indicator1 on");
            indicator1.SetActive(true);
            indicator1Handler = true;
            detectedPlayerHandler.detected = true;
            isSearching = true;

            //direction = player.position - this.transform.position;
            lastKnownPosition = player.position;
        }
        else
        {
            //Debug.Log("indicator1 off");
            indicator1.SetActive(false);
            indicator1Handler = false;
            detectedPlayerHandler.detected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlayerHandler.isInteracting)
        {
            detectedPlayerHandler.searchDetected = false;
            ReturnToOriginalPosition();
        }
        else if (isSearching)
        {
            detectedPlayerHandler.searchDetected = true;
            Search();
        }
        else if (returnToPosition)
        {
            detectedPlayerHandler.searchDetected = false;
            ReturnToOriginalPosition();
        }
        else if (isPatroling)
        {

        }


    }

    public void ColliderEnter()
    {
        //
        // increment detectedProgress
        //


    }

    public void ColliderStay()
    {

        //
        // increment detectedProgress
        //
        detectedPlayerHandler.detectedProgress++;


    }

    public void ColliderExit()
    {

    }

    public void ReturnToOriginalPosition()
    {
        mNavMeshAgent.destination = Battledrid_originalPosition;

        if (!returnToPositionFlag)
        {
            if (Vector3.Distance(Battledrid_originalPosition, this.transform.position) < .5f)
            {
                returnToPositionFlag = true;
                returning = false;
            }
            else
            {
                returning = true;
            }
            anim.SetBool("followPlayer", returning);

        }
        else
        {
            anim.SetBool("followPlayer", false);
        }


    }

    public void Search()
    {
        mNavMeshAgent.destination = lastKnownPosition;

        if (Vector3.Distance(lastKnownPosition, this.transform.position) < .5f)
        {
            anim.SetTrigger("isSearching");
            seeking = false;
            isSearching = false;

            if (indicator1Handler && !searchPaused)
            {
                StartCoroutine(ResumeSearch());

            }
            else
            {
                isSearching = false;
                returnToPositionFlag = false;
            }
        }
        else
        {
            seeking = true;
        }

        anim.SetBool("followPlayer", seeking);
    }

    private IEnumerator ResumeSearch()
    {
        searchPaused = true;
        yield return new WaitForSeconds(2f);
        lastKnownPosition = player.position;
        searchPaused = false;
        isSearching = true;

    }

    public void Patrol()
    {


    }
}
