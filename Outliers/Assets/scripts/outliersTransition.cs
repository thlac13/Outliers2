using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outliersTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Transitioncreate()
    {
        SceneManager.LoadScene("create");
    }

    public void Transitionjoim()
    {
        SceneManager.LoadScene("outliersJoin");
    }

    public void Transitionstartgame()
    { 
        SceneManager.LoadScene("startGame");
    }
}
