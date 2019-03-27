using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    private void Start()
    {
        animator = this.GetComponent<Animator>();

        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        else
        {
            animator.updateMode = AnimatorUpdateMode.Normal;
            Time.timeScale = 1;
        }
        
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void FadeInLevel()
    {
        animator.SetTrigger("FadeIn");
    }

    public void OnFadeComplete()
    {
        Debug.Log("Loading new scene");
        SceneManager.LoadScene(levelToLoad);

    }
}
