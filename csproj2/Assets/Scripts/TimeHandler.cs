using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimeHandler : MonoBehaviour
{

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    bool toggleSlowmotion = false;

    private IEnumerator coroutine;

    public GameObject detectedPlayer;
    DetectedHandler detectedPlayerHandler;

    //private void OnEnable()
    //{

    //    Debug.Log("starting slowmotion");
    //    StartCoroutine(StartSlowmotion());


    //}

    private void Start()
    {
        coroutine = StartSlowmotion();
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();
    }

    public void SlowmotionHandler(bool option)
    {
        if(option)
        {
            Debug.Log("starting slowmotion");
            StartCoroutine(coroutine);
        }
        else
        {
            //
            // stop the start slowmotion coroutine
            //
            StopCoroutine(coroutine);
            StartCoroutine(StopSlowmotion());
        }
        
    }

    private IEnumerator StopSlowmotion()
    {
        detectedPlayerHandler.isInteracting = true;
        Debug.Log("started slowmotion stop");
        float counter = 0;
        float duration = 5f; //seconds

        while (counter < duration)
        {
            Debug.Log(Time.timeScale);
            if (Time.timeScale > 0.5f)
            {
                Time.timeScale = 1;
                Debug.Log("break");
                break;
            }

            counter += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, counter / duration);
            yield return null;
        }

        if(Time.timeScale == 1)
        {
            Debug.Log("Timescale = 1");
        }
        else
        {
            Debug.Log("Timescale = " + Time.timeScale);
        }

        Debug.Log("finished slowmo stop");
        detectedPlayerHandler.isInteracting = false;

    }

    private IEnumerator StartSlowmotion()
    {
        detectedPlayerHandler.isInteracting = true;
        Debug.Log("started slowmotion");
        float counter = 0;
        float duration = 15f; //seconds

        while (counter < duration)
        {
            if (Time.timeScale < 0.05001f)
            {
                Debug.Log("break");
                break;
            }

            counter += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowdownFactor, counter / duration);
            yield return null;
        }

        counter = 0;
        duration = 10f; //seconds


        while (counter < duration)
        {
            if (Time.timeScale < 0.00001f)
            {
                Time.timeScale = 0.00001f;
                Debug.Log("break");
                break;
            }

            counter += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, counter / duration);
            yield return null;
        }

        Debug.Log("finished slowmo");
        detectedPlayerHandler.isInteracting = false;

    }
}
