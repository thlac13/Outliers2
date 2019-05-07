using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questionAskerOnLoad : MonoBehaviour
{
    public Text question;
    public Text answerOne;
    public Text answerTwo;
    public Text questioner;
    // Start is called before the first frame update
    void Start()
    {
        question.text =  Data.gQuestAndAns.quest;
        answerOne.text = "Answer 1: " + Data.gQuestAndAns.answer1;
        answerTwo.text = "Answer 2: " + Data.gQuestAndAns.answer2;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
