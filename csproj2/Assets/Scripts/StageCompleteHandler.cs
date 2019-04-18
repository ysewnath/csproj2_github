using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.Networking;

public class StageCompleteHandler : MonoBehaviour
{
    public GameObject levelChanger;
    LevelChanger levelChangerHandler;
    bool Fadein = false;

    [SerializeField]
    private SessionManager session;

    public List<QuestionModel> IncorrectQuestions = new List<QuestionModel>();

    int index = 0;

    public GameObject word_text;
    TextMeshProUGUI word_textHandler;

    public GameObject translation_text;
    TextMeshProUGUI translation_textHandler;

    public GameObject wordsMissed_text;
    TextMeshProUGUI wordsMissed_textHandler;

    public GameObject Stations_completed_text;
    TextMeshProUGUI Stations_completed_textHandler;


    // Use this for initialization
    void Start()
    {
        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();
        Time.timeScale = 1;

        if (session.offlineMode)
        {
            OutputLogFile();
        }
        else
        {
            StartCoroutine(OutputStatsToDatabase());
        }

        IncorrectQuestions = session.IncorrectQuestions;
        session.IncorrectQuestions = null;

        word_textHandler = word_text.GetComponent<TextMeshProUGUI>();
        translation_textHandler = translation_text.GetComponent<TextMeshProUGUI>();
        wordsMissed_textHandler = wordsMissed_text.GetComponent<TextMeshProUGUI>();
        Stations_completed_textHandler = Stations_completed_text.GetComponent<TextMeshProUGUI>();

        Stations_completed_textHandler.text = $"STATIONS COMPLETED: {session.numCorrect}/{session.numStations}";
        wordsMissed_textHandler.text = "WORDS MISSED: " + IncorrectQuestions.Count.ToString();

        if (IncorrectQuestions.Count == 0)
        {
            word_textHandler.text = "N/A";
            translation_textHandler.text = "N/A";

        }
        else
        {
            word_textHandler.text = IncorrectQuestions[index].Question;
            translation_textHandler.text = ValidateCorrectAnswer();
        }

    }

    IEnumerator OutputStatsToDatabase()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>()
        {
            new MultipartFormFileSection("userID",session.id.ToString()),
            new MultipartFormFileSection("deck_ID",session.selectedDeck.id.ToString()),
            new MultipartFormFileSection("correct",session.CorrectQuestions.Count.ToString()),
            new MultipartFormFileSection("incorrect",session.IncorrectQuestions.Count.ToString()),
            new MultipartFormFileSection("score",session.score.ToString())
        };

        string statsURL = "endlesslearner.com/insertstats";
        UnityWebRequest www = UnityWebRequest.Post(statsURL, formData);
        yield return www.SendWebRequest();
    }

    public void OutputLogFile()
    {
        UserModel offlineUser = new UserModel();
        offlineUser.DeckID = session.offlineDeck;
        offlineUser.Score = session.score;
        offlineUser.User = session.username;
        offlineUser.StationsCorrect = session.numCorrect;
        offlineUser.QuestionsCorrect = session.CorrectQuestions.Count;
        offlineUser.QuestionsWrong = session.IncorrectQuestions.Count;



        using (StreamWriter logFile = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\ProjectElle\\LogFile.txt", true))
        {
            logFile.WriteLine("----------------------------");
            logFile.WriteLine("session: " + DateTime.Now);
            logFile.WriteLine("user: " + offlineUser.User);
            logFile.WriteLine("score: " + offlineUser.Score);
            logFile.WriteLine("stations correct: " + offlineUser.StationsCorrect);
            logFile.WriteLine("questions correct: " + offlineUser.QuestionsCorrect);
            logFile.WriteLine("questions wrong: " + offlineUser.QuestionsWrong);
            logFile.WriteLine("----------------------------");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Fadein)
        {
            levelChangerHandler.FadeInLevel();
            Fadein = true;
        }

        if (Input.GetButtonDown("No"))
        {
            levelChangerHandler.FadeToLevel(1);

        }
        else if (Input.GetButtonDown("Yes"))
        {
            if (session.tutorial)
            {
                levelChangerHandler.FadeToLevel(2);
            }
            else
            {
                levelChangerHandler.FadeToLevel(6);
            }

        }
        else if (Input.GetButtonDown("Left"))
        {
            if (IncorrectQuestions.Count != 0)
            {
                if(index != 0)
                {
                    index--;
                    word_textHandler.text = IncorrectQuestions[index].Question;
                    translation_textHandler.text = ValidateCorrectAnswer();
                }


            }
        }
        else if (Input.GetButtonDown("Right"))
        {
            if (IncorrectQuestions.Count != 0)
            {
                if (index != IncorrectQuestions.Count - 1)
                {
                    index++;
                    word_textHandler.text = IncorrectQuestions[index].Question;
                    translation_textHandler.text = ValidateCorrectAnswer();
                }


            }
        }

    }

    public string ValidateCorrectAnswer()
    {
        switch (IncorrectQuestions[index].CorrectOption)
        {
            case 0:
                return IncorrectQuestions[index].OptionA;
            case 1:
                return IncorrectQuestions[index].OptionB;
            case 2:
                return IncorrectQuestions[index].OptionC;
            case 3:
                return IncorrectQuestions[index].OptionD;
        }
        return null;
    }
}
