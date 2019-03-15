using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattledroidHandler : MonoBehaviour
{

    public Transform player;
    Vector3 direction;

    public GameObject indicator1;
    bool indicator1Handler = false;

    private Animator anim;
    private NavMeshAgent mNavMeshAgent;

    Vector3 lastKnownPosition;
    bool isSearching = false;
    bool isPatroling = false;

    bool seeking = false;
    bool searching = false;
    bool searchPaused = false;

    float pathX;
    float upperPathX;
    float lowerPathX;
    float currentPosX;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        mNavMeshAgent = this.GetComponent<NavMeshAgent>();
        enabled = false;
    }

    public void PlayerDetect(bool option)
    {
        if (option)
        {
            Debug.Log("indicator1 on");
            indicator1.SetActive(true);
            indicator1Handler = true;
            isSearching = true;

            //direction = player.position - this.transform.position;
            lastKnownPosition = player.position;
        }
        else
        {
            Debug.Log("indicator1 off");
            indicator1.SetActive(false);
            indicator1Handler = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Search();
        
    }

    public void Search()
    {
        if (isSearching)
        {
            mNavMeshAgent.destination = lastKnownPosition;

            if (Vector3.Distance(lastKnownPosition, this.transform.position) < .5f)
            {
                anim.SetTrigger("isSearching");
                seeking = false;
                isSearching = false;

                if(indicator1Handler && !searchPaused)
                {
                    StartCoroutine(ResumeSearch());

                }
                else
                {
                    isSearching = false;
                }

            }
            else
            {
                seeking = true;
            }

            anim.SetBool("followPlayer", seeking);

        }
    }

    private IEnumerator ResumeSearch()
    {
        searchPaused = true;
        yield return new WaitForSeconds(2f);
        lastKnownPosition = player.position;
        searchPaused = false;
        isSearching = true;

    }

    public void Patrol()
    {


    }
}
