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

    [SerializeField]
    private SessionManager session;

    public GameObject whispersAudio;
    AudioSource whisperAudioHandler;

    float whispersVolume = 0;

    private void Start()
    {
        redFlareHandler = redFlare.GetComponent<ParticleSystem>();
        SlowmotionHandler = TimeHandler.GetComponent<TimeHandler>();
        whisperAudioHandler = whispersAudio.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameover && !startSlowmotion)
        {
            //
            // start slowmotion
            //
            session.gameover_detected = true;
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

            if (whisperAudioHandler.volume < 1)
            {
                whispersVolume = (detectedProgress / 2) / 100f;
                whisperAudioHandler.volume = whispersVolume;
                //Debug.Log("whispers volume: " + whispersVolume);
                //Debug.Log("whispers true volume: " + whisperAudioHandler.volume);

            }


        }
        else if (detectedProgress != 0)
        {
            detectedProgress--;
            Debug.Log("detected progress: " + detectedProgress);

            if (whisperAudioHandler.volume < 1)
            {
                whispersVolume = (detectedProgress / 2) / 100f;
                whisperAudioHandler.volume = whispersVolume;
                //Debug.Log("whispers volume: " + whispersVolume);
                //Debug.Log("whispers true volume: " + whisperAudioHandler.volume);

            }

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
