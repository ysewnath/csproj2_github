using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.EventSystems;

public class MenuIntroScript : MonoBehaviour
{

    public GameObject IntroScreen;
    Image screen;

    public GameObject header1;
    public GameObject header2;
    TextMeshProUGUI header1Handler;
    TextMeshProUGUI header2Handler;

    Ray ray;

    public GameObject tutorialPrompt;
    public GameObject tutorial_yesBtn;
    public GameObject tutorial_noBtn;
    public GameObject loginPrompt;
    TextMeshProUGUI tutorial_yesBtnHandler;
    TextMeshProUGUI tutorial_noBtnHandler;

    public List<GameObject> menu_text;
    List<TextMeshProUGUI> menu_textHandler = new List<TextMeshProUGUI>();

    public CinemachineVirtualCamera vcam8;

    public GameObject menuStartTimeline;
    bool menu = false;
    int currentSelection = 0;

    Color whiteText = new Color(255, 255, 255);
    Color greyText = new Color(135, 135, 135);

    bool tutorial = false;

    // Use this for initialization
    void Start()
    {
        screen = IntroScreen.GetComponent<Image>();
        header1Handler = header1.GetComponent<TextMeshProUGUI>();
        header2Handler = header2.GetComponent<TextMeshProUGUI>();
        tutorial_yesBtnHandler = tutorial_yesBtn.GetComponent<TextMeshProUGUI>();
        tutorial_noBtnHandler = tutorial_noBtn.GetComponent<TextMeshProUGUI>();

        foreach (var option in menu_text)
        {
            menu_textHandler.Add(option.GetComponent<TextMeshProUGUI>());
        }

        enabled = false;
        menuStartTimeline.SetActive(true);
        StartCoroutine(MenuHold());

    }

    //private void Update()
    //{
    //    //
    //    // look for user input
    //    //


    //    if (Input.GetButtonDown("Left"))
    //    {
    //        if (currentSelection < 0)
    //        {
    //            Debug.Log("warning, current selection is out of bounds, < 0");
    //        }
    //        else if(tutorial)
    //        {
    //            tutorial_noBtnHandler.color = new Color32(135, 135, 135, 255);
    //            tutorial_yesBtnHandler.color = new Color32(255, 255, 255, 255);

    //        }
    //        else if (currentSelection != 0)
    //        {
    //            // prev selection
    //            currentSelection--;
    //            menu_textHandler[currentSelection + 1].color = new Color32(135, 135, 135,255);
    //            //menu_textHandler[currentSelection + 1].UpdateFontAsset();
    //            menu_textHandler[currentSelection].color = new Color32(255, 255, 255,255);
    //            Debug.Log("currentSelection = " + currentSelection);
    //        }
    //    }
    //    else if (Input.GetButtonDown("Right"))
    //    {
    //        if (currentSelection > 2)
    //        {
    //            Debug.Log("warning, current selection is out of bounds, > 2");
    //        }
    //        else if (tutorial)
    //        {
    //            tutorial_noBtnHandler.color = new Color32(255, 255, 255, 255);
    //            tutorial_yesBtnHandler.color = new Color32(135, 135, 135, 255);

    //        }
    //        else if (currentSelection != 2)
    //        {
    //            // next selection
    //            currentSelection++;
    //            menu_textHandler[currentSelection - 1].color = new Color32(135, 135, 135,255);
    //            //menu_textHandler[currentSelection - 1].UpdateFontAsset();
    //            menu_textHandler[currentSelection].color = new Color32(255, 255, 255,255);
    //            Debug.Log("currentSelection = " + currentSelection);
    //        }
    //    }
    //    else if (Input.GetButtonDown("Enter"))
    //    {
    //        if(currentSelection == 0)
    //        {
    //            // start
    //            // display tutorial prompt
    //            //tutorialPrompt.SetActive(true);
    //            loginPrompt.SetActive(true);
    //            //tutorial = true;
    //            vcam8.Priority = 17;
    //        }

    //    }
    //    else if (Input.GetButtonDown("Escape"))
    //    {

    //    }
    //}

    public void yeet(int test)
    {



    }

    public IEnumerator MenuHold()
    {

        yield return new WaitForSeconds(5f);
        enabled = true;
    }

}
