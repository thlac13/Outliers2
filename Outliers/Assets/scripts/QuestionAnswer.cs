using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class QuestionAnswer
{
	public string[] questions;
	public string[] ans1;
	public string[] ans2;
    public string quest;
    public string answer1;
    public string answer2;
	
	    public QuestionAnswer(int size)
    {
		this.questions = new string[size];
		this.ans1 = new string[size];
		this.ans2 = new string[size];
	}
	
	
} 