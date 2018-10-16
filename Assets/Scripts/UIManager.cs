using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager
{
    public Text scoreText = null;
    public Text clientsText = null;

    public void ChangeScoreText(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void ChangeClientsText(int score)
    {
        clientsText.text = "Clients Remaining : " + score;
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static UIManager _instance;

    public static UIManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
