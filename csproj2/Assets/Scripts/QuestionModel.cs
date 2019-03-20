using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionModel {

    public string ID { get; set; }
    public string Question { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }
    public int CorrectOption { get; set; }
    public int CurrentSelection { get; set; }

    public bool isAnswered { get; set; }


}
