using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameInfo 
{
    public string gameCode;
    public string gameName;
    public bool gameReady;
    public int round;
    public int question;
    public int questPerRound;
    public String[] playerNames;
	public int numPlayersAns;
	public bool questionerAskedQuest;
    public int numOfPlayers;
    public int maxNumPlayers;
    public bool allQuestionersResp;


    public GameInfo(string gameCode, string gameName)
    {
        this.gameCode = gameCode;
        this.gameName = gameName;
        this.gameReady = false;
        this.round = 0;
        this.question = 0;
        this.questPerRound = 0;
		this.numPlayersAns = 0;
		this.questionerAskedQuest = false;
        this.numOfPlayers = 0;
        this.maxNumPlayers = 0;
        this.allQuestionersResp = false;
    }

    public string getCode()
    {
        return gameCode;
    }
    public string getName()
    {
        return gameName;
    }
}
