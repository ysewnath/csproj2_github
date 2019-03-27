using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedHandler : MonoBehaviour
{

    public bool detected = false;
    public bool searchDetected = false;
    public bool isInteracting = false;

    public int numCorrect = 0;
    public int numStations = 0;
    public int stationIndex = 0;

    public bool winCondition = false;

    // if detected Progress == 50, then play is detected, trigger game over
    public int detectedProgress = 0;

    //public bool pauseHandler = true;

    public GameObject findStations_Dialog;
    public GameObject findStations_collider;
    public GameObject redFlare;
    ParticleSystem redFlareHandler;
    bool toggleFlare = false;
    public bool gameover = false;

    Vector3 tempValue = new Vector3(0, 0.46298f, 0.46298f);
    ParticleSystem.EmissionModule emission;
    float tempVal = 0;
    bool startFlare = false;

    public GameObject TimeHandler;
    TimeHandler SlowmotionHandler;

    bool startSlowmotion = false;

    

    private void Start()
    {
        redFlareHandler = redFlare.GetComponent<ParticleSystem>();
        SlowmotionHandler = TimeHandler.GetComponent<TimeHandler>();      
    }

    private void Update()
    {
        if(gameover && !startSlowmotion)
        {
            //
            // start slowmotion
            //
            startSlowmotion = true;
            SlowmotionHandler.SlowmotionHandler(true);      
        }


        if (detected && !gameover)
        {
            Debug.Log("detected progress: " + detectedProgress);
            if (detectedProgress > 200)
            {

                //
                // game over
                //
                gameover = true;
            }

            //
            // adjust size of flare based on detectedProgress
            //
            if (detectedProgress > 20)
            {
                //
                // start timer
                //
                emission = redFlareHandler.emission;
                emission.rateOverTime = 5f;


            }


        }
        else if (detectedProgress != 0)
        {
            detectedProgress--;
            Debug.Log("detected progress: " + detectedProgress);

        }
        else if (detectedProgress == 0)
        {
            emission = redFlareHandler.emission;
            emission.rateOverTime = 0f;
            //
            // debug
            //
            //gameover = false;
        }
    }

    public void fullDetect()
    {
        gameover = true;

    }

    public void ShowRequirement(bool option)
    {
        Debug.Log("toggling FindStation prompt");
        if (option)
            findStations_Dialog.SetActive(true);
        else
            findStations_Dialog.SetActive(false);


    }


}
