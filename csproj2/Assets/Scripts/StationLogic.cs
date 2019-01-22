using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLogic : MonoBehaviour
{

    public GameObject questionStorage;
    QuestionGet questionGet;

    // Use this for initialization
    void Start()
    {
        questionGet = questionStorage.GetComponent<QuestionGet>();
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
