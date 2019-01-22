using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigateHandler : MonoBehaviour
{

    public Transform target;
    Vector3 storeTarget;
    bool savePos;
    bool overrideTarget;

    Vector3 acceleration;
    Vector3 velocity;
    Quaternion zAxisRotation;

    public float maxSpeed = 5f;
    float storeMaxSpeed;
    float targetSpeed;


    Vector3 prevRotation;

    Rigidbody rigidbody;

    Vector3 currentRoll;
    float currentRollFloat = 0;


    public List<Vector3> EscapeDirections = new List<Vector3>();

    // Use this for initialization
    void Start()
    {

        storeMaxSpeed = maxSpeed;
        targetSpeed = storeMaxSpeed;

        rigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.DrawLine(transform.position, target.position);

        Vector3 forces = MoveTowardsTarget(target.position);

        acceleration = forces;
        velocity += 2 * acceleration * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // moves towards target
        rigidbody.velocity = velocity;

        // sets the rotation in the current direction
        Quaternion desiredRotation = Quaternion.LookRotation(velocity);

        // multiplying by 3 makes the rotation faster
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 3);


    }


    Vector3 MoveTowardsTarget(Vector3 target)
    {
        Vector3 distance = target - transform.position;

        // if too close to the target
        if (distance.magnitude < 25)
        {
            return distance.normalized * -maxSpeed;
        }
        else
        {
            return distance.normalized * maxSpeed;
        }

    }
}
