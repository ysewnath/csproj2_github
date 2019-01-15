using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour {

    public GameObject station1;
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = station1.GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen", true);

    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen", false);
    }
}
