using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    public GameObject levelChanger;
    LevelChanger levelChangerHandler;
    bool Fadein = false;


    // Use this for initialization
    void Start()
    {
        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();
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
        //else if (Input.GetButtonDown("Yes"))
        //{
        //    levelChangerHandler.FadeToLevel(2);
        //}

    }
}
