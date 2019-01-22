using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDroidIntroScript : MonoBehaviour {

    public GameObject timeline;
    public GameObject player;
    Animator anim;
    bool isFinished = false;

    void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isFinished)
        {
            timeline.SetActive(true);
            StartCoroutine(toggleKneel());
            isFinished = true;
            enabled = false;
        }
        else
        {
            Debug.Log("hit sphere collider");
        }
        
    }

    private IEnumerator toggleKneel()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("isKneeling", true);

        yield return new WaitForSeconds(5f);
        anim.SetBool("isKneeling", false);


    }
}
