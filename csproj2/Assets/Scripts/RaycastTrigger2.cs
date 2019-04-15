using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class RaycastTrigger2 : MonoBehaviour
{
    public UnityEvent myEventEnter;
    GameObject raycast1;
    Vector3 raycastVector1;
    RaycastHit rayHit1;

    public GameObject detectedPlayer;
    DetectedHandler detectedPlayerHandler;


    // Use this for initialization
    void Start()
    {
        raycast1 = this.gameObject;
        detectedPlayerHandler = detectedPlayer.GetComponent<DetectedHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlayerHandler.winCondition)
        {
            raycastVector1 = raycast1.transform.TransformDirection(Vector3.forward) * 7;
            Debug.DrawRay(raycast1.transform.position, raycastVector1, Color.blue);
            Physics.Raycast(raycast1.transform.position, (raycastVector1), out rayHit1, 20f);

            if (rayHit1.collider != null && rayHit1.collider.name == "akai_e_espiritu")
            {
                enabled = false;
                myEventEnter.Invoke();
            }
        }

    }
}
