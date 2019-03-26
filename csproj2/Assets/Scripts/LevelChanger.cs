using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    private int levelToLoad;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
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
