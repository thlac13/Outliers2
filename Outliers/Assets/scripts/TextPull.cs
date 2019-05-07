using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Text;
using System.Linq;


public class TextPull : MonoBehaviour
{
    public GameInfo game;
    public submitAndRequest subs;
    public QuestionAnswer qa;

    public List<string> players1 = new List<string>();
    public List<string> players2 = new List<string>();
    public Text G1text;
    public Text G2text;

    public static string urlFire = "https://outliers-c0774.firebaseio.com/";

    // Start is called before the first frame update
    void Start()
    {
     /*   int numplayers = game.numPlayersAns;
        string answer = null;
        for (int i = 0;  i< numplayers; i++)
        {
            string player = game.orderOfPlayers[i];
            RestClient.Get<User>(urlFire + "Sessions/" + game.gameCode + "/User/" + player + "/Round1" + ".json").Then(onResolved: response =>
            {
                answer = response.name;
            });
            //Null check
            if(string.IsNullOrEmpty(answer) || string.IsNullOrEmpty(qa.ans1[1]))
            {
                System.Console.WriteLine("Empty text fields!");
            }
            //check to see which answer was chosen
            if (string.Compare(answer, qa.ans1[1]) == 0){
                players1.Add(player);
                G1text.GetComponent<Text>().text += player + ", ";
            }
            else
            {
                players2.Add(player);
                G2text.GetComponent<Text>().text += player + ", ";
            }
        }
*/
    } 

}
