
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
    public static String[] players;
    public static int maxPlayers;
    public static int numPlayers;

    public User user;
    public GameInfo gameInfo;
	public QuestionAnswer questAndAns;
	public Answers userAnswers;
    public TextPull textpull;
	
    public InputField playerNameText;
    public InputField questionText;
    public InputField gameNameText;
    public InputField getGameString;
	public InputField questionAns1Input;
	public InputField questionAns2Input;
    public InputField maxPlayersText;

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
            Data.gPlayerName = playerName;
			responseText.text = "Welcome!";
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

            questAndAns = new QuestionAnswer();

            questAndAns.quest = question;
            questAndAns.answer1 = questionAns1Text;
            questAndAns.answer2 = questionAns2Text;

            if(gameInfo.question + 1 > gameInfo.questPerRound || gameInfo.round == 0)
            {
                gameInfo.question = 1;
                gameInfo.round = gameInfo.round + 1;
            }
            else
            {
                gameInfo.question = gameInfo.question + 1;
            }

            roundNum = gameInfo.round;
            questionNum = gameInfo.question;

            RestClient.Put(urlFire + "Sessions/" + gameString + "/Round" + roundNum + "/Question" + questionNum +".json", questAndAns);
            gameInfo.questionerAskedQuest = true;
            gameInfo.allQuestionersResp = false;
            RestClient.Put(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json", gameInfo);
            Data.gInfo = gameInfo;

            SceneManager.LoadScene("outliersLobbyPlayers");
        }
        else
        {
            responseText.text = "The game has not been started yet";
        }
            
    }

   
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
            responseText.text = "You need to have a game name and max players";
        }
        else
        {
            gameString = RandomString();
			Data.gCode = gameString;
            gameName = gameNameText.text;
			Data.gName = gameName;
            gameInfo = new GameInfo(gameString, gameName);
            maxPlayers = Int32.Parse(maxPlayersText.text);
            Data.gMaxPlayers = maxPlayers;
            gameInfo.maxNumPlayers = maxPlayers;
            players = new string[maxPlayers - 1];
            players[0] = playerName;
            gameInfo.playerNames = players;
            Data.gPlayers = players;
            gameInfo.numOfPlayers = 1;

			Data.gInfo = gameInfo;

            RestClient.Put(urlFire + "Sessions/" +gameString + "/GameInfo" + ".json", gameInfo);
			user = new User();
			RestClient.Put(urlFire+ "Sessions/" + gameString + "/Users/" + playerName + ".json",user);
            responseText.text = "Use this code to invite your friends:   " + gameString;
		
            SceneManager.LoadScene("startGame");
        }
            
    }

    public void StartGame()
    {
        gameInfo.gameReady = true;
        Data.gInfo = gameInfo;
		Data.gReady = true;
        if(gameInfo.maxNumPlayers <= 9)
		{
			gameInfo.questPerRound = 2;
		}else if (gameInfo.maxNumPlayers <= 17)
		{
			gameInfo.questPerRound = 3;
		}else if (gameInfo.maxNumPlayers <= 33)
		{
			gameInfo.questPerRound = 4;
		} else {
			//double minus 1
			gameInfo.questPerRound = 5;
		}
        
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
            Debug.Log("Join was hit");

            RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
            {
                gameInfo = response;
				Data.gInfo = gameInfo;
                gameName = gameInfo.getName();
				Data.gName = gameName;
                Debug.Log("in the get request");
				
				//Maybe put text in next screene saying you joined the game
                //responseText.text = "You have joined game " + gameName;
                Debug.Log("Joined game");
                sendJoin();
            
            });
			
			
        }
        
    }

    private bool sendJoin()
    {
        if (gameInfo.gameReady)
        {
            RestClient.Get<User>(urlFire + "Sessions/" + gameString + "/Users/" + playerName + ".json").Then(onResolved: response =>
            {
                user = response;
                Data.gUser = user;
            });
        }
        else
        {
            user = new User();
            Data.gUser = user;
            gameInfo.playerNames[gameInfo.numOfPlayers] = playerName;
            gameInfo.numOfPlayers = gameInfo.numOfPlayers + 1;
            Data.gInfo = gameInfo;

            RestClient.Put(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json", gameInfo);
            RestClient.Put(urlFire + "Sessions/" + gameString + "/Users/" + playerName + ".json", user);
            SceneManager.LoadScene("outliersLobbyPlayers");
            //lobbyText.text = "Waiting for Players...";
        }
        return true;
    }

    public void submitAnswer1() {

        roundNum = gameInfo.round;
        questionNum = gameInfo.question;

        userAnswers = new Answers();
        userAnswers.answer = questAndAns.answer1;

        RestClient.Put(urlFire + "Sessions/" + gameString + "/Round" + roundNum + "/Question" + questionNum + "/Users/" + playerName +".json", userAnswers);

        RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
        {
            gameInfo = response;

            gameInfo.numPlayersAns = gameInfo.numPlayersAns + 1;
            if(gameInfo.numPlayersAns == gameInfo.numOfPlayers - 1)
            {
                gameInfo.numPlayersAns = 0;
                gameInfo.allQuestionersResp = true;
            }

            RestClient.Put(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json", gameInfo);
            Data.gInfo = gameInfo;


            SceneManager.LoadScene("outliersLobbyPlayers");
        });

    }

    public void submitAnswer2()
    {
        roundNum = gameInfo.round;
        questionNum = gameInfo.question;

        userAnswers = new Answers();
        userAnswers.answer = questAndAns.answer2;

        RestClient.Put(urlFire + "Sessions/" + gameString + "/Round" + roundNum + "/Question" + questionNum + "/Users/" + playerName + ".json", userAnswers);

        RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
        {
            gameInfo = response;

            gameInfo.numPlayersAns = gameInfo.numPlayersAns + 1;
            if (gameInfo.numPlayersAns == gameInfo.numOfPlayers - 1)
            {
                gameInfo.numPlayersAns = 0;
                gameInfo.allQuestionersResp = true;
            }

            RestClient.Put(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json", gameInfo);
            Data.gInfo = gameInfo;


            SceneManager.LoadScene("outliersLobbyPlayers");
        });
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
	
    /*
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
    */

    public void refresh()
    {
        RestClient.Get<GameInfo>(urlFire + "Sessions/" + gameString + "/GameInfo" + ".json").Then(onResolved: response =>
        {
            gameInfo = response;
            Data.gInfo = gameInfo;

            if (gameInfo.gameReady)
            {
                if (areQuestioner())
                {
                    SceneManager.LoadScene("outliersQuestionMaker");
                }
                else
                { 
                    if (gameInfo.questionerAskedQuest)
                    {
                        getAnswer(); 
                    }
                    else
                    {
                        //no new info, maybe put that
                    }
                }
            }

        });
    }

    private void getAnswer()
    {
        questAndAns = new QuestionAnswer();

        RestClient.Get<QuestionAnswer>(urlFire + "Sessions/" + gameString + "/Round" +gameInfo.round + "/Question" + gameInfo.question + ".json").Then(onResolved: response =>
        {
            Data.gQuestAndAns = response;

            SceneManager.LoadScene("outliersAsker");
        });
    }

    private bool areQuestioner()
    {
        if (gameInfo.allQuestionersResp)
        {
            if (gameInfo.question + 1 > gameInfo.questPerRound)
            {
                if (gameInfo.playerNames[gameInfo.round + 1] == playerName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (gameInfo.playerNames[gameInfo.round] == playerName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
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
