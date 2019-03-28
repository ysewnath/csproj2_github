﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameHandler : MonoBehaviour
{
    public GameObject levelChanger;
    LevelChanger levelChangerHandler;
    bool Fadein = false;

    [SerializeField]
    private SessionManager session;

    public GameObject reasonText;
    TextMeshProUGUI reasonTextHandler;


    // Use this for initialization
    void Start()
    {
        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();
        reasonTextHandler = reasonText.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 1;

        if(session.gameover_detected)
        {
            reasonTextHandler.text = "DETECTED BY DROIDS";
            session.gameover_detected = false;

        }
        else
        {
            reasonTextHandler.text = "TOO MANY INCORRECT STATIONS";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!Fadein)
        {
            levelChangerHandler.FadeInLevel();
            Fadein = true;
        }     

        if (Input.GetButtonDown("No"))
        {
            levelChangerHandler.FadeToLevel(1);

        }
        else if (Input.GetButtonDown("Yes"))
        {
            levelChangerHandler.FadeToLevel(2);
        }

    }
}
