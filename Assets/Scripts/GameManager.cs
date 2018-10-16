using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager 
{

    private bool _isEnded = false;

    public void Win()
    {

    }

    public void End()
    {
        if(!_isEnded)
        {
            Debug.Log("end of the game");
            _isEnded = true;

            PlayerManager.GetInstance().BlockAllPlayers(true);
        }
    }

    public void Loose()
    {

    }

    public void StartGame()
    {

    }

    public void StartMenu()
    {

    }

    public override void Reset()
    {

    }

    public void Restart()
    {
        StartCoroutine(TimerRestart(3.0f));
    }

    IEnumerator TimerRestart(float time)
    {
        yield return new WaitForSeconds(time);
        StartMenu();
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
