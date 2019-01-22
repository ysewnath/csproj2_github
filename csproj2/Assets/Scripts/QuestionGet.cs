using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGet : MonoBehaviour {

    public List<List<QuestionModel>> questionSets = new List<List<QuestionModel>>();
    Queue<List<QuestionModel>> questionSetsQueue = new Queue<List<QuestionModel>>();
    public List<GameObject> stations;
    StationInteractScript stationInteractScript;

    // Use this for initialization
    void Start () {
        enabled = false;

        //
        // get questions from database and put them into sets of 3
        //
        Queue<QuestionModel> questions = new Queue<QuestionModel>();

        questions.Enqueue(new QuestionModel { Question = "Sample question 1", isAnswered = false, OptionA = "test1 A", OptionB = "test1 B", OptionC = "test1 C", OptionD = "test1 D", CorrectOption = "A"});
        questions.Enqueue(new QuestionModel { Question = "Sample question 2", isAnswered = false, OptionA = "test2 A", OptionB = "test2 B", OptionC = "test2 C", OptionD = "test2 D", CorrectOption = "A" });
        questions.Enqueue(new QuestionModel { Question = "Sample question 3", isAnswered = false, OptionA = "test3 A", OptionB = "test3 B", OptionC = "test3 C", OptionD = "test3 D", CorrectOption = "A" });

        // check if questions are multiple of 3
        if(!(questions.Count % 3 == 0))
        {
            Debug.Log("total questions are not a multiple of 3");
            return;
        }
        // then place 3 questions in each set
        for(int i = 0; i < stations.Count; i++)
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

        foreach(List<QuestionModel> questionSet in questionSets)
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
