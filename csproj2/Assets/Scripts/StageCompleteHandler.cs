using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageCompleteHandler : MonoBehaviour
{
    public GameObject levelChanger;
    LevelChanger levelChangerHandler;
    bool Fadein = false;

    [SerializeField]
    private SessionManager session;


    // Use this for initialization
    void Start()
    {
        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Fadein)
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
