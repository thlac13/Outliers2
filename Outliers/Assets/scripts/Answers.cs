using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Answers
{
	public string[] answers;
    //public string quest;
	
	  public Answers(int size /*string str*/)
    {
		this.answers = new string[size];
        //this.quest = str;
	}
}