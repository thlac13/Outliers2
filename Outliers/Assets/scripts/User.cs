using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User 
{

    //public string question;
    public string name;
    public bool vacation;
    public bool wasOutlier;
    public bool isQuestioner;
    //public Character character;

    public User ()
    {
        //question = submitAndRequest.question;
        name = submitAndRequest.playerName;
        vacation = submitAndRequest.playerVac;
        this.wasOutlier = false;
        this.isQuestioner = false;
        //character.charname = character.RandomChar();
        
    }

}
