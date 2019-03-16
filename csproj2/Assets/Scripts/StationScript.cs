using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour {

    public GameObject station1;
    Animator animator;
    public bool locked = false;

	// Use this for initialization
	void Start () {
        animator = station1.GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(!locked)
            animator.SetBool("isOpen", true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!locked)
            animator.SetBool("isOpen", false);
    }
}
