using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    public GameObject levelChanger;
    LevelChanger levelChangerHandler;


    // Use this for initialization
    void Start()
    {
        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();
        levelChangerHandler.FadeInLevel();
    }

    // Update is called once per frame
    void Update()
    {

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
