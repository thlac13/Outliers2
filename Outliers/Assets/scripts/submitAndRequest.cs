
//Need rest client for unity from assests store
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using System.Random;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class submitAndRequest : MonoBehaviour
{
    //for a user
    public static string playerName;
    public static string question;
    public static bool playerVac;
    public static string gameString;
    public static string gameName;
    public static int roundNum;
    public static int questionNum;
	public static string questionAns1Text;
	public static string questionAns2Text;

    public User user;
    public GameInfo gameInfo;
	public QuestionAnswer questAndAns;
	public Answers userAnswers;
	
    public InputField playerNameText;
    public InputField questionText;
    public InputField gameNameText;
    public InputField getGameString;
	public InputField questionAns1Input;
	public InputField questionAns2Input;

    public Text responseText;
	public Text lobbyText;
	public Text questionSceneQuestion;
	public Text questionSceneAns1;
	public Text questionSceneAns2;


    //String of URL for Firebase
    public static string urlFire = "https://outliers-c0774.firebaseio.com/";




    // Start is called before the first frame update
    void Start()
    {
        //responseText.text = "Player Name : Question";
        playerVac = false;
		
		//set all the values
		user = Data.gUser;
		gameInfo = Data.gInfo;
		questAndAns = Data.gQuestAndAns;
		userAnswers = Data.gUserAnswers;
		playerName = Data.gPlayerName;
		gameString = Data.gCode;
		gameName = Data.gName;
		roundNum = Data.gRound;
		questionNum = Data.gQuestNum;
    }
	
	public void EnterName()
	{
		if (playerNameText.text == "")
        {
            responseText.text = "You need a player name";
        }else{
			playerName = playerNameText.text;
			responseText.text = "";
            SceneManager.LoadScene("lobby");
        }
	}

    //When the submit button is pressed
    public void OnSubmitQuestion()
    {
        if (questionText.text == "" || questionAns1Input.text == "" || questionAns2Input.text == "")
        {
            responseText.text = "You need a question and two answers";
        }
        else if (gameInfo.gameReady)
        {
            question = questionText.text; //sets value of text field
			questionAns1Text = questionAns1Input.text;
			questionAns2Text = questionAns2Input.text;
            
			if(roundNum == 1){
				questAndAns = new QuestionAnswer(gameInfo.questPerRound);
                //questAndAns.questions[1] = question;
                //questAndAns.ans1[1] = questionAns1Text;
                //questAndAns.ans2[1] = questionAns2Text;
                questAndAns.quest = question;
                questAndAns.answer1 = questionAns1Text;
                questAndAns.answer2 = questionAns2Text;
				RestClient.Put(urlFire+ "Sessions/" + gameString + "/Round1" + ".json",questAndAns);
				gameInfo.questionerAskedQuest = true;
				RestClient.Put(urlFire + "Sessions/" +gameString + "/GameInfo" + ".json", gameInfo);
				
			}else{
				questAndAns.questions[roundNum] = question;
				questAndAns.ans1[roundNum] = questionAns1Text;
				questAndAns.ans2[roundNum] = questionAns2Text;
				RestClient.Put(urlFire+ "Sessions/" + gameString + "/Round" + roundNum + ".json", questAndAns);
                //gotolobby waiting room
                gameInfo.questionerAskedQuest = true;
				RestClient.Put(urlFire + "Sessions/" +gameString + "/GameInfo" + ".json", gameInfo);

			}
            //responseText.text = "Submited player " + playerName;
            SceneManager.LoadScene("outliersLobbyPlayers");
        }
        else
        {
            responseText.text = "The game has not been started yet";
        }
            
    }

    /*public void OnGetQuestion()
    {
        if (playerNameText.text == "")
        {
            responseText.text = "You need to have a player name";
        }
        else
        {
            playerName = playerNameText.text;
            RetrieveFromDataBase();
        }
    }
    */
    public void SwitchVacMode()
    {
        if (playerVac) {
            playerVac = false;
        }
        else
        {
            playerVac = true;
        }
    }

    public void NewGame()
    {
        if (gameNameText.text == "")
        {
            responseText.text = "You need to have a game name";
        }
        else
        {
            gameString = RandomString();
			Data.gCode = gameString;
            gameName = gameNameText.text;
			Data.gName = gameName;
            gameInfo = new GameInfo(gameString, gameName);
			Data.gInfo = gameInfo;
			//gameInfo.orderOfPlayers.Add(playerName);
            //gameInfo.gameCode = gameString;
            //gameInfo.gameName = gameName;
            RestClient.Put(urlFire + "Sessions/" +gameString + "/GameInfo" + ".json", gameInfo);
			user = new User();
			RestClient.Put(urlFire+ "Sessions/" + gameString + "/User/" + playerName + ".json",user);
            responseText.text = "Use this code to invite your friends:   " + gameString;
			
			//add a button press of put the code in the next scene
            SceneManager.LoadScene("startGame");
        }
            
    }

    public void StartGame()
    {
        gameInfo.gameReady = true;
		Data.gReady = true;
		gameInfo.round = 1;
		Data.gRound = 1;
		gameInfo.question = 1;
		Data.gQuestNum = 1;
		roundNum = 1;
		questionNum = 1;
        /*if(gameInfo.orderOfPlayers.Count <= 9)
		{
			gameInfo.questPerRound = 2;
		}else if (gameInfo.orderOfPlayers.Count <= 17)
		{
			gameInfo.questPerRound = 3;
		}else if (gameInfo.orderOfPlayers.Count <= 33)
		{
			gameInfo.questPerRound = 4;
		} else {
			//double minus 1
			gameInfo.questPerRound = 5;
		}*/
        gameInfo.questPerRound = 2;
        RestClient.Put(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json", gameInfo);
        //responseText.text = "You have started a game";
        SceneManager.LoadScene("outliersQuestionMaker");
    }

    public void JoinGame()
    {
        if (getGameString.text == "")
        {
            responseText.text = "You need enter a game code";
        }
        else
        {
            gameString = getGameString.text;
			Data.gCode = gameString;

            RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
            {
                gameInfo = response;
				Data.gInfo = gameInfo;
                gameName = gameInfo.getName();
				Data.gName = gameName;
				
				//Maybe put text in next screene saying you joined the game
                responseText.text = "You have joined game " + gameName;
            
            });
			
			if(gameInfo.gameReady){
                RestClient.Get<User>(urlFire + "Sessions/" + gameString + "/User/" + playerName + ".json").Then(onResolved: response =>
                 {
                     user = response;
					 Data.gUser = user;
                 });
			}else{
				user = new User();
				Data.gUser = user;
				//gameInfo.orderOfPlayers.Add(playerName);
				RestClient.Put(urlFire + "Sessions/" +gameString + "/GameInfo" + ".json", gameInfo);
				RestClient.Put(urlFire+ "Sessions/" + gameString + "/User/" + playerName + ".json",user);
                SceneManager.LoadScene("outliersLobbyPlayers");
                //lobbyText.text = "Waiting for Players...";
			}
        }
        
    }
 
    public void submitAnswer1(){
		if (roundNum==1){
			userAnswers = new Answers(gameInfo.questPerRound);
			userAnswers.answers[1] = questAndAns.ans1[1];
			RestClient.Put(urlFire+ "Sessions/" + gameString + "/User/" + playerName + "/Round1" +".json",userAnswers);
		}else{
			userAnswers = new Answers(gameInfo.questPerRound);
			userAnswers.answers[roundNum] = questAndAns.ans1[roundNum];
			RestClient.Put(urlFire+ "Sessions/" + gameString + "/User/" + playerName + "/Round" + roundNum +".json",userAnswers);
		}
        SceneManager.LoadScene("outliersLobbyPlayers");
    }

    public void submitAnswer2()
    {
        if (roundNum == 1)
        {
            userAnswers = new Answers(gameInfo.questPerRound);
            userAnswers.answers[1] = questAndAns.ans2[1];
            RestClient.Put(urlFire + "Sessions/" + gameString + "/User/" + playerName + "/Round1" + ".json", userAnswers);
        }
        else
        {
            userAnswers = new Answers(gameInfo.questPerRound);
            userAnswers.answers[roundNum] = questAndAns.ans2[roundNum];
            RestClient.Put(urlFire + "Sessions/" + gameString + "/User/" + playerName + "/Round" + roundNum + ".json", userAnswers);
        }
        SceneManager.LoadScene("outliersLobbyPlayers");
    }



    //private void PostToDataBase()
    //{
    //User user1 = new User();
    //RestClient.Put( urlFire+ "Sessions/" + gameString + "/User/" + playerName + ".json",user1);
    //}

    //private void RetrieveFromDataBase()
    //{
    //RestClient.Get<User>(urlFire + "Sessions/" + gameString + "/User/" + playerName + ".json").Then( onResolved: response =>
    //{
    //user = response;
    //UpdateQuestion();
    //});
    //}

    /*private void UpdateQuestion()
    {
        responseText.text = "Player: " + user.name + "     Question: " + user.question;
    }
    */
    private string RandomString()
    {
        int size = 7;
        StringBuilder builder = new StringBuilder();
        System.Random random = new System.Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        return builder.ToString();
    }
	
	private void goToLobby(){
        //get in the lobby scene
        bool ready = false;
		while (ready == false)
		{
		System.Threading.Thread.Sleep(3500);
		ready = seeIfReady();
		}
		
		if (seeIfQuestioner()){
            //SceneManager.LoadScene("outliers");
        }
        else{
			//stay in lobby
			lobbyText.text = "Joined game, waiting for questioner to ask question";
			//going to wait again
			while (ready == false)
			{
				System.Threading.Thread.Sleep(3500);
				ready = seeIfWaiting();
			}
            SceneManager.LoadScene("outliersAsker");
            questionSceneAns1.text = questAndAns.ans1[roundNum];
			questionSceneAns2.text = questAndAns.ans1[roundNum];
			questionSceneQuestion.text = questAndAns.questions[roundNum];
			//go to question scene
		}
	}

    public void refresh()
    {
        RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
        {
            gameInfo = response;
            if (gameInfo.questionerAskedQuest)
            {
                SceneManager.LoadScene("outliersAsker");
            }
        });
    }
	
	private bool seeIfReady()
	{
		RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
            {
                gameInfo = response;
                
            });
        if (gameInfo.gameReady)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
	
	private bool seeIfWaiting()
	{
		RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
            {
                gameInfo = response;
                
            });
        if (gameInfo.questionerAskedQuest)
        {
            RestClient.Get<QuestionAnswer>(urlFire + "Sessions/" + gameString + "/Round" + roundNum + ".json").Then(onResolved: response =>
            {
                questAndAns = response;

            });
            return true;

        }
        else
        {
            return false;
        }
    }
	
	private bool seeIfQuestioner(){
		//if(playerName == gameInfo.orderOfPlayers[gameInfo.round]){
			//return true;
		//}else{
			return false;
		//}
	}

}
