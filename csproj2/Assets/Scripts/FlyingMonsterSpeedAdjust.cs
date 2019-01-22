using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsterSpeedAdjust : MonoBehaviour {

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        animator.speed = 0.2f;
	}
	
}
