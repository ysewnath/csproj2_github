using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class QuestionGet : MonoBehaviour
{

    public List<List<QuestionModel>> questionSets = new List<List<QuestionModel>>();
    Queue<List<QuestionModel>> questionSetsQueue = new Queue<List<QuestionModel>>();
    public List<GameObject> stations;
    StationInteractScript stationInteractScript;

    [SerializeField]
    private SessionManager session;

    private LanguagePackInterface languagePackInterface;

    private List<string> Buffer = new List<string>();
    private System.Random r = new System.Random();
    private System.Random r2 = new System.Random();
    private int index;
    private int index2;
    private string selectedTerm;
    private int callStack = 0;

    public GameObject introTimeline;
    public GameObject levelChanger;
    private LevelChanger levelChangerHandler;

    public GameObject player;
    MovementInput movementInputHandler;

    public GameObject thirdPersonCam;
    public GameObject thirdPersonCrouchCam;

    CinemachineFreeLook thirdPersonCamHandler;
    CinemachineFreeLook thirdPersonCrouchCamHandler;

    // Use this for initialization
    void Start()
    {
        enabled = false;
        levelChangerHandler = levelChanger.GetComponent<LevelChanger>();
        movementInputHandler = player.GetComponent<MovementInput>();

        thirdPersonCamHandler = thirdPersonCam.GetComponent<CinemachineFreeLook>();
        thirdPersonCrouchCamHandler = thirdPersonCrouchCam.GetComponent<CinemachineFreeLook>();

        if(session.invertYaxis)
        {
            thirdPersonCamHandler.m_YAxis.m_InvertInput = true;
            thirdPersonCrouchCamHandler.m_YAxis.m_InvertInput = true;
        }
        else
        {
            thirdPersonCamHandler.m_YAxis.m_InvertInput = false;
            thirdPersonCrouchCamHandler.m_YAxis.m_InvertInput = false;
        }

        languagePackInterface = new LanguagePackInterface(session.selectedDeck);
        if (languagePackInterface.Cards.Count > 5)
        {
            Debug.Log("successfully retrieved the cards from the csv file");
        }
        else
        {
            Debug.Log("could not get the cards from the csv file");
        }

        //
        // dest_term is the word while source_term is the translation
        //

        // create a chosen buffer list holding 4 possible translations.
        // If a translation is picked twice, then call the GetTranslationOption() method again and do the check



        //
        // get questions from csv and put them into sets of 3
        //
        Queue<QuestionModel> questions = new Queue<QuestionModel>();
        string optionA;
        string optionB;
        string optionC;
        string optionD;

        foreach (Card card in languagePackInterface.Cards)
        {
            Buffer.Clear();
            callStack = 0;
            //
            // place correct answer
            //
            index2 = r2.Next(0, 3);
            optionA = (index2 == 0 ? card.sourceTerm : GetTranslation(Buffer, card.sourceTerm));
            optionB = (index2 == 1 ? card.sourceTerm : GetTranslation(Buffer, card.sourceTerm));
            optionC = (index2 == 2 ? card.sourceTerm : GetTranslation(Buffer, card.sourceTerm));
            optionD = (index2 == 3 ? card.sourceTerm : GetTranslation(Buffer, card.sourceTerm));

            questions.Enqueue(new QuestionModel { Question = card.destTerm, isAnswered = false, OptionA = optionA, OptionB = optionB, OptionC = optionC, OptionD = optionD, CorrectOption = index2 });
        }

        //questions.Enqueue(new QuestionModel { Question = "Sample question 1", isAnswered = false, OptionA = "test1 A", OptionB = "test1 B", OptionC = "test1 C", OptionD = "test1 D", CorrectOption = "A"});
        //questions.Enqueue(new QuestionModel { Question = "Sample question 2", isAnswered = false, OptionA = "test2 A", OptionB = "test2 B", OptionC = "test2 C", OptionD = "test2 D", CorrectOption = "A" });
        //questions.Enqueue(new QuestionModel { Question = "Sample question 3", isAnswered = false, OptionA = "test3 A", OptionB = "test3 B", OptionC = "test3 C", OptionD = "test3 D", CorrectOption = "A" });

        // check if questions are multiple of 3
        if (!(questions.Count % 3 == 0))
        {
            questions.Dequeue();
            Debug.Log("total questions are not a multiple of 3, first dequeue");
            if (!(questions.Count % 3 == 0))
            {
                questions.Dequeue();
                Debug.Log("total questions are not a multiple of 3, second dequeue");
            }

        }
        // then place 3 questions in each set
        for (int i = 0; i < stations.Count; i++)
        {
            List<QuestionModel> questionSetUnit = new List<QuestionModel>();
            questionSetUnit.Add(questions.Dequeue());
            questionSetUnit.Add(questions.Dequeue());
            questionSetUnit.Add(questions.Dequeue());

            questionSets.Add(questionSetUnit);
        }

        //
        // shuffle questionSets, then enqueue
        //
        questionSets = ShuffleSet(questionSets);

        foreach (List<QuestionModel> questionSet in questionSets)
        {
            questionSetsQueue.Enqueue(questionSet);
        }

        // assign each station a number and distribute questions to each station
        for (int i = 0; i < stations.Count; i++)
        {
            stationInteractScript = stations[i].GetComponent<StationInteractScript>();
            stationInteractScript.stationID = i;
            stationInteractScript.questions = questionSetsQueue.Dequeue();

        }

        //
        // start introTimeline
        //
        levelChangerHandler.FadeInLevel();
        introTimeline.SetActive(true);
        movementInputHandler.PlayIntroAnimaton();


    }

    public string GetTranslation(List<string> Buffer, string sourceTerm)
    {
        callStack++;
        if (callStack > 10)
        {
            Debug.Log("callStack > 10, terminating recursive method");
            return null;
        }
        //
        // get random source_term from deck
        // 
        index = r.Next(0, languagePackInterface.Cards.Count - 1);
        selectedTerm = languagePackInterface.Cards[index].sourceTerm;

        if(selectedTerm == sourceTerm)
        {
            GetTranslation(Buffer, sourceTerm);
        }
        else if (!Buffer.Contains(selectedTerm))
        {
            Buffer.Add(selectedTerm);
        }
        else
        {
            GetTranslation(Buffer, sourceTerm);
        }

        return selectedTerm;
    }

    public List<List<QuestionModel>> ShuffleSet(List<List<QuestionModel>> aList)
    {

        System.Random _random = new System.Random();

        List<QuestionModel> myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }

}
